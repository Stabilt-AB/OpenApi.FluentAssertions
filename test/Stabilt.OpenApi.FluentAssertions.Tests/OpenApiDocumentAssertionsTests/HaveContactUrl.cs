namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveContactUrl
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create();

    [Fact]
    public void When_correct_as_string_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveContactUrl("https://test.stabilt.dev/contact");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_string_Throws_exception()
    {
        var act = () => _document.Should().HaveContactUrl("https://test.stabilt.dev/foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _document to have contact URL https://test.stabilt.dev/foo because reasons, but found https://test.stabilt.dev/contact.");
    }

    [Fact]
    public void When_correct_as_uri_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveContactUrl(new Uri("https://test.stabilt.dev/contact"));

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_as_uri_Throws_exception()
    {
        var act = () => _document.Should().HaveContactUrl(new Uri("https://test.stabilt.dev/foo"), "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _document to have contact URL https://test.stabilt.dev/foo because reasons, but found https://test.stabilt.dev/contact.");
    }
}
