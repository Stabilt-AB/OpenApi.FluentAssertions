namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiPathItemAssertionsTests;

public class HaveOperationCount
{
    private readonly OpenApiPathItem _pathItem = OpenApiPathItemHelper.Create()
        .WithOperation(OperationType.Put, OpenApiOperationHelper.Create("putItem"))
        .WithOperation(OperationType.Get, OpenApiOperationHelper.Create("getItem"))
        .WithOperation(OperationType.Delete, OpenApiOperationHelper.Create("deleteItem"));

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _pathItem.Should().HaveOperationCount(3);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _pathItem.Should().HaveOperationCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _pathItem to contain 1337 operations because reasons, but found 3: {"Put", "Get", "Delete"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().HaveOperationCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var pathItem = OpenApiPathItemHelper.Create();

        var act = () => pathItem.Should().HaveOperationCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected pathItem to contain 3 operations because reasons, but found no operations.");
    }
}
