namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeInHeader
{
    [Fact]
    public void When_location_is_Header_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(ParameterLocation.Header);

        var act = () => securityScheme.Should().BeInHeader();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Path)]
    [InlineData(ParameterLocation.Query)]
    public void When_location_is_not_Header_Does_not_throw_exception(ParameterLocation location)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(location);

        var act = () => securityScheme.Should().BeInHeader("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to be in "Header" because reasons, but found "{location}".""");
    }
}
