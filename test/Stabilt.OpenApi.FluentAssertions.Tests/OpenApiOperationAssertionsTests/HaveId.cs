namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class HaveId
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create("updateItems");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _operation.Should().HaveId("updateItems");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _operation.Should().HaveId("getItems", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to have ID "getItems" because reasons, but found "updateItems".""");
    }
}
