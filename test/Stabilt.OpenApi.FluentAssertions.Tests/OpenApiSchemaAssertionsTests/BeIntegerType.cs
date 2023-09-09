namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeIntegerType
{
    [Fact]
    public void When_type_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("integer");

        var act = () => schema.Should().BeIntegerType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeIntegerType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "integer" because reasons, but found "string".""");
    }
}
