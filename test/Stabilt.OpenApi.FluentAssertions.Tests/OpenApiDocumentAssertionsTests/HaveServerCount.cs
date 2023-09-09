namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveServerCount
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithServer(OpenApiServerHelper.Create());

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveServerCount(1);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveServerCount(2, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have 2 servers because reasons, but found 1: {"https://test.stabilt.dev"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveServerCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveServerCount(2, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected document to have 2 servers because reasons, but found no servers.");
    }
}
