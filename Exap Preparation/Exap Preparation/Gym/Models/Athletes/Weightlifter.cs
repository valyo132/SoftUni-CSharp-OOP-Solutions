namespace Gym.Models.Athletes
{
    public class Weightlifter : Athlete
    {
        public Weightlifter(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals)
        {
            this.Stamina = 50;
        }

        public override void Exercise()
        {
            this.Stamina += 10;

            base.Exercise();
        }
    }
}
