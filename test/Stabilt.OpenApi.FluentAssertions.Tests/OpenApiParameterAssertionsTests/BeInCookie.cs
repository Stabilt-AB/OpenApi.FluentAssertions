namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeInCookie
{
    [Fact]
    public void When_location_is_cookie_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create(location: ParameterLocation.Cookie);

        var act = () => parameter.Should().BeInCookie();

        act.Should().NotThrow();
    }

    [Theory]
    [InlineData(ParameterLocation.Query)]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Path)]
    public void When_location_is_not_cookie_Throws_exception(ParameterLocation location)
    {
        var parameter = OpenApiParameterHelper.Create(location: location);

        var act = () => parameter.Should().BeInCookie("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage($"""Expected parameter to be in "Cookie" because reasons, but found "{location}".""");
    }
}
