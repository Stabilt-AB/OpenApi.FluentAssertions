namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveLicenseName
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveLicenseName("Test license");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveLicenseName("Foo license", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have license name "Foo license" because reasons, but found "Test license".""");
    }
}
