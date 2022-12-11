namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using BookingApp.Models.Rooms.Contracts;
    using BookingApp.Repositories.Contracts;

    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }

        public void AddNew(IRoom model)
        {
            rooms.Add(model);
        }

        public IReadOnlyCollection<IRoom> All()
            => rooms.AsReadOnly();

        public IRoom Select(string criteria)
            => rooms.FirstOrDefault(x => x.GetType().Name == criteria);
    }
}
