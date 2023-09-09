namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HavePropertyCount
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject()
        .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
        .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"));

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _schema.Should().HavePropertyCount(3);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_wrong_Throws_exception()
    {
        var act = () => _schema.Should().HavePropertyCount(4, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to have 4 properties because reasons, but found 3: {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_no_properties_exists_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().HavePropertyCount(4, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected schema to have 4 properties because reasons, but found no properties.");
    }
}
