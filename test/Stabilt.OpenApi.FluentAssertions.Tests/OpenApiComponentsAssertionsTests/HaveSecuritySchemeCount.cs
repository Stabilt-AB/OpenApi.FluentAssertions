namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiComponentsAssertionsTests;

public class HaveSecuritySchemeCount
{
    private readonly OpenApiComponents _components = OpenApiComponentsHelper.Create();

    [Fact]
    public void When_count_is_correct_Does_not_throw_exception()
    {
        _components
            .WithSecurityScheme("bearer-1")
            .WithSecurityScheme("bearer-2");

        var act = () => _components.Should().HaveSecuritySchemeCount(2);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var act = () => _components.Should().HaveSecuritySchemeCount(0, "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var act = () => _components.Should().HaveSecuritySchemeCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _components to have 3 security schemes because reasons, but found no security schemes.");
    }

    [Fact]
    public void When_count_is_incorrect_Throws_exception()
    {
        _components
            .WithSecurityScheme("bearer-1")
            .WithSecurityScheme("bearer-2");

        var act = () => _components.Should().HaveSecuritySchemeCount(3, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _components to have 3 security schemes because reasons, but found 2: {"bearer-1", "bearer-2"}.""");
    }
}
