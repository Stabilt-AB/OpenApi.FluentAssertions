namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class OnlyContainEnumValues
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create()
        .WithEnumSchema("yes", "no");

    [Theory]
    [InlineData("yes", "no")]
    [InlineData("no", "yes")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] expectedValues)
    {
        var act = () => _parameter.Should().OnlyContainEnumValues(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _parameter.Should().OnlyContainEnumValues(["yes"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to only contain enum values {"yes"} because reasons, but found {"yes", "no"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var parameter = OpenApiParameterHelper.Create().WithEnumSchema();

        var act = () => parameter.Should().OnlyContainEnumValues(["yes", "no"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected parameter to only contain enum values {"yes", "no"} because reasons, but found no enum values.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var parameter = OpenApiParameterHelper.Create().WithEnumSchema();

        var act = () => parameter.Should().OnlyContainEnumValues([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
