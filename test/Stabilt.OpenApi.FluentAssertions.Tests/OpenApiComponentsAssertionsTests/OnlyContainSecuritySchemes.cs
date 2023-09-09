namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class OnlyContainSecuritySchemes
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Theory]
    [InlineData("bearer-1", "bearer-2")]
    [InlineData("bearer-2", "bearer-1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] securitySchemes)
    {
        _components
            .WithSecurityScheme("bearer-1")
            .WithSecurityScheme("bearer-2");

        var act = () => _components.Should().OnlyContainSecuritySchemes(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().OnlyContainSecuritySchemes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var act = () => _components.Should().OnlyContainSecuritySchemes(["bearer-1", "bearer-2"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to only contain security schemes {"bearer-1", "bearer-2"} because reasons, but found no security schemes.""");
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        _components
            .WithSecurityScheme("bearer-1")
            .WithSecurityScheme("bearer-2");

        var act = () => _components.Should().OnlyContainSecuritySchemes(["bearer-1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to only contain security schemes {"bearer-1"} because reasons, but found {"bearer-1", "bearer-2"}.""");
    }
}
