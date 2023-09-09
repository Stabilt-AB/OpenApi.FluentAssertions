namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveEmptyFormat
{
    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().HaveEmptyFormat();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string", "uuid");

        var act = () => schema.Should().HaveEmptyFormat("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have empty format because reasons, but found "uuid".""");
    }
}
