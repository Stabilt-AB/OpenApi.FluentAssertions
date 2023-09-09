namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class ContainParameter
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create()
        .WithParameter(OpenApiParameterHelper.Create("id"))
        .WithParameter(OpenApiParameterHelper.Create("name"));

    [Theory]
    [InlineData("id")]
    [InlineData("name")]
    public void When_contains_Does_not_throw_exception(String name)
    {
        var act = () => _operation.Should().ContainParameter(name);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_contains_Throws_exception()
    {
        var act = () => _operation.Should().ContainParameter("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to contain a parameter with name "foo" because reasons, but found {"id", "name"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var operation = OpenApiOperationHelper.Create();

        var act = () => operation.Should().ContainParameter("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected operation to contain a parameter with name "foo" because reasons, but found no parameters.""");
    }
}
