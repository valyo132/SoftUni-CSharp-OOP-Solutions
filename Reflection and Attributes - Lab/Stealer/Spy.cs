namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string StealFieldInfo(string classToInvestigate, params string[] fieldToCheck)
        {
            Type type = Type.GetType(classToInvestigate);

            BindingFlags allFlags = (BindingFlags)60;
            FieldInfo[] fields = type.GetFields(allFlags);

            object instanceClass = Activator.CreateInstance(type);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Class under investigation: {classToInvestigate}");

            foreach (var property in fields.Where(x => fieldToCheck.Contains(x.Name)))
            {
                sb.AppendLine($"{property.Name} = {property.GetValue(instanceClass)}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
