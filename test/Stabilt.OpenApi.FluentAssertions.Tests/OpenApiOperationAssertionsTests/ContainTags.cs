namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainTags
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithTags("TagA", "TagB", "TagC");

    [Theory]
    [InlineData("TagA")]
    [InlineData("TagA", "TagB")]
    [InlineData("TagA", "TagB", "TagC")]
    [InlineData("TagA", "TagC")]
    [InlineData("TagA", "TagC", "TagB")]
    [InlineData("TagB")]
    [InlineData("TagB", "TagA")]
    [InlineData("TagB", "TagA", "TagC")]
    [InlineData("TagB", "TagC")]
    [InlineData("TagB", "TagC", "TagA")]
    [InlineData("TagC")]
    [InlineData("TagC", "TagA")]
    [InlineData("TagC", "TagA", "TagB")]
    [InlineData("TagC", "TagB")]
    [InlineData("TagC", "TagB", "TagA")]
    public void When_contains_all_Does_not_throw_exception(params string[] expectedValues)
    {
        var act = () => _operation.Should().ContainTags(expectedValues);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_asserting_for_none_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainTags([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_any_Throws_exception()
    {
        var act = () => _operation.Should().ContainTags(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain tags {"Foo", "Bar"} because reasons, but found {"TagA", "TagB", "TagC"}.""");
    }

    [Fact]
    public void When_contains_some_Throws_exception()
    {
        var act = () => _operation.Should().ContainTags(["Foo", "TagA"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain tags {"Foo", "TagA"} because reasons, but found {"TagA", "TagB", "TagC"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_for_none_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainTags([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainTags(["Foo", "Bar"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain tags {"Foo", "Bar"} because reasons, but found no tags.""");
    }
}
