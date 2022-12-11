namespace BookingApp.Models.Bookings
{
    using System;
    using System.Text;

    using BookingApp.Models.Bookings.Contracts;
    using BookingApp.Models.Rooms.Contracts;
    using BookingApp.Utilities.Messages;

    public class Booking : IBooking
    {
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            BookingNumber = bookingNumber;
        }

        public IRoom Room
        {
            get { return room; }
            private set { room = value; }
        }

        public int ResidenceDuration
        {
            get { return residenceDuration; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurationZeroOrLess);
                }

                residenceDuration = value;
            }
        }

        public int AdultsCount
        {
            get { return adultsCount; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.AdultsZeroOrLess);
                }

                adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get { return childrenCount; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.ChildrenNegative);
                }

                childrenCount = value;
            }
        }

        public int BookingNumber
        {
            get { return bookingNumber; }
            private set { bookingNumber = value; }
        }

        public string BookingSummary()
        {
            StringBuilder result = new StringBuilder();

            var totalPaid = Math.Round(this.ResidenceDuration * this.Room.PricePerNight, 2);

            result.AppendLine($"Booking number: {this.BookingNumber}");
            result.AppendLine($"Room type: {this.Room.GetType().Name}");
            result.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            result.AppendLine($"Total amount paid: {totalPaid:F2} $");

            return result.ToString().TrimEnd();
        }
    }
}
