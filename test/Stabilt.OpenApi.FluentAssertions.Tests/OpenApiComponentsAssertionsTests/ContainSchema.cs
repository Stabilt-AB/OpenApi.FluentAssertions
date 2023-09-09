namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class ContainSchema
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        _components.WithSchema("Item");

        var act = () => _components.Should().ContainSchema("Item");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        _components.WithSchema("Item");

        var act = () => _components.Should().ContainSchema("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain a schema "Foo" because reasons, but found {"Item"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _components.Should().ContainSchema("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain a schema "Foo" because reasons, but found no schemas.""");
    }
}
