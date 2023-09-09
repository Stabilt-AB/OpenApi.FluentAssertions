namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainParametersInOrder
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithParameter(OpenApiParameterHelper.Create("itemId"))
        .WithParameter(OpenApiParameterHelper.Create("another"));

    [Fact]
    public void When_contains_only_expected_Does_not_throw_exception()
    {
        var act = () => _operation.Should().OnlyContainParametersInOrder(["itemId", "another"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainParametersInOrder(["itemId"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _operation to only contain parameters {\"itemId\"} in order because reasons, but found {\"itemId\", \"another\"}.");
    }

    [Fact]
    public void When_wrong_order_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainParametersInOrder(["another", "itemId"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _operation to only contain parameters {\"another\", \"itemId\"} in order because reasons, but found {\"itemId\", \"another\"}.");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainParametersInOrder([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainParametersInOrder(["itemId"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected operation to only contain parameters {\"itemId\"} in order because reasons, but found no parameters.");
    }
}
