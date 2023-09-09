using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiResponseAssertionsTests;

public class NotContainAnyContent
{
    private readonly OpenApiResponse _response = OpenApiResponseHelper.Create();

    [Fact]
    public void When_not_contains_Does_not_throw_exception()
    {
        var act = () => _response.Should().NotContainAnyContent();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_contains_Throws_exception()
    {
        _response.WithContent(MediaTypeNames.Application.Json);

        var act = () => _response.Should().NotContainAnyContent("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _response to not contain any response content because reasons, but found {"application/json"}.""");
    }
}
