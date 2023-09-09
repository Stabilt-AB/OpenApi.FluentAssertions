namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class OnlyContainEnumValues
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateEnum("yes", "no");

    [Theory]
    [InlineData("no", "yes")]
    [InlineData("yes", "no")]
    public void When_contains_all_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _schema.Should().OnlyContainEnumValues(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _schema.Should().OnlyContainEnumValues(["yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only contain enum values {"yes"} because reasons, but found {"yes", "no"}.""");
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _schema.Should().OnlyContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only contain enum values {"foo", "bar"} because reasons, but found {"yes", "no"}.""");
    }

    [Fact]
    public void When_no_enum_values_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().OnlyContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
