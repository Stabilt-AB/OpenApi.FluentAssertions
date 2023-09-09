namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiPathItemAssertionsTests;

public class ContainOperation
{
    private readonly OpenApiPathItem _pathItem = OpenApiPathItemHelper.Create()
        .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("putItem"))
        .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
        .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem"));

    [Theory]
    [InlineData(OperationType.Get)]
    [InlineData(OperationType.Delete)]
    [InlineData(OperationType.Put)]
    public void When_contains_Does_not_throw_exception(OperationType operationType)
    {
        var act = () => _pathItem.Should().ContainOperation(operationType);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _pathItem.Should().ContainOperation(OperationType.Trace, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _pathItem to contain an operation of type "Trace" because reasons, but found {"Put", "Get", "Delete"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().ContainOperation(OperationType.Get, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected pathItem to contain an operation of type "Get" because reasons, but found no operations.""");
    }
}
