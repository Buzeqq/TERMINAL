using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace Terminal.Backend.Architecture;

public class ArchitectureTests
{
    private const string CoreNamespace = "Core";
    private const string ApplicationNamespace = "Application";
    private const string InfrastructureNamespace = "Infrastructure";
    private const string ApiNamespace = "Api";

    [Fact]
    public void Core_Should_Not_Have_Dependency_On_Other_Projects()
    {
        var assembly = typeof(Core.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApplicationNamespace,
            InfrastructureNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Have_Dependency_On_Other_Projects()
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNamespace,
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Have_Dependency_On_Other_Projects()
    {
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            ApiNamespace
        };

        var result = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
