namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class HaveResponseCodeCount
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithResponseCode(200, OpenApiResponseHelper.Create())
        .WithResponseCode(404, OpenApiResponseHelper.Create());

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        var act = () => _operation.Should().HaveResponseCodeCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        var act = () => _operation.Should().HaveResponseCodeCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to have 1337 responses because reasons, but found {"200", "404"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().HaveResponseCodeCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().HaveResponseCodeCount(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected operation to have 1337 responses because reasons, but found no responses.");
    }
}
