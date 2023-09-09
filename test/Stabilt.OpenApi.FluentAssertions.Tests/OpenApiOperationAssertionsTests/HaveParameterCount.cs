namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class HaveParameterCount
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithParameter(OpenApiParameterHelper.Create("itemId"))
        .WithParameter(OpenApiParameterHelper.Create("another"));

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _operation.Should().HaveParameterCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _operation.Should().HaveParameterCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to have 1337 parameters because reasons, but found 2: {"itemId", "another"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().HaveParameterCount(0);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().HaveParameterCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected operation to have 1337 parameters because reasons, but found no parameters.");
    }
}
