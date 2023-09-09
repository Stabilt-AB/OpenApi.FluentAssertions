namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiDocumentAssertionsTests;

public class ContainSchemas
{
    private readonly OpenApiDocument _document = OpenApiDocumentHelper.Create()
        .WithSchemaComponent("Schema1")
        .WithSchemaComponent("Schema2")
        .WithSchemaComponent("Schema3");

    [Theory]
    [InlineData("Schema1")]
    [InlineData("Schema1", "Schema2")]
    [InlineData("Schema1", "Schema2", "Schema3")]
    [InlineData("Schema1", "Schema3")]
    [InlineData("Schema1", "Schema3", "Schema2")]
    [InlineData("Schema2")]
    [InlineData("Schema2", "Schema1")]
    [InlineData("Schema2", "Schema1", "Schema3")]
    [InlineData("Schema2", "Schema3")]
    [InlineData("Schema2", "Schema3", "Schema1")]
    [InlineData("Schema3")]
    [InlineData("Schema3", "Schema1")]
    [InlineData("Schema3", "Schema1", "Schema2")]
    [InlineData("Schema3", "Schema2")]
    [InlineData("Schema3", "Schema2", "Schema1")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _document.Should().ContainSchemas(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _document.Should().ContainSchemas([]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _document.Should().ContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain schemas {"Foo", "Bar"} because reasons, but found {"Schema1", "Schema2", "Schema3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _document.Should().ContainSchemas(["Foo", "Schema1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _document to contain schemas {"Foo", "Schema1"} because reasons, but found {"Schema1", "Schema2", "Schema3"}.""");
    }

    [Fact]
    public void When_components_is_null_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create();

        var act = () => document.Should().ContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain schemas {"Foo", "Bar"} because reasons, but found no schemas.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var document = OpenApiDocumentHelper.Create()
            .WithComponents();

        var act = () => document.Should().ContainSchemas(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected document to contain schemas {"Foo", "Bar"} because reasons, but found no schemas.""");
    }
}
