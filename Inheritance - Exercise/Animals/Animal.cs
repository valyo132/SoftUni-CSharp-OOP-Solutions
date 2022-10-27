using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animals
{
    public class Animal
    {
        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public virtual string ProduceSound()
            => null;

        public override string ToString()
            => $"{GetType().ToString().Split('.').Last()}" + Environment.NewLine +
                 $"{Name} {Age} {Gender}" + Environment.NewLine +
            ProduceSound();
    }
}
