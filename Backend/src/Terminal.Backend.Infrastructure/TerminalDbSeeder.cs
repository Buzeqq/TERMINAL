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
        var tag1 = new Tag(TagId.Create(), "new-sample");
        var tag2 = new Tag(TagId.Create(), "methane-rich");
        var tag3 = new Tag(TagId.Create(), "popular-sample");
        var tag4 = new Tag(TagId.Create(), "hot");
        var tag5 = new Tag(TagId.Create(), "high-pressure");
        
        _dbContext.Tags.AddRange(tag1, tag2, tag3, tag4, tag5);
        #endregion

        #region projects

        var projectUpturn = new Project(ProjectId.Create(), "Upturn");
        var projectBessy2 = new Project(ProjectId.Create(), "Bessy 2");
        var projectNitro = new Project(ProjectId.Create(), "Nitro");
        var projectNobelium = new Project(ProjectId.Create(), "Nobelium");

        #endregion

        #region parameters

        var bcParameter = new IntegerParameter(ParameterId.Create(), "B/C", "ppm", 1);
        var hydrogenParameter = new IntegerParameter(ParameterId.Create(), "H\u2082", "sccm", 1);
        var methaneParameter = new IntegerParameter(ParameterId.Create(), "CH\u2084", "sccm", 1);
        var diboranParameter = new IntegerParameter(ParameterId.Create(), "B\u2082H\u2086", "sccm", 1);
        var nucleationParameter = new TextParameter(ParameterId.Create(), "Nucleation Method",
            new List<string>
            {
                "spin-coating",
                "nucleation",
                "dip-coating",
                "without nucleation"
            });
        var temperatureParameter = new IntegerParameter(ParameterId.Create(), "Temperature", "C\u2070", 1);
        var pressureParameter = new IntegerParameter(ParameterId.Create(), "Pressure", "Torr", 1);
        var powerParameter = new IntegerParameter(ParameterId.Create(), "Pmw", "W", 1);
        var timeParameter = new DecimalParameter(ParameterId.Create(), "Time", "h", 0.1m);
        var substrateParameter = new TextParameter(ParameterId.Create(), "Substrate",
            new List<string>
            {
                "silicon",
                "silicon dioxide",
                "glass",
                "tantalum"
            });
        var bufferParameter = new DecimalParameter(ParameterId.Create(), "Buffer", "h", 0.1m);
        var additionalGasesParameter = new TextParameter(ParameterId.Create(), "Additional gases", new List<string> { "nitrogen", "oxygen" });

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

        #region samples

        var sample1 = new Sample(
            SampleId.Create(),
            projectUpturn,
            null,
            new Comment("First sample!"),
            new List<Step> { step1 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample1);
        _dbContext.SaveChanges();

        var sample2 = new Sample(
            SampleId.Create(),
            projectUpturn,
            null,
            new Comment("First sample!"),
            new List<Step> { step2 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample2);
        _dbContext.SaveChanges();

        var sample3 = new Sample(
            SampleId.Create(),
            projectBessy2,
            null,
            new Comment("First sample!"),
            new List<Step> { step3 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample3);
        _dbContext.SaveChanges();

        var sample4 = new Sample(
            SampleId.Create(),
            projectBessy2,
            null,
            new Comment("First sample!"),
            new List<Step> { step4 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample4);
        _dbContext.SaveChanges();

        var sample5 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step5 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample5);
        _dbContext.SaveChanges();

        var sample6 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step6 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample6);
        _dbContext.SaveChanges();

        var sample7 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<Step> { step7 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample7);
        _dbContext.SaveChanges();

        var sample8 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<Step> { step8 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample8);
        _dbContext.SaveChanges();

        var sample9 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<Step> { step9 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample9);
        _dbContext.SaveChanges();
        var sample10 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample10);
        _dbContext.SaveChanges();
        var sample11 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample11);_dbContext.SaveChanges();
        _dbContext.SaveChanges();
        var sample12 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample12);_dbContext.SaveChanges();
        _dbContext.SaveChanges();
        var sample13 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample13);_dbContext.SaveChanges();
        _dbContext.SaveChanges();
        var sample14 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample14);_dbContext.SaveChanges();
        _dbContext.SaveChanges();
        var sample15 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample15);_dbContext.SaveChanges();
        var sample16 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample16);_dbContext.SaveChanges();
        var sample17 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample17);_dbContext.SaveChanges();
        var sample18 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample18);_dbContext.SaveChanges();
        var sample19 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample19);_dbContext.SaveChanges();
        var sample20 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample20);_dbContext.SaveChanges();
        var sample21 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample21);_dbContext.SaveChanges();
        var sample22 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample22);_dbContext.SaveChanges();
        var sample23 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample23);_dbContext.SaveChanges();
        var sample24 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample24);_dbContext.SaveChanges();
        var sample25 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample25);_dbContext.SaveChanges();
        var sample26 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample26);_dbContext.SaveChanges();
        var sample27 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample27);_dbContext.SaveChanges();
        var sample28 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample28);_dbContext.SaveChanges();
        var sample29 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample29);_dbContext.SaveChanges();
        var sample30 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample30);_dbContext.SaveChanges();
        var sample31 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample31);_dbContext.SaveChanges();
        var sample32 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample32);_dbContext.SaveChanges();
        var sample33 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample33);_dbContext.SaveChanges();
        var sample34 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample34);_dbContext.SaveChanges();
        var sample35 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample35);_dbContext.SaveChanges();
        var sample36 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample36);_dbContext.SaveChanges();
        var sample37 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample37);_dbContext.SaveChanges();
        var sample38 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample38);_dbContext.SaveChanges();
        var sample39 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample39);_dbContext.SaveChanges();
        var sample40 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample40);_dbContext.SaveChanges();
        var sample41 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample41);_dbContext.SaveChanges();
        var sample42 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample42);_dbContext.SaveChanges();
        var sample43 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample43);_dbContext.SaveChanges();
        var sample44 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample44);
        _dbContext.SaveChanges();
        
        var sample45 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<Step> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        _dbContext.Samples.Add(sample45);
        _dbContext.SaveChanges();

        #endregion

        #region add
        
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