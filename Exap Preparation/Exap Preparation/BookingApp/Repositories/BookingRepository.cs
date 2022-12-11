namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using BookingApp.Models.Bookings.Contracts;
    using BookingApp.Repositories.Contracts;

    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookigs;

        public BookingRepository()
        {
            bookigs = new List<IBooking>();
        }

        public void AddNew(IBooking model)
        {
            bookigs.Add(model);
        }

        public IReadOnlyCollection<IBooking> All()
            => bookigs.AsReadOnly();

        public IBooking Select(string criteria)
            => bookigs.FirstOrDefault(x => x.BookingNumber.ToString() == criteria);   
    }
}
