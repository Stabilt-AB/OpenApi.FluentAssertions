using System.Net.Mime;

namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiResponseAssertionsTests;

public class ContainContent
{
    private readonly OpenApiResponse _response = OpenApiResponseHelper.Create()
        .WithContent(MediaTypeNames.Application.Json);

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _response.Should().ContainContent("application/json");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _response.Should().ContainContent("text/html", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _response to contain response content type "text/html" because reasons, but found {"application/json"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var response = OpenApiResponseHelper.Create();

        var act = () => response.Should().ContainContent("text/html", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected response to contain response content type "text/html" because reasons, but it didn't have any content.""");
    }
}
