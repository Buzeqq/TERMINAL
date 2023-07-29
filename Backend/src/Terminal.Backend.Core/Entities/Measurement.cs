using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public class Measurement
{
    public MeasurementId Id { get; private set; }
    public RecipeId RecipeId { get; private set; }
    public List<Step> Steps { get; private set; }
    public List<Tag> Tags { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public string Comment { get; private set; }
}