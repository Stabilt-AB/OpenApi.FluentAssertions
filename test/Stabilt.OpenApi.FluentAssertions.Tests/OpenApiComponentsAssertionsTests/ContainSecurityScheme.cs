namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class ContainSecurityScheme
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        _components.WithSecurityScheme("bearer");

        var act = () => _components.Should().ContainSecurityScheme("bearer");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        _components.WithSecurityScheme("bearer");

        var act = () => _components.Should().ContainSecurityScheme("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain a security scheme "foo" because reasons, but found {"bearer"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _components.Should().ContainSecurityScheme("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain a security scheme "foo" because reasons, but found no security schemes.""");
    }
}
