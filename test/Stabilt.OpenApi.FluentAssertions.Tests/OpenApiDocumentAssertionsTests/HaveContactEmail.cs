namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveContactEmail
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveContactEmail("contact@test.stabilt.dev");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveContactEmail("foo@test.stabilt.dev", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have contact email "foo@test.stabilt.dev" because reasons, but found "contact@test.stabilt.dev".""");
    }
}
