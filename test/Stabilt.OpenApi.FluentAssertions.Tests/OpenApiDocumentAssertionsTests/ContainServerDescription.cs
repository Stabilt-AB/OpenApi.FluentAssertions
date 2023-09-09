namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainServerDescription
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithServer(OpenApiServerHelper.Create());

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainServerDescription("Test server");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _document.Should().ContainServerDescription("Foo server", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have a server with description "Foo server" because reasons, but found {"Test server"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainServerDescription("Foo server", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to have a server with description "Foo server" because reasons, but found no servers.""");
    }
}
