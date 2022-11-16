namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string AnalyzeAccessModifiers(string classToInvestigate)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType($"{(typeof(Spy).Namespace)}.{classToInvestigate}");
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] nonPublicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }
            foreach (var method in nonPublicMethods.Where(x => x.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }
            foreach (var method in publicMethods.Where(x => x.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().TrimEnd();
        }

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
