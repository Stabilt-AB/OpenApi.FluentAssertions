namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class HaveSchemaCount
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        _components
            .WithSchema("Item1")
            .WithSchema("Item2");

        var act = () => _components.Should().HaveSchemaCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().HaveSchemaCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var act = () => _components.Should().HaveSchemaCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _components to have 3 schemas because reasons, but found no schemas.");
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        _components
            .WithSchema("Item1")
            .WithSchema("Item2");

        var act = () => _components.Should().HaveSchemaCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to have 3 schemas because reasons, but found 2: {"Item1", "Item2"}.""");
    }
}
