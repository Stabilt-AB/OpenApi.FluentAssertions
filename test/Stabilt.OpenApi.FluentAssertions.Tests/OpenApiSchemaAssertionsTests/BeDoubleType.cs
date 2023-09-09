namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeDoubleType
{
    [Fact]
    public void When_type_and_format_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number", "double");

        var act = () => schema.Should().BeDoubleType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_format_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number");

        var act = () => schema.Should().BeDoubleType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "number" with format "double" because reasons, but found format <null>.""");
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeDoubleType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "number" with format "double" because reasons, but found type "string".""");
    }
}
