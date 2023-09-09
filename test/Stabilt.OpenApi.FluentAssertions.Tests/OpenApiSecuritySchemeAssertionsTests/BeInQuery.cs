namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class BeInQuery
{
    [Fact]
    public void When_location_is_Query_Does_not_throw_exception()
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(ParameterLocation.Query);

        var act = () => securityScheme.Should().BeInQuery();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path)]
    public void When_location_is_not_Query_Does_not_throw_exception(ParameterLocation location)
    {
        var securityScheme = OpenApiSecuritySchemeHelper.Create().WithLocation(location);

        var act = () => securityScheme.Should().BeInQuery("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected securityScheme to be in "Query" because reasons, but found "{location}".""");
    }
}
