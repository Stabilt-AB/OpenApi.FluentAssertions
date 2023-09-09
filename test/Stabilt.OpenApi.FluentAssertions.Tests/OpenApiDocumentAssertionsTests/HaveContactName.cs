namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveContactName
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveContactName("Test Contact");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveContactName("Foo Contact", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have contact name "Foo Contact" because reasons, but found "Test Contact".""");
    }
}
