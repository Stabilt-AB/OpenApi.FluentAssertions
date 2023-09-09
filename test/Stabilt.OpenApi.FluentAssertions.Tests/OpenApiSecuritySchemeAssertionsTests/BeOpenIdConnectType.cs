namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeOpenIdConnectType
{
    [Fact]
    public void When_type_is_OpenIdConnect_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.CreateOpenIdConnectScheme();

        var act = () => securityScheme.Should().BeOpenIdConnectType();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OAuth2)]
    public void When_type_is_not_OpenIdConnect_Does_not_throw_exception(SecuritySchemeType type)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(type);

        var act = () => securityScheme.Should().BeOpenIdConnectType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to have type "OpenIdConnect" because reasons, but found "{type}".""");
    }
}
