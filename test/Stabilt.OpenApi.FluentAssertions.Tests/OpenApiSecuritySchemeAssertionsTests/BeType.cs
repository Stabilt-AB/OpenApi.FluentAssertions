namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeType
{
    [Theory]
    [InlineData(SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OAuth2)]
    [InlineData(SecuritySchemeType.OpenIdConnect)]
    public void When_type_is_as_expected_Does_not_throw_exception(SecuritySchemeType type)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(type);

        var act = () => securityScheme.Should().BeType(type);

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(SecuritySchemeType.ApiKey, SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.ApiKey, SecuritySchemeType.OAuth2)]
    [InlineData(SecuritySchemeType.ApiKey, SecuritySchemeType.OpenIdConnect)]
    [InlineData(SecuritySchemeType.Http, SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.Http, SecuritySchemeType.OAuth2)]
    [InlineData(SecuritySchemeType.Http, SecuritySchemeType.OpenIdConnect)]
    [InlineData(SecuritySchemeType.OAuth2, SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.OAuth2, SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OAuth2, SecuritySchemeType.OpenIdConnect)]
    [InlineData(SecuritySchemeType.OpenIdConnect, SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.OpenIdConnect, SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OpenIdConnect, SecuritySchemeType.OAuth2)]
    public void When_type_is_not_as_expected_Does_not_throw_exception(SecuritySchemeType expected, SecuritySchemeType actual)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(actual);

        var act = () => securityScheme.Should().BeType(expected, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to have type "{expected}" because reasons, but found "{actual}".""");
    }
}
