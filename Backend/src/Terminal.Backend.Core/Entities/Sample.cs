using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Core.Entities;

public sealed class Sample
{
    public SampleId Id { get; private set; }
    public SampleCode Code { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public Comment Comment { get; private set; }
    
    public Project Project { get; private set; }
    public Recipe? Recipe { get; private set; }
    public ICollection<Step> Steps { get; private set; } = new List<Step>();
    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    public Sample(SampleId id, Project project, Recipe? recipe, Comment comment, ICollection<Step> steps, ICollection<Tag> tags)
    {
        Id = id;
        Recipe = recipe;
        Comment = comment;
        Steps = steps;
        Tags = tags;
        Project = project;
        CreatedAtUtc = DateTime.UtcNow;
    }

    private Sample(SampleId id, SampleCode code, DateTime createdAtUtc, Comment comment)
    {
        Id = id;
        Code = code;
        CreatedAtUtc = createdAtUtc;
        Comment = comment;
    }
}