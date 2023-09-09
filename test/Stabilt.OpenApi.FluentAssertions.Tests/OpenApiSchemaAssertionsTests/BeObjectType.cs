namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeObjectType
{
    [Fact]
    public void When_type_is_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObjectReference("Item");

        var act = () => schema.Should().BeObjectType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_type_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeObjectType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "object" because reasons, but found "string".""");
    }
}
