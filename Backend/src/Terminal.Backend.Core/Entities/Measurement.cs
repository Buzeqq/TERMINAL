using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Measurement
{
    public MeasurementId Id { get; private set; }
    public MeasurementCode Code { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public Comment Comment { get; private set; }
    
    public Project Project { get; private set; }
    public Recipe? Recipe { get; private set; }
    public ICollection<Step> Steps { get; private set; } = new List<Step>();
    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    public Measurement(MeasurementId id, Project project, Recipe? recipe, Comment comment, ICollection<Step> steps, ICollection<Tag> tags)
    {
        Id = id;
        Recipe = recipe;
        Comment = comment;
        Steps = steps;
        Tags = tags;
        CreatedAtUtc = DateTime.UtcNow;
    }

    private Measurement(MeasurementId id, MeasurementCode code, DateTime createdAtUtc, Comment comment)
    {
        Id = id;
        Code = code;
        CreatedAtUtc = createdAtUtc;
        Comment = comment;
    }
}