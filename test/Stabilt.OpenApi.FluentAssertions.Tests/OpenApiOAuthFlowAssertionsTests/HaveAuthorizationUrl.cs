namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOAuthFlowAssertionsTests;

public class HaveAuthorizationUrl
{
    private readonly OpenApiOAuthFlow _flow = OpenApiOAuthFlowHelper.Create()
        .WithAuthorizationUrl("https://authorization");

    [Fact]
    public void When_correct_as_string_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveAuthorizationUrl("https://authorization");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_string_Throws_exception()
    {
        var act = () => _flow.Should().HaveAuthorizationUrl("https://foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have authorization URL https://foo/ because reasons, but found https://authorization/.");
    }

    [Fact]
    public void When_correct_as_uri_Does_not_throw_exception()
    {
        var act = () => _flow.Should().HaveAuthorizationUrl(new Uri("https://authorization"));

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_uri_Throws_exception()
    {
        var act = () => _flow.Should().HaveAuthorizationUrl(new Uri("https://foo"), "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _flow to have authorization URL https://foo/ because reasons, but found https://authorization/.");
    }
}
