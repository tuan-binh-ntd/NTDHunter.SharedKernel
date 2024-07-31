namespace NTDHunter.SharedKernel;

/// <summary>
/// NOTE: Use `readonly record struct` for most cases in C# 10+
/// See: https://nietras.com/2021/06/14/csharp-10-record-struct/
/// 
/// For this class implementation, reference:
/// See: https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
/// </summary>
[Serializable]
public abstract class ValueObject : IComparable, IComparable<ValueObject>
{
    private int? _cachedHashCode;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// Compare 2 value objects
    /// </summary>
    /// <param name="obj">An instance of object</param>
    /// <returns>True if two value objects equals; otherwise false</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (GetUnproxiedType(this) != GetUnproxiedType(obj))
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    /// GetHashCode
    /// </summary>
    /// <returns>A value od hashcode</returns>
    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }

    /// <summary>
    /// Compare two value objects
    /// </summary>
    /// <param name="obj">An instance of object</param>
    /// <returns></returns>
    public int CompareTo(object? obj)
    {
        if (obj == null)
            return 1;

        var thisType = GetUnproxiedType(this);
        var otherType = GetUnproxiedType(obj);

        if (thisType != otherType)
            return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);

        var other = (ValueObject)obj;

        var components = GetEqualityComponents().ToArray();
        var otherComponents = other.GetEqualityComponents().ToArray();

        for (var i = 0; i < components.Length; i++)
        {
            var comparison = CompareComponents(components[i], otherComponents[i]);
            if (comparison != 0)
                return comparison;
        }

        return 0;
    }

    private static int CompareComponents(object? object1, object? object2)
    {
        if (object1 is null && object2 is null)
            return 0;

        if (object1 is null)
            return -1;

        if (object2 is null)
            return 1;

        if (object1 is IComparable comparable1 && object2 is IComparable comparable2)
            return comparable1.CompareTo(comparable2);

        return object1.Equals(object2) ? 0 : -1;
    }

    /// <summary>
    /// Compare two value objects
    /// </summary>
    /// <param name="other">An instance of value object</param>
    /// <returns></returns>
    public int CompareTo(ValueObject? other)
    {
        return CompareTo(other as object);
    }

    /// <summary>
    /// == implicit operator
    /// </summary>
    /// <param name="a">An instance of value object</param>
    /// <param name="b">An instance of value object</param>
    /// <returns>True if <paramref name="a"/> is the same <paramref name="b"/>; otherwise false</returns>
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    /// <summary>
    /// != implicit operator
    /// </summary>
    /// <param name="a">An instance of value object</param>
    /// <param name="b">An instance of value object</param>
    /// <returns>True if <paramref name="a"/> is the not same <paramref name="b"/>; otherwise false</returns>
    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }

    internal static Type GetUnproxiedType(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        const string EFCoreProxyPrefix = "Castle.Proxies.";
        const string NHibernateProxyPostfix = "Proxy";

        var type = obj.GetType();
        var typeString = type.ToString();

        if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            return type.BaseType!;

        return type;
    }
}
