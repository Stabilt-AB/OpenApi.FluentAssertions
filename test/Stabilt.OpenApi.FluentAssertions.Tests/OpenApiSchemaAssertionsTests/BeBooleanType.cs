namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeBooleanType
{
    [Fact]
    public void When_type_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("boolean");

        var act = () => schema.Should().BeBooleanType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("integer");

        var act = () => schema.Should().BeBooleanType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "boolean" because reasons, but found "integer".""");
    }
}
