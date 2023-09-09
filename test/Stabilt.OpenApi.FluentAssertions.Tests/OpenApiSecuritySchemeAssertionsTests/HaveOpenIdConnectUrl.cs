namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveOpenIdConnectUrl
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateOpenIdConnectScheme("https://oidc");

    [Fact]
    public void With_string_When_scheme_has_OpenIdConnectUrl_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveOpenIdConnectUrl("https://oidc");

        act.Should().NotThrow();
    }

    [Fact]
    public void With_string_When_url_is_wrong_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveOpenIdConnectUrl("https://foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OpenID Connect URL https://foo/ because reasons, but found https://oidc/.");
    }

    [Fact]
    public void With_url_When_scheme_has_OpenIdConnectUrl_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveOpenIdConnectUrl(new Uri("https://oidc"));

        act.Should().NotThrow();
    }

    [Fact]
    public void With_url_When_url_is_wrong_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveOpenIdConnectUrl(new Uri("https://foo"), "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OpenID Connect URL https://foo/ because reasons, but found https://oidc/.");
    }
}
