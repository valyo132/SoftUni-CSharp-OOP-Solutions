namespace Stealer
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Spy
    {
        public string CollectGettersAndSetters(string classToInvestigate)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType($"{typeof(Spy).Namespace}.{classToInvestigate}");

            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var method in methods.Where(x => x.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }

            foreach (var method in methods.Where(x => x.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string classToInvestigate)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType($"{(typeof(Spy).Namespace)}.{classToInvestigate}");
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            sb.AppendLine($"All Private Methods of Class: {type}");
            sb.AppendLine($"Base Class {type.BaseType.Name}");

            foreach (var method in methods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();
        }

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
