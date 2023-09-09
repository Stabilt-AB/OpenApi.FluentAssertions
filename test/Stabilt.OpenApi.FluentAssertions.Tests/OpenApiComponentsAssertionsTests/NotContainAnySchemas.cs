namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class NotContainAnySchemas
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().NotContainAnySchemas();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _components.WithSchema("Schema");

        var act = () => _components.Should().NotContainAnySchemas("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to not contain any schemas because reasons, but found {"Schema"}.""");
    }
}
