namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class ContainSchemas
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create()
        .WithSchema("Schema1")
        .WithSchema("Schema2")
        .WithSchema("Schema3");

    [Theory]
    [InlineData("Schema1")]
    [InlineData("Schema1", "Schema2")]
    [InlineData("Schema1", "Schema2", "Schema3")]
    [InlineData("Schema1", "Schema3")]
    [InlineData("Schema1", "Schema3", "Schema2")]
    [InlineData("Schema2")]
    [InlineData("Schema2", "Schema1")]
    [InlineData("Schema2", "Schema1", "Schema3")]
    [InlineData("Schema2", "Schema3")]
    [InlineData("Schema2", "Schema3", "Schema1")]
    [InlineData("Schema3")]
    [InlineData("Schema3", "Schema1")]
    [InlineData("Schema3", "Schema1", "Schema2")]
    [InlineData("Schema3", "Schema2")]
    [InlineData("Schema3", "Schema2", "Schema1")]
    public void When_contains_all_Does_not_throw_exception(params String[] schemas)
    {
        var act = () => _components.Should().ContainSchemas(schemas);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _components.Should().ContainSchemas([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _components.Should().ContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain schemas {"Foo", "Bar"} because reasons, but found {"Schema1", "Schema2", "Schema3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _components.Should().ContainSchemas(["Foo", "Schema1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain schemas {"Foo", "Schema1"} because reasons, but found {"Schema1", "Schema2", "Schema3"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var components = OpenApiComponentsHelper.Create();

        var act = () => components.Should().ContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected components to contain schemas {"Foo", "Bar"} because reasons, but found no schemas.""");
    }
}
