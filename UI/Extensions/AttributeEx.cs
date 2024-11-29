using System.Reflection;

namespace Extensions;

public static class AttributeEx
{
    public static IEnumerable<ShowAttribute> GetShowAttributeElementsFrom<TValue>(
        this IEnumerable<TValue> collection, bool order = false)
    {
        var showAttributes = collection.Select(x => x!.GetType().GetCustomAttribute<ShowAttribute>())
            .Where(x => x != null)
            .Select(x => x!);
        return order ? showAttributes.OrderBy(x => x.OrderPriority) : showAttributes;
    }

    public static ShowAttribute? GetShowAttribute<TValue>(this TValue value) =>
        value.GetAttribute<TValue, ShowAttribute>();

    public static TAttribute? GetAttribute<TValue, TAttribute>(this TValue value) where TAttribute : Attribute =>
        value!.GetType().GetCustomAttribute<TAttribute>();

    public static bool TryGetShowAttribute<TValue>(TValue value, out ShowAttribute? showAttribute)
    {
        showAttribute = value.GetShowAttribute();
        return showAttribute != null;
    }
}