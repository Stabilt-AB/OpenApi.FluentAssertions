namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiPathItemAssertionsTests;

public class OnlyContainOperations
{
    private readonly OpenApiPathItem _pathItem = OpenApiPathItemHelper.Create()
        .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("putItem"))
        .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
        .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem"));

    [Theory]
    [InlineData(OperationType.Delete, OperationType.Get, OperationType.Put)]
    [InlineData(OperationType.Delete, OperationType.Put, OperationType.Get)]
    [InlineData(OperationType.Get, OperationType.Delete, OperationType.Put)]
    [InlineData(OperationType.Get, OperationType.Put, OperationType.Delete)]
    [InlineData(OperationType.Put, OperationType.Delete, OperationType.Get)]
    [InlineData(OperationType.Put, OperationType.Get, OperationType.Delete)]
    public void When_contains_only_expected_Does_not_throw_exception(params OperationType[] operationTypes)
    {
        var act = () => _pathItem.Should().OnlyContainOperations(operationTypes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _pathItem.Should().OnlyContainOperations([OperationType.Get, OperationType.Put], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _pathItem to only contain operations {"Get", "Put"} because reasons, but found {"Put", "Get", "Delete"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().OnlyContainOperations([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().OnlyContainOperations([OperationType.Get, OperationType.Put], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected pathItem to only contain operations {"Get", "Put"} because reasons, but found no operations.""");
    }
}
