namespace MilitaryElite.Interfaces
{
    using System.Collections.Generic;

    public interface ICommando
    {
        public List<Mission> Missions { get; set; }
    }
}
