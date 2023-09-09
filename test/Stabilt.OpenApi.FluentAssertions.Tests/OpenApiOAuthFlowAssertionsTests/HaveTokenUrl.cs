namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOAuthFlowAssertionsTests;

public class HaveTokenUrl
{
    private readonly OpenApiOAuthFlow _flow = OpenApiOAuthFlowHelper.Create()
        .WithTokenUrl("https://token");

    [Fact]
    public void When_correct_as_string_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveTokenUrl("https://token");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_string_Throws_exception()
    {
        var act = () => _flow.Should().HaveTokenUrl("https://foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have token URL https://foo/ because reasons, but found https://token/.");
    }

    [Fact]
    public void When_correct_as_uri_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveTokenUrl(new Uri("https://token"));

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_uri_Throws_exception()
    {
        var act = () => _flow.Should().HaveTokenUrl(new Uri("https://foo"), "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have token URL https://foo/ because reasons, but found https://token/.");
    }
}
