namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiPathItemAssertionsTests;

public class ContainOperations
{
    private readonly OpenApiPathItem _pathItem = OpenApiPathItemHelper.Create()
        .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("putItem"))
        .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
        .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem"));

    [Theory]
    [InlineData(OperationType.Delete)]
    [InlineData(OperationType.Delete, OperationType.Get)]
    [InlineData(OperationType.Delete, OperationType.Get, OperationType.Put)]
    [InlineData(OperationType.Delete, OperationType.Put)]
    [InlineData(OperationType.Delete, OperationType.Put, OperationType.Get)]
    [InlineData(OperationType.Get)]
    [InlineData(OperationType.Get, OperationType.Delete)]
    [InlineData(OperationType.Get, OperationType.Delete, OperationType.Put)]
    [InlineData(OperationType.Get, OperationType.Put)]
    [InlineData(OperationType.Get, OperationType.Put, OperationType.Delete)]
    [InlineData(OperationType.Put)]
    [InlineData(OperationType.Put, OperationType.Delete)]
    [InlineData(OperationType.Put, OperationType.Delete, OperationType.Get)]
    [InlineData(OperationType.Put, OperationType.Get)]
    [InlineData(OperationType.Put, OperationType.Get, OperationType.Delete)]
    public void When_contains_all_Does_not_throw_exception(params OperationType[] operationTypes)
    {
        var act = () => _pathItem.Should().ContainOperations(operationTypes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _pathItem.Should().ContainOperations([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _pathItem.Should().ContainOperations([OperationType.Trace], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _pathItem to contain operations {"Trace"} because reasons, but found {"Put", "Get", "Delete"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _pathItem.Should().ContainOperations([OperationType.Get, OperationType.Trace], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _pathItem to contain operations {"Get", "Trace"} because reasons, but found {"Put", "Get", "Delete"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().ContainOperations([OperationType.Get, OperationType.Trace], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected pathItem to contain operations {"Get", "Trace"} because reasons, but found no operations.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().ContainOperations([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
