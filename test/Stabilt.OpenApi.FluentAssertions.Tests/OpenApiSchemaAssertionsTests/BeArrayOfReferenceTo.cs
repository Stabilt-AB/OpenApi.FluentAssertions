namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeArrayOfReferenceTo
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_schema_is_array_with_items_reference_Does_not_throw_exception()
    {
        _schema.WithItems(OpenApiSchemaHelper.CreateObjectReference("Item"));

        var act = () => _schema.Should().BeArrayOfReferenceTo("Item");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_array_of_wrong_reference_Throws_exception()
    {
        _schema.WithItems(OpenApiSchemaHelper.CreateObjectReference("Item"));

        var act = () => _schema.Should().BeArrayOfReferenceTo("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to be an array of references to "Foo" because reasons, but found an array of references to "Item".""");
    }

    [Fact]
    public void When_items_is_null_Throws_exception()
    {
        var act = () => _schema.Should().BeArrayOfReferenceTo("Item", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to be an array of references to "Item" because reasons, but found an array with undefined items.""");
    }

    [Fact]
    public void When_type_instead_of_reference_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeArrayOfReferenceTo("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be an array of references to "Foo" because reasons, but found type "string".""");
    }
}
