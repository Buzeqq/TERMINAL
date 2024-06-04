using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.ValueObjects;
using Terminal.Backend.Infrastructure.DAL;

namespace Terminal.Backend.Infrastructure;

internal sealed class TerminalDbSeeder(TerminalDbContext dbContext)
{
    public void Seed()
    {
        #region tags

        var tag1 = new Tag(TagId.Create(), "new-sample");
        var tag2 = new Tag(TagId.Create(), "methane-rich");
        var tag3 = new Tag(TagId.Create(), "popular-sample");
        var tag4 = new Tag(TagId.Create(), "hot");
        var tag5 = new Tag(TagId.Create(), "high-pressure");

        dbContext.Tags.AddRange(tag1, tag2, tag3, tag4, tag5);

        #endregion

        #region projects

        var projectUpturn = new Project(ProjectId.Create(), "Upturn");
        var projectBessy2 = new Project(ProjectId.Create(), "Bessy 2");
        var projectNitro = new Project(ProjectId.Create(), "Nitro");
        var projectNobelium = new Project(ProjectId.Create(), "Nobelium");

        dbContext.Projects.AddRange(projectUpturn, projectBessy2, projectNitro, projectNobelium);

        #endregion

        #region parameters

        var bcParameter = new IntegerParameter(ParameterId.Create(), "B/C", "ppm", 1);
        var hydrogenParameter = new IntegerParameter(ParameterId.Create(), "H\u2082", "sccm", 1);
        var methaneParameter = new IntegerParameter(ParameterId.Create(), "CH\u2084", "sccm", 1);
        var diboranParameter = new IntegerParameter(ParameterId.Create(), "B\u2082H\u2086", "sccm", 1);
        var nucleationParameter = new TextParameter(ParameterId.Create(), "Nucleation Method",
        [
            "spin-coating",
            "nucleation",
            "dip-coating",
            "without nucleation"
        ]);
        var temperatureParameter = new IntegerParameter(ParameterId.Create(), "Temperature", "C\u2070", 1);
        var pressureParameter = new IntegerParameter(ParameterId.Create(), "Pressure", "Torr", 1);
        var powerParameter = new IntegerParameter(ParameterId.Create(), "Pmw", "W", 1);
        var timeParameter = new DecimalParameter(ParameterId.Create(), "Time", "h", 0.1m);
        var substrateParameter = new TextParameter(ParameterId.Create(), "Substrate",
        [
            "silicon",
            "silicon dioxide",
            "glass",
            "tantalum"
        ]);
        var bufferParameter = new DecimalParameter(ParameterId.Create(), "Buffer", "h", 0.1m);
        var additionalGasesParameter = new TextParameter(ParameterId.Create(), "Additional gases",
            ["none", "nitrogen", "oxygen"], order: 12);
        var additionalGassesAmountParameter =
            new IntegerParameter(ParameterId.Create(), "Additional gases amount", "sccm", 1);
        additionalGassesAmountParameter.SetParent(additionalGasesParameter);
        dbContext.Parameters.AddRange(bufferParameter,
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
            additionalGasesParameter,
            additionalGassesAmountParameter);

        dbContext.Parameters.AddRange(bufferParameter,
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

        #region steps

        var step1 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step1.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step1.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step1.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));
        step1.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));

        var step2 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step2.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step2.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step2.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step2.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step3 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step3.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step3.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step3.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step3.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step4 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step4.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step4.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step4.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step4.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));


        var step5 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step5.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step5.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step5.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step5.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step6 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step6.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step6.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step6.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step6.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step7 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step7.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step7.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step7.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step7.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step8 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step8.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step8.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step8.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step8.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step9 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step9.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step9.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step9.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step9.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        var step10 = new SampleStep(StepId.Create(), new Comment("First step!"));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), bcParameter, 2000));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), hydrogenParameter, 300));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), methaneParameter, 100));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), diboranParameter, 240));
        step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), nucleationParameter,
            nucleationParameter.AllowedValues.First()));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), temperatureParameter, 800));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), pressureParameter, 20));
        step10.Parameters.Add(new IntegerParameterValue(ParameterValueId.Create(), powerParameter, 1300));
        step10.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), timeParameter, 2.3m));
        step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), substrateParameter,
            substrateParameter.AllowedValues.First()));
        step10.Parameters.Add(new DecimalParameterValue(ParameterValueId.Create(), bufferParameter, 0.0m));
        step10.Parameters.Add(new TextParameterValue(ParameterValueId.Create(), additionalGasesParameter,
            additionalGasesParameter.AllowedValues.First()));

        #endregion

        #region samples

        var sample1 = new Sample(
            SampleId.Create(),
            projectUpturn,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step1 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample1);
        dbContext.SaveChanges();

        var sample2 = new Sample(
            SampleId.Create(),
            projectUpturn,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step2 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample2);
        dbContext.SaveChanges();

        var sample3 = new Sample(
            SampleId.Create(),
            projectBessy2,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step3 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample3);
        dbContext.SaveChanges();

        var sample4 = new Sample(
            SampleId.Create(),
            projectBessy2,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step4 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample4);
        dbContext.SaveChanges();

        var sample5 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step5 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample5);
        dbContext.SaveChanges();

        var sample6 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step6 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample6);
        dbContext.SaveChanges();

        var sample7 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step7 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample7);
        dbContext.SaveChanges();

        var sample8 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step8 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample8);
        dbContext.SaveChanges();

        var sample9 = new Sample(
            SampleId.Create(),
            projectNobelium,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step9 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample9);
        dbContext.SaveChanges();

        var sample10 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample10);
        dbContext.SaveChanges();

        var sample11 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample11);
        dbContext.SaveChanges();

        var sample12 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample12);
        dbContext.SaveChanges();

        var sample13 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample13);
        dbContext.SaveChanges();

        var sample14 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample14);
        dbContext.SaveChanges();

        var sample15 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample15);
        dbContext.SaveChanges();

        var sample16 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample16);
        dbContext.SaveChanges();

        var sample17 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample17);
        dbContext.SaveChanges();

        var sample18 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample18);
        dbContext.SaveChanges();

        var sample19 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample19);
        dbContext.SaveChanges();

        var sample20 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample20);
        dbContext.SaveChanges();

        var sample21 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample21);
        dbContext.SaveChanges();

        var sample22 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample22);
        dbContext.SaveChanges();

        var sample23 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample23);
        dbContext.SaveChanges();

        var sample24 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample24);
        dbContext.SaveChanges();

        var sample25 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample25);
        dbContext.SaveChanges();

        var sample26 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample26);
        dbContext.SaveChanges();

        var sample27 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample27);
        dbContext.SaveChanges();

        var sample28 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample28);
        dbContext.SaveChanges();

        var sample29 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample29);
        dbContext.SaveChanges();

        var sample30 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample30);
        dbContext.SaveChanges();

        var sample31 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample31);
        dbContext.SaveChanges();

        var sample32 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample32);
        dbContext.SaveChanges();

        var sample33 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample33);
        dbContext.SaveChanges();

        var sample34 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample34);
        dbContext.SaveChanges();

        var sample35 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample35);
        dbContext.SaveChanges();

        var sample36 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample36);
        dbContext.SaveChanges();

        var sample37 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample37);
        dbContext.SaveChanges();

        var sample38 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample38);
        dbContext.SaveChanges();

        var sample39 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample39);
        dbContext.SaveChanges();

        var sample40 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample40);
        dbContext.SaveChanges();

        var sample41 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample41);
        dbContext.SaveChanges();

        var sample42 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample42);
        dbContext.SaveChanges();

        var sample43 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample43);
        dbContext.SaveChanges();

        var sample44 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample44);
        dbContext.SaveChanges();

        var sample45 = new Sample(
            SampleId.Create(),
            projectNitro,
            null,
            new Comment("First sample!"),
            new List<SampleStep> { step10 },
            new List<Tag> { tag1, tag3, tag5 });
        dbContext.Samples.Add(sample45);

        dbContext.SaveChanges();

        #endregion
    }
}