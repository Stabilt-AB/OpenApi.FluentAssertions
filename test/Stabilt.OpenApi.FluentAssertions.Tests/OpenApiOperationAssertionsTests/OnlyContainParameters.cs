namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainParameters
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithParameter(OpenApiParameterHelper.Create("itemId"))
        .WithParameter(OpenApiParameterHelper.Create("another"));

    [Theory]
    [InlineData("itemId", "another")]
    [InlineData("another", "itemId")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] parameters)
    {
        var act = () => _operation.Should().OnlyContainParameters(parameters);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainParameters(["itemId"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain parameters {"itemId"} because reasons, but found {"itemId", "another"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainParameters([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainParameters(["itemId"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain parameters {"itemId"} because reasons, but found no parameters.""");
    }
}
