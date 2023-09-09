namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainTag
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithTags("Items");

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        var act = () => _operation.Should().ContainTag("Items");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _operation.Should().ContainTag("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain tag "Foo" because reasons, but found {"Items"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainTag("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain tag "Foo" because reasons, but found no tags.""");
    }
}
