namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class ContainEnumValues
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create()
        .WithEnumSchema("yes", "no", "maybe");

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
        var act = () => _parameter.Should().ContainEnumValues(securitySchemes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _parameter.Should().ContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contain_any_Throws_exception()
    {
        var act = () => _parameter.Should().ContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to contain enum values {"foo", "bar"} because reasons, but found {"yes", "no", "maybe"}.""");
    }

    [Fact]
    public void When_not_contain_some_Throws_exception()
    {
        var act = () => _parameter.Should().ContainEnumValues(["foo", "yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to contain enum values {"foo", "yes"} because reasons, but found {"yes", "no", "maybe"}.""");
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create();

        var act = () => parameter.Should().ContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected parameter to contain enum values {"foo", "bar"} because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_no_enum_values_exists_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create()
            .WithSchema("string");

        var act = () => parameter.Should().ContainEnumValues(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected parameter to contain enum values {"foo", "bar"} because reasons, but found no enum values.""");
    }

    [Fact]
    public void When_no_enum_values_exists_and_asserting_for_none_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create()
            .WithSchema("string");

        var act = () => parameter.Should().ContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
