namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class ContainEnumValues
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateEnum("yes", "no", "maybe");

    [Theory]
    [InlineData("maybe")]
    [InlineData("maybe", "no")]
    [InlineData("maybe", "no", "yes")]
    [InlineData("maybe", "yes")]
    [InlineData("maybe", "yes", "no")]
    [InlineData("no")]
    [InlineData("no", "maybe")]
    [InlineData("no", "maybe", "yes")]
    [InlineData("no", "yes")]
    [InlineData("no", "yes", "maybe")]
    [InlineData("yes")]
    [InlineData("yes", "maybe")]
    [InlineData("yes", "maybe", "no")]
    [InlineData("yes", "no")]
    [InlineData("yes", "no", "maybe")]
    public void When_contains_all_Does_not_throw_exception(params String[] securitySchemes)
    {
        var act = () => _schema.Should().ContainEnumValues(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _schema.Should().ContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _schema.Should().ContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain enum values {"foo", "bar"} because reasons, but found {"yes", "no", "maybe"}.""");
    }

    [Fact]
    public void When_not_contain_some_Throws_exception()
    {
        var act = () => _schema.Should().ContainEnumValues(["foo", "yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to contain enum values {"foo", "yes"} because reasons, but found {"yes", "no", "maybe"}.""");
    }

    [Fact]
    public void When_no_enum_values_exists_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().ContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to contain enum values {"foo", "bar"} because reasons, but found no enum values.""");
    }

    [Fact]
    public void When_no_enum_values_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().ContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
