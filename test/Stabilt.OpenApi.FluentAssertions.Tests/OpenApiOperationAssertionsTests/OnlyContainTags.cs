namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class OnlyContainTags
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithTags("TagA", "TagB");

    [Theory]
    [InlineData("TagA", "TagB")]
    [InlineData("TagB", "TagA")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] tags)
    {
        var act = () => _operation.Should().OnlyContainTags(tags);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _operation.Should().OnlyContainTags(["TagA", "Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to only contain tags {"TagA", "Foo"} because reasons, but found {"TagA", "TagB"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainTags(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to only contain tags {"Foo"} because reasons, but found no tags.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().OnlyContainTags([], "because {0}", "reasons");

        act.Should().NotThrow();
    }
}
