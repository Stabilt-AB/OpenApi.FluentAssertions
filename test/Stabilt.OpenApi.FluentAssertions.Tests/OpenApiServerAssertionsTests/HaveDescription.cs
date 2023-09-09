namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiServerAssertionsTests;

public class HaveDescription
{
    private readonly OpenApiServer _server = OpenApiServerHelper.Create(description: "Test");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _server.Should().HaveDescription("Test");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _server.Should().HaveDescription("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _server to have description "Foo" because reasons, but found "Test".""");
    }
}
