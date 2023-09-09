namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeOAuth2Type
{
    [Fact]
    public void When_type_is_OAuth2_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.CreateOAuth2ImplicitScheme();

        var act = () => securityScheme.Should().BeOAuth2Type();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OpenIdConnect)]
    public void When_type_is_not_OAuth2_Does_not_throw_exception(SecuritySchemeType type)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(type);

        var act = () => securityScheme.Should().BeOAuth2Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to have type "OAuth2" because reasons, but found "{type}".""");
    }
}
