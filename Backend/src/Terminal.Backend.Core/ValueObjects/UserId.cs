namespace Terminal.Backend.Core.ValueObjects;

public record UserId(Guid Value)
{
    public static UserId Create() => new(Guid.NewGuid());
    public static implicit operator UserId(Guid id) => new(id);
    public static implicit operator Guid(UserId id) => id.Value;
    public static implicit operator string(UserId id) => id.ToString();
    public static implicit operator UserId(string id) => new(Guid.Parse(id));
}
