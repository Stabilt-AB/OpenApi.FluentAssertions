namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOAuthFlowAssertionsTests;

public class HaveRefreshUrl
{
    private readonly OpenApiOAuthFlow _flow = OpenApiOAuthFlowHelper.Create()
        .WithRefreshUrl("https://refresh");

    [Fact]
    public void When_correct_as_string_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveRefreshUrl("https://refresh");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_string_Throws_exception()
    {
        var act = () => _flow.Should().HaveRefreshUrl("https://foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have refresh URL https://foo/ because reasons, but found https://refresh/.");
    }

    [Fact]
    public void When_correct_as_uri_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveRefreshUrl(new Uri("https://refresh"));

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_uri_Throws_exception()
    {
        var act = () => _flow.Should().HaveRefreshUrl(new Uri("https://foo"), "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have refresh URL https://foo/ because reasons, but found https://refresh/.");
    }
}
