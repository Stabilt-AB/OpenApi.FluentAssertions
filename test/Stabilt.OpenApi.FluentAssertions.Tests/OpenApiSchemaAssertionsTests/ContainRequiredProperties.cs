namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class ContainRequiredProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject()
        .WithProperty("id", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("name", OpenApiSchemaHelper.CreateType("string"), true)
        .WithProperty("age", OpenApiSchemaHelper.CreateType("integer", "int32"), true)
        .WithProperty("shoeSize", OpenApiSchemaHelper.CreateType("integer", "int32"));

    [Theory]
    [InlineData("age")]
    [InlineData("age", "id")]
    [InlineData("age", "id", "name")]
    [InlineData("age", "name")]
    [InlineData("age", "name", "id")]
    [InlineData("id")]
    [InlineData("id", "age")]
    [InlineData("id", "age", "name")]
    [InlineData("id", "name")]
    [InlineData("id", "name", "age")]
    [InlineData("name")]
    [InlineData("name", "age")]
    [InlineData("name", "age", "id")]
    [InlineData("name", "id")]
    [InlineData("name", "id", "age")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _schema.Should().ContainRequiredProperties(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _schema.Should().ContainRequiredProperties([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _schema.Should().ContainRequiredProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain required properties {"foo", "bar"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_not_contain_some_Throws_exception()
    {
        var act = () => _schema.Should().ContainRequiredProperties(["foo", "id"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain required properties {"foo", "id"} because reasons, but found {"id", "name", "age"}.""");
    }

    [Fact]
    public void When_no_properties_exists_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().ContainRequiredProperties(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to contain required properties {"foo", "bar"} because reasons, but found no required properties.""");
    }

    [Fact]
    public void When_no_properties_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().ContainRequiredProperties([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
