namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveOAuth2ImplicitFlow
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateApiKeyScheme();

    [Fact]
    public void When_scheme_has_Implicit_flow_Does_not_throw_exception()
    {
        _securityScheme.WithOAuth2ImplicitFlow(OpenApiOAuthFlowHelper.Create());

        var act = () => _securityScheme.Should().HaveOAuth2ImplicitFlow("test");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_Flows_is_null_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveOAuth2ImplicitFlow("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OAuth2 implicit flow because reasons, but found no OAuth2 flows.");
    }

    [Fact]
    public void When_Implicit_flow_is_null_Throws_exception()
    {
        _securityScheme.WithOAuth2Flows();

        var act = () => _securityScheme.Should().HaveOAuth2ImplicitFlow("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _securityScheme to have OAuth2 implicit flow because reasons, but it hadn't.");
    }
}
