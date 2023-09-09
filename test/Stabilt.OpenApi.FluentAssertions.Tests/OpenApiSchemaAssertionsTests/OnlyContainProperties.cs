namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class OnlyContainProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject()
        .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
        .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"));

    [Theory]
    [InlineData("age", "name", "id")]
    [InlineData("age", "id", "name")]
    [InlineData("id", "age", "name")]
    [InlineData("id", "name", "age")]
    [InlineData("name", "id", "age")]
    [InlineData("name", "age", "id")]
    public void When_contains_all_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _schema.Should().OnlyContainProperties(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _schema.Should().OnlyContainProperties(["name"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only contain properties {"name"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _schema.Should().OnlyContainProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only contain properties {"foo", "bar"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_no_properties_exists_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().OnlyContainProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to only contain properties {"foo", "bar"} because reasons, but found no properties.""");
    }

    [Fact]
    public void When_no_properties_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().OnlyContainProperties([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
