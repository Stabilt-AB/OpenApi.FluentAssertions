namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeInt32Type
{
    [Fact]
    public void When_type_and_format_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("integer", "int32");

        var act = () => schema.Should().BeInt32Type();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_format_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("integer");

        var act = () => schema.Should().BeInt32Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "integer" with format "int32" because reasons, but found format <null>.""");
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeInt32Type("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "integer" with format "int32" because reasons, but found type "string".""");
    }
}
