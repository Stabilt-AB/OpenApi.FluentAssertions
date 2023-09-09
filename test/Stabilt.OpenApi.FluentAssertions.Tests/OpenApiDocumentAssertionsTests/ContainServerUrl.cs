namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainServerUrl
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithServer(OpenApiServerHelper.Create());

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainServerUrl("https://test.stabilt.dev");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainServerUrl("https://foo.stabilt.dev", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain a server with URL "https://foo.stabilt.dev" because reasons, but found {"https://test.stabilt.dev"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainServerUrl("https://foo.stabilt.dev", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain a server with URL "https://foo.stabilt.dev" because reasons, but found no servers.""");
    }
}
