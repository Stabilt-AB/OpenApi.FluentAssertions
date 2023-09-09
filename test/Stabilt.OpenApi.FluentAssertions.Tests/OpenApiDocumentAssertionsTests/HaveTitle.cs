namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveTitle
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveTitle("Test API");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveTitle("This is wrong", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have title "This is wrong" because reasons, but found "Test API".""");
    }
}
