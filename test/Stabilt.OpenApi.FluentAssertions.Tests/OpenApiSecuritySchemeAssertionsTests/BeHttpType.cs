namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeHttpType
{
    [Fact]
    public void When_type_is_Http_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.CreateJwtBearerScheme();

        var act = () => securityScheme.Should().BeHttpType();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(SecuritySchemeType.ApiKey)]
    [InlineData(SecuritySchemeType.OAuth2)]
    [InlineData(SecuritySchemeType.OpenIdConnect)]
    public void When_type_is_not_Http_Does_not_throw_exception(SecuritySchemeType type)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create(type);

        var act = () => securityScheme.Should().BeHttpType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to have type "Http" because reasons, but found "{type}".""");
    }
}
