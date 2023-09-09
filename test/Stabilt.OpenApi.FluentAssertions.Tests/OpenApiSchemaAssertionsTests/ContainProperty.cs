namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class ContainProperty
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject()
        .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
        .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"));

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _schema.Should().ContainProperty("id");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _schema.Should().ContainProperty("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain a property "foo" because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().ContainProperty("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to contain a property "foo" because reasons, but found no properties.""");
    }
}
