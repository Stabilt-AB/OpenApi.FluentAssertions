namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class ContainProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject()
        .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
        .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"));

    [Theory]
    [InlineData("age")]
    [InlineData("age", "name")]
    [InlineData("age", "id")]
    [InlineData("age", "name", "id")]
    [InlineData("age", "id", "name")]
    [InlineData("id")]
    [InlineData("id", "age")]
    [InlineData("id", "name")]
    [InlineData("id", "age", "name")]
    [InlineData("id", "name", "age")]
    [InlineData("name")]
    [InlineData("name", "id")]
    [InlineData("name", "age")]
    [InlineData("name", "id", "age")]
    [InlineData("name", "age", "id")]
    public void When_contains_all_Does_not_throw_exception(params string[] expectedValues)
    {
        var act = () => _schema.Should().ContainProperties(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _schema.Should().ContainProperties([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _schema.Should().ContainProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain properties {"foo", "bar"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_not_contain_some_Throws_exception()
    {
        var act = () => _schema.Should().ContainProperties(["foo", "name"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain properties {"foo", "name"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_no_properties_exists_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().ContainProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to contain properties {"foo", "bar"} because reasons, but found no properties.""");
    }

    [Fact]
    public void When_no_properties_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().ContainProperties([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
