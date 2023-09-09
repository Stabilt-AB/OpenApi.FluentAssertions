namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOAuthFlowAssertionsTests;

public class ContainScopes
{
    private readonly OpenApiOAuthFlow _oauthFlow = OpenApiOAuthFlowHelper.Create()
        .WithScopes("scope1", "scope2", "scope3");

    [Theory]
    [InlineData("scope1")]
    [InlineData("scope1", "scope2")]
    [InlineData("scope1", "scope2", "scope3")]
    [InlineData("scope1", "scope3")]
    [InlineData("scope1", "scope3", "scope2")]
    [InlineData("scope2")]
    [InlineData("scope2", "scope1")]
    [InlineData("scope2", "scope1", "scope3")]
    [InlineData("scope2", "scope3")]
    [InlineData("scope2", "scope3", "scope1")]
    [InlineData("scope3")]
    [InlineData("scope3", "scope1")]
    [InlineData("scope3", "scope1", "scope2")]
    [InlineData("scope3", "scope2")]
    [InlineData("scope3", "scope2", "scope1")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _oauthFlow.Should().ContainScopes(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _oauthFlow.Should().ContainScopes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _oauthFlow.Should().ContainScopes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _oauthFlow to contain scopes {"foo", "bar"} because reasons, but found {"scope1", "scope2", "scope3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _oauthFlow.Should().ContainScopes(["foo", "scope1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _oauthFlow to contain scopes {"foo", "scope1"} because reasons, but found {"scope1", "scope2", "scope3"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var oauthFlow = OpenApiOAuthFlowHelper.Create();

        var act = () => oauthFlow.Should().ContainScopes(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected oauthFlow to contain scopes {"foo", "bar"} because reasons, but found no scopes.""");
    }
}
