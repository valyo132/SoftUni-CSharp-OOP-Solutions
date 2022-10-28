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
            set { salary = value; }
        }


        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            private set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            private set { lastName = value; }
        }


        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
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
