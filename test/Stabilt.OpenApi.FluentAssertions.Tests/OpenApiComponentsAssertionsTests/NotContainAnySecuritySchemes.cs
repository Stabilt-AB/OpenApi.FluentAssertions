namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class NotContainAnySecuritySchemes
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().NotContainAnySecuritySchemes();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _components.WithSecurityScheme("bearer");

        var act = () => _components.Should().NotContainAnySecuritySchemes("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to not contain any security schemes because reasons, but found {"bearer"}.""");
    }
}
