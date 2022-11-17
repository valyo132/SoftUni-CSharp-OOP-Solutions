namespace ValidationAttributes.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Attributes;

    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            Type type = obj.GetType();

            // Get all the properties that has an attribute of type MyValidationAttribute.
            PropertyInfo[] props = type.GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(MyValidationAttribute)).Any())
                .ToArray();

            foreach (var prop in props)
            {
                object value = prop.GetValue(obj);

                // Get the attribute that is onherited by the current property (prop).
                MyValidationAttribute attribute = prop.GetCustomAttribute<MyValidationAttribute>();
                bool isValid = attribute.IsValid(value);

                if (!isValid)
                    return false;
            }

            return true;
        }
    }
}
