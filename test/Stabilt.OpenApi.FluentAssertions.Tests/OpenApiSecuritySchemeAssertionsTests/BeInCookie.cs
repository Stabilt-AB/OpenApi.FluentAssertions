namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeInCookie
{
    [Fact]
    public void When_location_is_Cookie_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(ParameterLocation.Cookie);

        var act = () => securityScheme.Should().BeInCookie();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path)]
    [InlineData(ParameterLocation.Query)]
    public void When_location_is_not_Cookie_Does_not_throw_exception(ParameterLocation location)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(location);

        var act = () => securityScheme.Should().BeInCookie("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to be in "Cookie" because reasons, but found "{location}".""");
    }
}
