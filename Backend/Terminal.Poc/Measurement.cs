using System.ComponentModel.DataAnnotations;

namespace Terminal.Poc;

public class Measurement
{
    public Measurement(decimal value)
    {
        Id = new Guid();
        Value = value;
        CreatedOnUtc = DateTime.UtcNow;
    }

    [Key] public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}

public class PostMeasurementRequest
{
    public decimal Value { get; set; }
}

public class PutMeasurementRequest
{
    public decimal Value { get; set; }
}