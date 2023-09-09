namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainParameters
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithParameter("param1")
        .WithParameter("param2")
        .WithParameter("param3");

    [Theory]
    [InlineData("param1")]
    [InlineData("param1", "param2")]
    [InlineData("param1", "param2", "param3")]
    [InlineData("param1", "param3")]
    [InlineData("param1", "param3", "param2")]
    [InlineData("param2")]
    [InlineData("param2", "param1")]
    [InlineData("param2", "param1", "param3")]
    [InlineData("param2", "param3")]
    [InlineData("param2", "param3", "param1")]
    [InlineData("param3")]
    [InlineData("param3", "param1")]
    [InlineData("param3", "param1", "param2")]
    [InlineData("param3", "param2")]
    [InlineData("param3", "param2", "param1")]
    public void When_contains_all_Does_not_throw_exception(params string[] expectedValues)
    {
        var act = () => _operation.Should().ContainParameters(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainParameters([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _operation.Should().ContainParameters(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain parameters {"foo", "bar"} because reasons, but found {"param1", "param2", "param3"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _operation.Should().ContainParameters(["foo", "param1"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain parameters {"foo", "param1"} because reasons, but found {"param1", "param2", "param3"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainParameters([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainParameters(["foo", "bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain parameters {"foo", "bar"} because reasons, but found no parameters.""");
    }
}
