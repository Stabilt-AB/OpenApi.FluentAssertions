namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeEnumType
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateEnum("yes", "no");

    [Fact]
    public void When_schema_is_enum_Does_not_throw_exception()
    {
        var act = () => _schema.Should().BeEnumType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_no_enum_values_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeEnumType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with enum values because reasons, but found no enum values.""");
    }

    [Fact]
    public void When_type_is_wrong_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number");

        var act = () => schema.Should().BeEnumType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with enum values because reasons, but found type "number".""");
    }

    [Theory]
    [InlineData("yes", "no")]
    [InlineData("no", "yes")]
    public void With_values_When_schema_is_enum_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _schema.Should().BeEnumType(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void With_values_When_no_enum_values_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().BeEnumType(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with only enum values {"yes", "no"} because reasons, but found no enum values.""");
    }

    [Fact]
    public void With_values_When_subset_Throws_exception()
    {
        var act = () => _schema.Should().BeEnumType(["yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to be of type "string" with only enum values {"yes"} because reasons, but found enum values {"yes", "no"}.""");
    }

    [Fact]
    public void With_values_When_type_is_wrong_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("number");

        var act = () => schema.Should().BeEnumType(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to be of type "string" with only enum values {"yes", "no"} because reasons, but found type "number".""");
    }
}
