namespace CommandPattern.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using CommandPattern.Core.Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private ICommand command;

        public string Read(string args)
        {
            string[] tokens = args.Split();

            string commandName = tokens[0] + "Command";
            string[] argsForExcetution = tokens.Skip(1).ToArray();

            // Get the assmebly witch name is suitalbe with commandName.
            Type type = Assembly.GetCallingAssembly().GetTypes().First(x => x.Name == commandName);
            command = Activator.CreateInstance(type) as ICommand;

            string result = command.Execute(argsForExcetution);

            return result;
        }
    }
}
