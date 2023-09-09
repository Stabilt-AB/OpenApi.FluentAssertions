namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiServerAssertionsTests;

public class HaveUrl
{
    private readonly OpenApiServer _server = OpenApiServerHelper.Create(url: "https://test.stabilt.dev");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _server.Should().HaveUrl("https://test.stabilt.dev");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _server.Should().HaveUrl("https://foo.stabilt.dev", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _server to have URL "https://foo.stabilt.dev" because reasons, but found "https://test.stabilt.dev".""");
    }
}
