namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeInPath
{
    [Fact]
    public void When_location_is_path_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create(location: ParameterLocation.Path);

        var act = () => parameter.Should().BeInPath();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Query)]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Cookie)]
    public void When_location_is_not_path_Throws_exception(ParameterLocation location)
    {
        var parameter = OpenApiParameterHelper.Create(location: location);

        var act = () => parameter.Should().BeInPath("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected parameter to be in "Path" because reasons, but found "{location}".""");
    }
}
