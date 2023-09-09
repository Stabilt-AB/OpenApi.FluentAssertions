namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class OnlyContainSchemas
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSchemaComponent("Item")
        .WithSchemaComponent("Another");

    [Theory]
    [InlineData("Item", "Another")]
    [InlineData("Another", "Item")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _document.Should().OnlyContainSchemas(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _document.Should().OnlyContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to only contain schemas {"Foo", "Bar"} because reasons, but found {"Item", "Another"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSchemas([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().OnlyContainSchemas(["Item", "Another"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to only contain schemas {"Item", "Another"} because reasons, but found no schemas.""");
    }
}
