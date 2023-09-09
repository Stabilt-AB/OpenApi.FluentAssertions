namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeInHeader
{
    [Fact]
    public void When_location_is_header_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create(location: ParameterLocation.Header);

        var act = () => parameter.Should().BeInHeader();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Query)]
    [InlineData(ParameterLocation.Path)]
    [InlineData(ParameterLocation.Cookie)]
    public void When_location_is_not_header_Throws_exception(ParameterLocation location)
    {
        var parameter = OpenApiParameterHelper.Create(location: location);

        var act = () => parameter.Should().BeInHeader("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected parameter to be in "Header" because reasons, but found "{location}".""");
    }
}
