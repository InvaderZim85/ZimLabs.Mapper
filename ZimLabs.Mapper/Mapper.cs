using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZimLabs.Mapper
{
    /// <summary>
    /// Provides the functions to map two objects
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Maps the values of the properties of <paramref name="source"/> object into the properties of the <paramref name="target"/> object
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TTarget">The type of the target object</typeparam>
        /// <param name="source">The source object that provides the values for the target object</param>
        /// <param name="target">The target object, which takes over the values from the source object</param>
        /// <exception cref="ArgumentNullException">Will be thrown when the target or the source is null</exception>
        public static void Map<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            var targetProperties = target.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();

            foreach (var targetProperty in targetProperties)
            {
                // Check if the property should be ignored
                if (targetProperty.IgnoreProperty())
                    continue;

                // Determine the source property
                var sourceProperty = sourceProperties.FirstOrDefault(f =>
                    f.Name.Equals(targetProperty.Name, StringComparison.OrdinalIgnoreCase));

                // If the source property is null, skip the rest
                if (sourceProperty == null)
                    continue;

                // Check if the types are matching...
                if (targetProperty.PropertyType == sourceProperty.PropertyType)
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
                else
                {
                    // The types are not matching, check if maybe a type is nullable
                    var targetType = Nullable.GetUnderlyingType(targetProperty.PropertyType);
                    var sourceType = Nullable.GetUnderlyingType(sourceProperty.PropertyType);
                    var sourceValue = sourceProperty.GetValue(source);

                    // The source type is nullable, but has the same underlying type
                    if (targetType == null && sourceType != null && targetProperty.PropertyType == sourceType)
                    {
                        if (sourceValue == null)
                            continue; // The value is null, so skip the rest

                        targetProperty.SetValue(target, sourceValue);
                    }
                    // The target type is nullable, but has the same type
                    else if (targetType != null && sourceType == null && targetType == sourceProperty.PropertyType)
                    {
                        targetProperty.SetValue(target, sourceValue);
                    }
                    // No else, because if both types are nullable, the first if were true
                }

            }
        }

        /// <summary>
        /// Maps the values of the properties of <paramref name="source"/> object into the properties of the new created target object
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TTarget">The type of the target object</typeparam>
        /// <param name="source">The source object that provides the values for the target object</param>
        /// <returns>The new created property</returns>
        /// <exception cref="ArgumentNullException">Will be thrown when the source is null</exception>
        public static TTarget CreateAndMap<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            var instance = Activator.CreateInstance<TTarget>();

            Map(source, instance);

            return instance;
        }

        /// <summary>
        /// Gets the changes of the t
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="newObj"></param>
        /// <returns></returns>
        public static List<ChangeEntry> GetChanges<T>(this T obj, T newObj) where T : class
        {
            var properties = typeof(T).GetProperties();

            return (from property in properties
                where !property.IgnoreProperty()
                let oldValue = property.GetValue(obj)
                let newValue = property.GetValue(newObj)
                    where !Equals(oldValue, newValue)
                select new ChangeEntry(property.Name, oldValue, newValue)).ToList();
        }

        /// <summary>
        /// Checks if the property should be ignored
        /// </summary>
        /// <param name="propertyInfo">The property info</param>
        /// <returns>true when the property should be ignore, otherwise false</returns>
        private static bool IgnoreProperty(this MemberInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<IgnorePropertyAttribute>();
            return attribute != null;
        }
    }
}
