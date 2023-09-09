namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeInQuery
{
    [Fact]
    public void When_location_is_query_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create(location: ParameterLocation.Query);

        var act = () => parameter.Should().BeInQuery();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path)]
    [InlineData(ParameterLocation.Cookie)]
    public void When_location_is_not_query_Throws_exception(ParameterLocation location)
    {
        var parameter = OpenApiParameterHelper.Create(location: location);

        var act = () => parameter.Should().BeInQuery("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected parameter to be in "Query" because reasons, but found "{location}".""");
    }
}
