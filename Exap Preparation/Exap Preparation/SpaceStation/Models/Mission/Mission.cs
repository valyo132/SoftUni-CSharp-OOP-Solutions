namespace SpaceStation.Models.Mission
{
    using System.Collections.Generic;
    using System.Linq;

    using SpaceStation.Models.Astronauts.Contracts;
    using SpaceStation.Models.Mission.Contracts;
    using SpaceStation.Models.Planets.Contracts;

    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var person in astronauts)
            {
                while (person.CanBreath && planet.Items.Any())
                {
                    var item = planet.Items.First();
                    person.Bag.Items.Add(item);
                    planet.Items.Remove(item); 
                    person.Breath();
                }
            }
        }
    }
}
