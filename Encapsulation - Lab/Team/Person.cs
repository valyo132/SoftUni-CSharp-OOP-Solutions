using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsInfo
{
    public class Person
    {
        public Person(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }

        private decimal salary;

        public decimal Salary
        {
            get { return salary; }
            private set
            {
                if (value < 650)
                    throw new ArgumentException("Salary cannot be less than 650 leva!");
                else 
                    salary = value;
            }

        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (value.Length < 3)
                    throw new ArgumentException("First name cannot contain fewer than 3 symbols!");
                else
                    firstName = value;
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (value.Length < 3)
                    throw new ArgumentException("Last name cannot contain fewer than 3 symbols!");
                else
                    firstName = value;
            }
        }


        private int age;

        public int Age
        {
            get { return age; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Age cannot be zero or a negative integer!");
                else
                    age = value;
            }
        }

        public void IncreaseSalary(decimal percentage)
        {
            if (Age < 30)
                Salary = Salary + (Salary * (percentage / 200));
            else
                Salary = Salary + (Salary * (percentage / 100));
        }

        public override string ToString()
            => $"{FirstName} {LastName} receives {Salary:f2} leva.";
    }
}
