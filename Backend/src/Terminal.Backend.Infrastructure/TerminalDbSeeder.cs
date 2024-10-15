using Bogus;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Infrastructure;

internal sealed class TerminalDbSeeder(TerminalDbContext dbContext)
{
    private readonly Faker _faker = new();

    public void Seed()
    {
        var projects = CreateProjects(100).ToList();
        var tags = CreateTags(1000).ToList();
        dbContext.Projects.AddRange(projects);
        dbContext.Tags.AddRange(tags);

        var textParameters = CreateTextParameters(3).ToList();
        var decimalParameters = CreateIntegerParameters(4).ToList();
        var integerParameters = CreateDecimalParameters(5).ToList();

        List<Parameter> parameters = [
            ..textParameters,
            ..decimalParameters,
            ..integerParameters
        ];
        dbContext.Parameters.AddRange(parameters);

        var samples = CreateSamples(1000, projects, tags, parameters);
        dbContext.Samples.AddRange(samples);
        dbContext.SaveChanges();
    }

    private IEnumerable<Project> CreateProjects(int amount)
        => Enumerable.Range(0, amount)
        .Select(i => new Project(
            _faker.Random.Guid(),
            _faker.Company.CompanyName() + i,
            _faker.Random.Bool(0.9f)
        ));

    private IEnumerable<Tag> CreateTags(int amount)
        => Enumerable.Range(0, amount)
            .Select(i => new Tag(
                _faker.Random.Guid(),
                _faker.Commerce.ProductAdjective() + i,
                _faker.Random.Bool(0.9f)
            ));

    private IEnumerable<TextParameter> CreateTextParameters(int amount)
        => Enumerable.Range(0, amount)
            .Select(i => new TextParameter(
                _faker.Random.Guid(),
                _faker.Database.Column() + i,
                null,
                _faker.Lorem.Words(_faker.Random.Int(1, 5)).ToList()
            ));

    private IEnumerable<IntegerParameter> CreateIntegerParameters(int amount)
        => Enumerable.Range(0, amount)
            .Select(i => new IntegerParameter(
                _faker.Random.Guid(),
                _faker.Database.Column() + i,
                null,
                _faker.Random.String2(_faker.Random.Int(1, 4)),
                _faker.PickRandom(1, 10, 1000, 10_000, 100_000, 1_000_000)
            ));

    private IEnumerable<DecimalParameter> CreateDecimalParameters(int amount)
        => Enumerable.Range(0, amount)
            .Select(i => new DecimalParameter(
                _faker.Random.Guid(),
                _faker.Database.Column() + i,
                null,
                _faker.Random.String2(_faker.Random.Int(1, 4)),
                _faker.PickRandom(0.1m, 1m, 10m, 100m, 1_000m, 10_000m, 100_000m)
            ));

    private IEnumerable<Sample> CreateSamples(int amount, IEnumerable<Project> projects, IEnumerable<Tag> tags, IEnumerable<Parameter> parameters)
        => Enumerable.Range(0, amount)
            .Select(_ => new Sample(
                _faker.Random.Guid(),
                _faker.PickRandom(projects),
                null,
                _faker.Rant.Review(),
                CreateSampleSteps(_faker.Random.Int(1, 10), parameters).ToList(),
                _faker.PickRandom(tags, _faker.Random.Int(1, 5)).ToList()
            ));

    private IEnumerable<ParameterValue> CreateParameterValues(IEnumerable<Parameter> parameters)
        => parameters.Select<Parameter, ParameterValue>(p => p switch
            {
                TextParameter textParameter => new TextParameterValue(
                    _faker.Random.Guid(),
                    textParameter,
                    _faker.PickRandom(textParameter.AllowedValues)
                ),
                DecimalParameter decimalParameter => new DecimalParameterValue(
                    _faker.Random.Guid(),
                    decimalParameter,
                    decimalParameter.Step * _faker.Random.Int(min: (int)(-1_000_000 / decimalParameter.Step), max: (int)(1_000_000/ decimalParameter.Step))
                ),
                IntegerParameter integerParameter => new IntegerParameterValue(
                    _faker.Random.Guid(),
                    integerParameter,
                    integerParameter.Step * _faker.Random.Int(min: -1_000_000 / integerParameter.Step, max: 1_000_000 / integerParameter.Step)
                ),
                _ => throw new ArgumentOutOfRangeException(nameof(parameters))
            });

    private IEnumerable<SampleStep> CreateSampleSteps(int amount, IEnumerable<Parameter> parameters)
        => Enumerable.Range(0, amount)
            .Select(_ => new SampleStep(
                _faker.Random.Guid(),
                _faker.Rant.Review(),
                CreateParameterValues(parameters).ToList()));
}
