namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class OnlyContainSchemas
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Theory]
    [InlineData("Item1", "Item2")]
    [InlineData("Item2", "Item1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] securitySchemes)
    {
        _components
            .WithSchema("Item1")
            .WithSchema("Item2");

        var act = () => _components.Should().OnlyContainSchemas(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().OnlyContainSchemas([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var act = () => _components.Should().OnlyContainSchemas(["Item1", "Item2"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to only contain schemas {"Item1", "Item2"} because reasons, but found no schemas.""");
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        _components
            .WithSchema("Item1")
            .WithSchema("Item2");

        var act = () => _components.Should().OnlyContainSchemas(["Item1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to only contain schemas {"Item1"} because reasons, but found {"Item1", "Item2"}.""");
    }
}
