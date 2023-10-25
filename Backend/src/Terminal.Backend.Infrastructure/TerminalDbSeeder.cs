using Serilog;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.DAL;

using ILogger = Serilog.ILogger;

namespace Terminal.Backend.Infrastructure;

internal sealed class TerminalDbSeeder
{
    private readonly TerminalDbContext _dbContext;
    private readonly ILogger _logger;

    public TerminalDbSeeder(TerminalDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Seed()
    {
        if (_dbContext.Tags.Any()) return; 
        // Tags
        var tag1 = new Tag("new-measurement");
        _dbContext.Tags.Add(tag1);
        var tag2 = new Tag("methane-rich");
        _dbContext.Tags.Add(tag2);
        var tag3 = new Tag("popular-measurement");
        _dbContext.Tags.Add(tag3);
        var tag4 = new Tag("hot");
        _dbContext.Tags.Add(tag4);
        var tag5 = new Tag("high-pressure");
        _dbContext.Tags.Add(tag5);
        _dbContext.SaveChanges();
        
        
        // Projects
        var projectUpturn = new Project(ProjectId.Create(), "Upturn");
        _dbContext.Projects.Add(projectUpturn);
        var projectBessy2 = new Project(ProjectId.Create(), "Bessy 2");
        _dbContext.Projects.Add(projectBessy2);
        var projectNitro = new Project(ProjectId.Create(), "Nitro");
        _dbContext.Projects.Add(projectNitro);
        var projectNobelium = new Project(ProjectId.Create(), "Nobelium");
        _dbContext.Projects.Add(projectNobelium);
        _dbContext.SaveChanges();

        // Parameters
        var bcParameter = new IntegerParameter("B/C", "ppm", 1);
        _dbContext.IntegerParameters.Add(bcParameter);
        var hydrogenParameter = new IntegerParameter("H\u2082", "sccm", 1);
        _dbContext.IntegerParameters.Add(hydrogenParameter);
        var methaneParameter = new IntegerParameter("CH\u2084", "sccm", 1);
        _dbContext.IntegerParameters.Add(methaneParameter);
        var diboranParameter = new IntegerParameter("B\u2082H\u2086", "sccm", 1);
        _dbContext.IntegerParameters.Add(diboranParameter);
        var nucleationParameter = new TextParameter("Nucleation Method",
            new List<string>
            {
                "spin-coating",
                "nucleation",
                "dip-coating",
                "without nucleation"
            });
        _dbContext.TextParameters.Add(nucleationParameter);
        var temperatureParameter = new IntegerParameter("Temperature", "C\u2070", 1);
        _dbContext.IntegerParameters.Add(temperatureParameter);
        var pressureParameter = new IntegerParameter("Pressure", "Torr", 1);
        _dbContext.IntegerParameters.Add(pressureParameter);
        var powerParameter = new IntegerParameter("Pmw", "W", 1);
        _dbContext.IntegerParameters.Add(powerParameter);
        var timeParameter = new DecimalParameter("Time", "h", 0.1m);
        _dbContext.DecimalParameters.Add(timeParameter);
        var substrateParameter = new TextParameter("Substrate",
            new List<string>
            {
                "silicon",
                "silicon dioxide",
                "glass",
                "tantalum"
            });
        _dbContext.TextParameters.Add(substrateParameter);
        var bufferParameter = new DecimalParameter("Buffer", "h", 0.1m);
        _dbContext.DecimalParameters.Add(bufferParameter);
        var additionalGasesParameter = new TextParameter("Additional gases", new List<string> { "nitrogen", "oxygen" });
        _dbContext.TextParameters.Add(additionalGasesParameter);
        _dbContext.SaveChanges();
        
        // Measurement
        var measurement1 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectUpturn.Measurements.Add(measurement1);
        _dbContext.Measurements.Add(measurement1);
        _dbContext.SaveChanges();
        
        var measurement2 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectUpturn.Measurements.Add(measurement2);
        _dbContext.Measurements.Add(measurement2);
        _dbContext.SaveChanges();
        
        var measurement3 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectBessy2.Measurements.Add(measurement3);
        _dbContext.Measurements.Add(measurement3);
        _dbContext.SaveChanges();
        
        var measurement4 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectBessy2.Measurements.Add(measurement4);
        _dbContext.Measurements.Add(measurement4);
        _dbContext.SaveChanges();
        
        var measurement5 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNitro.Measurements.Add(measurement5);
        _dbContext.Measurements.Add(measurement5);
        _dbContext.SaveChanges();
        
        var measurement6 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNitro.Measurements.Add(measurement6);
        _dbContext.Measurements.Add(measurement6);
        _dbContext.SaveChanges();
        
        var measurement7 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNitro.Measurements.Add(measurement7);
        _dbContext.Measurements.Add(measurement7);
        _dbContext.SaveChanges();
        
        
        var measurement8 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNobelium.Measurements.Add(measurement8);
        _dbContext.Measurements.Add(measurement8);
        _dbContext.SaveChanges();
        
        var measurement9 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNobelium.Measurements.Add(measurement9);
        _dbContext.Measurements.Add(measurement9);
        _dbContext.SaveChanges();
        
        var measurement10 = new Measurement(MeasurementId.Create(), null, new Comment("First measurement!"), new List<Step>
        {
            new(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
            {
                new IntegerParameterValue(bcParameter, 2000),
                new IntegerParameterValue(hydrogenParameter, 300),
                new IntegerParameterValue(methaneParameter, 100),
                new IntegerParameterValue(diboranParameter, 240),
                new TextParameterValue(nucleationParameter, nucleationParameter.AllowedValues.First()),
                new IntegerParameterValue(temperatureParameter, 800),
                new IntegerParameterValue(pressureParameter, 20),
                new IntegerParameterValue(powerParameter, 1300),
                new DecimalParameterValue(timeParameter, 2.3m),
                new TextParameterValue(substrateParameter, substrateParameter.AllowedValues.First()),
                new DecimalParameterValue(bufferParameter, 0.0m),
                new TextParameterValue(additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
            })
        }, new List<Tag> { tag1, tag3, tag5 });
        projectNobelium.Measurements.Add(measurement10);
        _dbContext.Measurements.Add(measurement10);
        _dbContext.SaveChanges();
    }
}