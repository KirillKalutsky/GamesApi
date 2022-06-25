using System.Reflection;

namespace GamesApi
{
    public static class Extensions
    {
        public static bool IsAnyPropertiesNullOrEmpty<T>(this T obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return true;

            return obj.GetType().GetProperties()
                .Any(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        public static bool IsAllPropertiesNullOrEmpty<T>(this T obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return true;

            return obj.GetType().GetProperties()
                .All(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        private static bool IsNullOrEmpty(object value)
        {
            if (Object.ReferenceEquals(value, null))
                return true;

            var type = value.GetType();
            return type.IsValueType
                && Object.Equals(value, Activator.CreateInstance(type));
        }
    }
}
