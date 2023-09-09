namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeApiKeyType
{
    [Fact]
    public void When_type_is_ApiKey_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.CreateApiKeyScheme();

        var act = () => securityScheme.Should().BeApiKeyType();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(SecuritySchemeType.Http)]
    [InlineData(SecuritySchemeType.OAuth2)]
    [InlineData(SecuritySchemeType.OpenIdConnect)]
    public void When_type_is_not_ApiKey_Does_not_throw_exception(SecuritySchemeType type)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(type);

        var act = () => securityScheme.Should().BeApiKeyType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to have type "ApiKey" because reasons, but found "{type}".""");
    }
}
