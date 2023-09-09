namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveFormat
{
    [Fact]
    public void When_schema_has_format_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string", "uuid");

        var act = () => schema.Should().HaveFormat("uuid");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_format_is_wrong_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string", "uuid");

        var act = () => schema.Should().HaveFormat("date-time", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have format "date-time" because reasons, but found "uuid".""");
    }
}
