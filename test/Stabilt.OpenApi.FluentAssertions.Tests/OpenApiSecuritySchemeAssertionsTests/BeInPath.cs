namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeInPath
{
    [Fact]
    public void When_location_is_Path_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(ParameterLocation.Path);

        var act = () => securityScheme.Should().BeInPath();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Query)]
    public void When_location_is_not_Path_Does_not_throw_exception(ParameterLocation location)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(location);

        var act = () => securityScheme.Should().BeInPath("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to be in "Path" because reasons, but found "{location}".""");
    }
}
