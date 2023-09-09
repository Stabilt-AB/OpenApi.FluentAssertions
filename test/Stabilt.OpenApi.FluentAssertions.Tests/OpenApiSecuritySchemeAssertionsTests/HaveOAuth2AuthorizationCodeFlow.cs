namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveOAuth2AuthorizationCodeFlow
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateApiKeyScheme();

    [Fact]
    public void When_scheme_has_AuthorizationCode_flow_Does_not_throw_exception()
    {
        _securityScheme.WithOAuth2AuthorizationCodeFlow(OpenApiOAuthFlowHelper.Create());

        var act = () => _securityScheme.Should().HaveOAuth2AuthorizationCodeFlow("test");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_Flows_is_null_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveOAuth2AuthorizationCodeFlow("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OAuth2 authorization code flow because reasons, but found no OAuth2 flows.");
    }

    [Fact]
    public void When_AuthorizationCode_flow_is_null_Throws_exception()
    {
        _securityScheme.WithOAuth2Flows();

        var act = () => _securityScheme.Should().HaveOAuth2AuthorizationCodeFlow("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OAuth2 authorization code flow because reasons, but it hadn't.");
    }
}
