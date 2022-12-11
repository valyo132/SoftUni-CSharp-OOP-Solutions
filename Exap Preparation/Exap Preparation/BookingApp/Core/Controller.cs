namespace BookingApp.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using BookingApp.Core.Contracts;
    using BookingApp.Models.Bookings;
    using BookingApp.Models.Hotels;
    using BookingApp.Models.Rooms;
    using BookingApp.Models.Rooms.Contracts;
    using BookingApp.Repositories;
    using BookingApp.Utilities.Messages;

    public class Controller : IController
    {
        private HotelRepository hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            if (hotels.Select(hotelName) != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            var hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            var hotel = hotels.Select(hotelName);

            if (hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }

            IRoom room = roomTypeName switch
            {
                nameof(DoubleBed) => new DoubleBed(),
                nameof(Studio) => new Studio(),
                nameof(Apartment) => new Apartment(),
                _ => throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect)
            };

            hotel.Rooms.AddNew(room);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            var hotel = hotels.Select(hotelName);

            if (roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            if (hotel.Rooms.Select(roomTypeName) == null)
            {
                return OutputMessages.RoomTypeNotCreated;
            }

            var room = hotel.Rooms.Select(roomTypeName);

            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);

            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (hotels.All().FirstOrDefault(x => x.Category == category) == null)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            var orderedHotels =
                this.hotels.All().Where(x => x.Category == category).OrderBy(x => x.FullName);

            foreach (var hotel in orderedHotels)
            {
                IRoom room = hotel.Rooms.All()
                    .Where(x => x.PricePerNight > 0)
                    .Where(y => y.BedCapacity >= adults + children)
                    .OrderBy(x => x.BedCapacity)
                    .FirstOrDefault();

                if (room != null)
                {
                    var bookingNumber = hotel.Bookings.All().Count + 1;
                    var booking = new Booking(room, duration, adults, children, bookingNumber);

                    hotel.Bookings.AddNew(booking);

                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }

            return OutputMessages.RoomNotAppropriate;
        }

        public string HotelReport(string hotelName)
        {
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            var hotel = hotels.Select(hotelName);

            StringBuilder result = new StringBuilder();

            result.AppendLine($"Hotel name: {hotelName}");
            result.AppendLine($"--{hotel.Category} star hotel");
            result.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            result.AppendLine($"--Bookings:");
            result.AppendLine();

            if (hotel.Bookings.All().Count > 0)
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    result.AppendLine(booking.BookingSummary());
                    result.AppendLine();
                }
            }
            else
            {
                result.AppendLine("none");
            }

            return result.ToString().TrimEnd();
        }
    }
}
