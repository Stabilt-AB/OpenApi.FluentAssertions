namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeEnumType
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_is_enum_type_Does_not_throw_exception()
    {
        _parameter.WithEnumSchema("yes", "no");

        var act = () => _parameter.Should().BeEnumType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeEnumType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("number");

        var act = () => _parameter.Should().BeEnumType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values because reasons, but found type "number".""");
    }

    [Theory]
    [InlineData("yes", "no")]
    [InlineData("no", "yes")]
    public void With_values_When_parameter_is_enum_type_Does_not_throw_exception(params String[] expectedValues)
    {
        _parameter.WithEnumSchema("yes", "no");

        var act = () => _parameter.Should().BeEnumType(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void With_values_When_no_enum_values_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().BeEnumType(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values {"yes", "no"} because reasons, but found no enum values.""");
    }

    [Fact]
    public void With_values_When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeEnumType(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values {"yes", "no"} because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void With_values_When_subset_Throws_exception()
    {
        _parameter.WithEnumSchema("yes", "no");

        var act = () => _parameter.Should().BeEnumType(["yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values {"yes"} because reasons, but found enum values {"yes", "no"}.""");
    }

    [Fact]
    public void With_values_When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("number");

        var act = () => _parameter.Should().BeEnumType(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with enum values {"yes", "no"} because reasons, but found type "number".""");
    }
}
