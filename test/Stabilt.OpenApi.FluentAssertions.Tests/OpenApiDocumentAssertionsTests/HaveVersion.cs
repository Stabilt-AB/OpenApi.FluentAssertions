namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveVersion
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveVersion("1.33.7");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveVersion("2.44.8", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have version "2.44.8" because reasons, but found "1.33.7".""");
    }
}
