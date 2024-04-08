using DataLayer.Interfaces;
using System.Reflection;

namespace DataLayer.Helpers;

public static class EntityCopy<TEntity> where TEntity : class, IEntity
{
    private static readonly List<PropertyInfo> sourceProperties = new();
    private static readonly List<PropertyInfo> targetProperties = new();

    public static void Copy(TEntity source, TEntity target, params string[] excludeFields)
    {
        Initialize(excludeFields);

        for (int i = 0; i < sourceProperties.Count; i++)
            targetProperties[i].SetValue(target, sourceProperties[i].GetValue(source, null), null);
    }

    private static void Initialize(params string[] excludeFields)
    {
        sourceProperties.Clear();
        targetProperties.Clear();

        List<string> excludeProperties = [.. excludeFields];

        var excludeEntityProperties = typeof(IEntity)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name)
            .Where(name => name != "Name"); // Don't exclude the property Name.

        excludeProperties.AddRange(excludeEntityProperties);

        var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo sourceProperty in properties)
        {
            // If source property cannot be read or must be excluded, skip the current iteration.
            if (!sourceProperty.CanRead || excludeProperties.Contains(sourceProperty.Name))
                continue;

            PropertyInfo? targetProperty = typeof(TEntity).GetProperty(sourceProperty.Name);
            if (targetProperty != null)
            {
                // If target property cannot be setted because is readonly, skip the current iteration.
                if (!targetProperty.CanWrite)
                    continue;

                // If target property cannot be setted because it doesn't have a set method, skip the current iteration.
                if (targetProperty.GetSetMethod()?.Attributes.HasFlag(MethodAttributes.Static) == false)
                    continue;

                //bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));
                sourceProperties.Add(sourceProperty);
                targetProperties.Add(targetProperty);
            }
        }
    }
}