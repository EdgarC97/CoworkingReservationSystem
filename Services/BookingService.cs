using CoworkingReservationSystem.Interfaces.Services;
using CoworkingReservationSystem.Models.DTOs.Requests;
using CoworkingReservationSystem.Models.DTOs.Responses;
using CoworkingReservationSystem.Models;
using CoworkingReservationSystem.Repositories.Interfaces;

namespace CoworkingReservationSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;

        public BookingService(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository,
            IUserRepository userRepository,
            IAuditService auditService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _auditService = auditService;
        }

        public async Task<BookingResponse> CreateBookingAsync(CreateBookingRequest request, int userId)
        {
            if (request.StartTime >= request.EndTime)
            {
                throw new InvalidOperationException("Start time must be before end time");
            }

            if (request.StartTime < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Cannot book in the past");
            }

            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var overlappingBookings = await _bookingRepository.GetOverlappingBookingsAsync(
                request.RoomId, request.StartTime, request.EndTime);

            if (overlappingBookings.Any())
            {
                throw new InvalidOperationException("Room is already booked for the selected time");
            }

            var booking = new Booking
            {
                UserId = userId,
                RoomId = request.RoomId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = BookingStatus.Confirmed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdBooking = await _bookingRepository.CreateAsync(booking);

            await _auditService.CreateAuditAsync(
                userId,
                "Booking",
                createdBooking.Id.ToString(),
                AuditAction.Create,
                null,
                $"Booking created for room {room.Name} from {request.StartTime} to {request.EndTime}"
            );

            return await MapToBookingResponseAsync(createdBooking);
        }

        public async Task<BookingResponse> CancelBookingAsync(int bookingId, CancelBookingRequest request, int userId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            if (booking.UserId != userId)
            {
                throw new InvalidOperationException("You can only cancel your own bookings");
            }

            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new InvalidOperationException("Booking is already cancelled");
            }

            if (booking.StartTime <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Cannot cancel a booking that has already started");
            }

            var oldStatus = booking.Status;
            booking.Status = BookingStatus.Cancelled;
            booking.CancellationReason = request.CancellationReason;
            booking.UpdatedAt = DateTime.UtcNow;

            await _bookingRepository.UpdateAsync(booking);

            await _auditService.CreateAuditAsync(
                userId,
                "Booking",
                booking.Id.ToString(),
                AuditAction.Cancel,
                $"Status: {oldStatus}",
                $"Status: {booking.Status}, Reason: {booking.CancellationReason}"
            );

            return await MapToBookingResponseAsync(booking);
        }

        public async Task<IEnumerable<BookingResponse>> GetUserBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            var responses = new List<BookingResponse>();

            foreach (var booking in bookings)
            {
                responses.Add(await MapToBookingResponseAsync(booking));
            }

            return responses;
        }

        public async Task<BookingResponse> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            return await MapToBookingResponseAsync(booking);
        }

        private async Task<BookingResponse> MapToBookingResponseAsync(Booking booking)
        {
            var room = booking.Room ?? await _roomRepository.GetByIdAsync(booking.RoomId);
            var user = booking.User ?? await _userRepository.GetByIdAsync(booking.UserId);

            return new BookingResponse
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                RoomName = room?.Name,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Status = booking.Status,
                CancellationReason = booking.CancellationReason,
                CreatedAt = booking.CreatedAt,
                User = user != null ? new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedAt = user.CreatedAt
                } : null,
                Room = room != null ? new RoomResponse
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description,
                    Capacity = room.Capacity,
                    HasProjector = room.HasProjector,
                    HasWhiteboard = room.HasWhiteboard,
                    CreatedAt = room.CreatedAt
                } : null
            };
        }
    }
}