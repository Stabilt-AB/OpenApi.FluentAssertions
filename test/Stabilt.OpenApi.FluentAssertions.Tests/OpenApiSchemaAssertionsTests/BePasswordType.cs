namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BePasswordType
{
    [Fact]
    public void When_type_and_format_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string", "password");

        var act = () => schema.Should().BePasswordType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_format_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BePasswordType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with format "password" because reasons, but found format <null>.""");
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number");

        var act = () => schema.Should().BePasswordType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with format "password" because reasons, but found type "number".""");
    }
}
