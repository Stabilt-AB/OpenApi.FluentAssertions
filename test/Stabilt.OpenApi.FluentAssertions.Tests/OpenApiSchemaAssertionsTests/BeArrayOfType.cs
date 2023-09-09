namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeArrayOfType
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_schema_is_array_with_type_Does_not_throw_exception()
    {
        _schema.WithItems(OpenApiSchemaHelper.CreateType("string"));

        var act = () => _schema.Should().BeArrayOfType("string");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_array_of_wrong_type_Throws_exception()
    {
        _schema.WithItems(OpenApiSchemaHelper.CreateType("string"));

        var act = () => _schema.Should().BeArrayOfType("number", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to be an array of "number" because reasons, but found an array of "string" types.""");
    }

    [Fact]
    public void When_items_is_null_Throws_exception()
    {
        var act = () => _schema.Should().BeArrayOfType("number", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to be an array of "number" because reasons, but found an array with undefined items.""");
    }

    [Fact]
    public void When_schema_is_non_array_type_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number");

        var act = () => schema.Should().BeArrayOfType("string", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be an array of "string" because reasons, but found "number" instead of an array.""");
    }
}
