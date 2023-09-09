namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class HaveSchemaCount
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSchemaComponent("Item")
        .WithSchemaComponent("Another");

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _document.Should().HaveSchemaCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _document.Should().HaveSchemaCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to have 3 schemas because reasons, but found 2: {"Item", "Another"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveSchemaCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().HaveSchemaCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected document to have 3 schemas because reasons, but found no schemas.");
    }
}
