using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Infrastructure;

internal sealed class TerminalDbSeeder
{
    private readonly TerminalDbContext _dbContext;

    public TerminalDbSeeder(TerminalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        #region tags
        var tag1 = new Tag("new-measurement");
        var tag2 = new Tag("methane-rich");
        var tag3 = new Tag("popular-measurement");
        var tag4 = new Tag("hot");
        var tag5 = new Tag("high-pressure");
        #endregion

        #region projects

        var projectUpturn = new Project(ProjectId.Create(), "Upturn");
        var projectBessy2 = new Project(ProjectId.Create(), "Bessy 2");
        var projectNitro = new Project(ProjectId.Create(), "Nitro");
        var projectNobelium = new Project(ProjectId.Create(), "Nobelium");

        #endregion

        #region parameters

        var bcParameter = new IntegerParameter("B/C", "ppm", 1);
        var hydrogenParameter = new IntegerParameter("H\u2082", "sccm", 1);
        var methaneParameter = new IntegerParameter("CH\u2084", "sccm", 1);
        var diboranParameter = new IntegerParameter("B\u2082H\u2086", "sccm", 1);
        var nucleationParameter = new TextParameter("Nucleation Method",
            new List<string>
            {
                "spin-coating",
                "nucleation",
                "dip-coating",
                "without nucleation"
            });
        var temperatureParameter = new IntegerParameter("Temperature", "C\u2070", 1);
        var pressureParameter = new IntegerParameter("Pressure", "Torr", 1);
        var powerParameter = new IntegerParameter("Pmw", "W", 1);
        var timeParameter = new DecimalParameter("Time", "h", 0.1m);
        var substrateParameter = new TextParameter("Substrate",
            new List<string>
            {
                "silicon",
                "silicon dioxide",
                "glass",
                "tantalum"
            });
        var bufferParameter = new DecimalParameter("Buffer", "h", 0.1m);
        var additionalGasesParameter = new TextParameter("Additional gases", new List<string> { "nitrogen", "oxygen" });

        #endregion

        #region steps

        var step1 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step2 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step3 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step4 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step5 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step6 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step7 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step8 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step9 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        var step10 = new Step(StepId.Create(), new Comment("First step!"), new List<ParameterValue>
        {
            new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000),
            new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300),
            new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100),
            new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240),
            new TextParameterValue(ParameterValueId.Create(), nucleationParameter, nucleationParameter.AllowedValues.First()),
            new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800),
            new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20),
            new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300),
            new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m),
            new TextParameterValue(ParameterValueId.Create(), substrateParameter, substrateParameter.AllowedValues.First()),
            new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m),
            new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter, additionalGasesParameter.AllowedValues.First())
        });

        #endregion

        #region measurements

        var measurement1 = new Measurement(
            MeasurementId.Create(),
            projectUpturn,
            null,
            new Comment("First measurement!"),
            new List<Step> { step1 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement1);
        _dbContext.SaveChanges();

        var measurement2 = new Measurement(
            MeasurementId.Create(),
            projectUpturn,
            null,
            new Comment("First measurement!"),
            new List<Step> { step2 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement2);
        _dbContext.SaveChanges();

        var measurement3 = new Measurement(
            MeasurementId.Create(),
            projectBessy2,
            null,
            new Comment("First measurement!"),
            new List<Step> { step3 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement3);
        _dbContext.SaveChanges();

        var measurement4 = new Measurement(
            MeasurementId.Create(),
            projectBessy2,
            null,
            new Comment("First measurement!"),
            new List<Step> { step4 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement4);
        _dbContext.SaveChanges();

        var measurement5 = new Measurement(
            MeasurementId.Create(),
            projectNitro,
            null,
            new Comment("First measurement!"),
            new List<Step> { step5 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement5);
        _dbContext.SaveChanges();

        var measurement6 = new Measurement(
            MeasurementId.Create(),
            projectNitro,
            null,
            new Comment("First measurement!"),
            new List<Step> { step6 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement6);
        _dbContext.SaveChanges();

        var measurement7 = new Measurement(
            MeasurementId.Create(),
            projectNobelium,
            null,
            new Comment("First measurement!"),
            new List<Step> { step7 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement7);
        _dbContext.SaveChanges();

        var measurement8 = new Measurement(
            MeasurementId.Create(),
            projectNobelium,
            null,
            new Comment("First measurement!"),
            new List<Step> { step8 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement8);
        _dbContext.SaveChanges();

        var measurement9 = new Measurement(
            MeasurementId.Create(),
            projectNobelium,
            null,
            new Comment("First measurement!"),
            new List<Step> { step9 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement9);
        _dbContext.SaveChanges();

        var measurement10 = new Measurement(
            MeasurementId.Create(),
            projectNitro,
            null,
            new Comment("First measurement!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Measurements.Add(measurement10);
        _dbContext.SaveChanges();

        #endregion

        #region add

        _dbContext.Tags.AddRange(tag1, tag2, tag3, tag4, tag5);
        _dbContext.Projects.AddRange(projectUpturn, projectBessy2, projectNitro, projectNobelium);
        _dbContext.Parameters.AddRange(bufferParameter,
            substrateParameter,
            timeParameter,
            powerParameter,
            pressureParameter,
            temperatureParameter,
            nucleationParameter,
            diboranParameter,
            methaneParameter,
            bcParameter,
            hydrogenParameter,
            additionalGasesParameter);

        #endregion

        _dbContext.SaveChanges();
    }
}