namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class ContainSecuritySchemes
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create()
        .WithSecurityScheme("scheme1")
        .WithSecurityScheme("scheme2")
        .WithSecurityScheme("scheme3");

    [Theory]
    [InlineData("scheme1")]
    [InlineData("scheme1", "scheme2")]
    [InlineData("scheme1", "scheme2", "scheme3")]
    [InlineData("scheme1", "scheme3")]
    [InlineData("scheme1", "scheme3", "scheme2")]
    [InlineData("scheme2")]
    [InlineData("scheme2", "scheme1")]
    [InlineData("scheme2", "scheme1", "scheme3")]
    [InlineData("scheme2", "scheme3")]
    [InlineData("scheme2", "scheme3", "scheme1")]
    [InlineData("scheme3")]
    [InlineData("scheme3", "scheme1")]
    [InlineData("scheme3", "scheme1", "scheme2")]
    [InlineData("scheme3", "scheme2")]
    [InlineData("scheme3", "scheme2", "scheme1")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _components.Should().ContainSecuritySchemes(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _components.Should().ContainSecuritySchemes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _components.Should().ContainSecuritySchemes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain security schemes {"foo", "bar"} because reasons, but found {"scheme1", "scheme2", "scheme3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _components.Should().ContainSecuritySchemes(["foo", "scheme1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to contain security schemes {"foo", "scheme1"} because reasons, but found {"scheme1", "scheme2", "scheme3"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var components = OpenApiComponentsHelper.Create();

        var act = () => components.Should().ContainSecuritySchemes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected components to contain security schemes {"foo", "bar"} because reasons, but found no security schemes.""");
    }
}
