using System.Reflection;

namespace Terminal.Backend.Core.Abstractions;

public abstract class Enumeration<TEnum, TId> : IEquatable<Enumeration<TEnum, TId>>
    where TEnum : Enumeration<TEnum, TId> where TId : notnull
{
    private static readonly Dictionary<TId, TEnum>? Enumerations = CreateEnumerations();

    public TId Value { get; protected init; }

    public string Name { get; protected init; }

    protected Enumeration(TId value, string name)
    {
        Value = value;
        Name = name;
    }

    public static TEnum? FromValue(TId value)
    {
        return Enumerations!.TryGetValue(value, out var enumeration) ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations!
            .Values
            .SingleOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<TEnum> GetValues()
    {
        return Enumerations!.Values;
    }

    public static bool IsDefined(TId id) => Enumerations?.ContainsKey(id) ?? true;

    public static implicit operator string(Enumeration<TEnum, TId> x) => x.Name;

    private static Dictionary<TId, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);
        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }

    #region equetable

    public bool Equals(Enumeration<TEnum, TId>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TId>.Default.Equals(Value, other.Value) && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Enumeration<TEnum, TId>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Name);
    }

    public static bool operator ==(Enumeration<TEnum, TId>? left, Enumeration<TEnum, TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Enumeration<TEnum, TId>? left, Enumeration<TEnum, TId>? right)
    {
        return !Equals(left, right);
    }

    #endregion
}