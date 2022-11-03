namespace BorderControl
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var peopleList = new List<BaseHuman>();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string name = tokens[0];

                if (tokens.Length == 2)
                {
                    string id = tokens[1];
                    BaseHuman robot = new Robot(name, id);
                    peopleList.Add(robot);
                }
                else if (tokens.Length == 3)
                {
                    int age = int.Parse(tokens[1]);
                    string id = tokens[2];
                    BaseHuman human = new Citizent(name, age, id);
                    peopleList.Add(human);
                }
            }

            string fakeIdInput = Console.ReadLine();

            foreach (BaseHuman thing in peopleList)
                thing.CheckId(fakeIdInput);
        }
    }
}
