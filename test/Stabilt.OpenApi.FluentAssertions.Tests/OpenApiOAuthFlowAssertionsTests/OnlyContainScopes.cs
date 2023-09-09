namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOAuthFlowAssertionsTests;

public class OnlyContainScopes
{
    private readonly OpenApiOAuthFlow _flow = OpenApiOAuthFlowHelper.Create()
        .WithScopes(new Dictionary<String, String>
        {
            ["scope-1"] = "scope",
            ["scope-2"] = "scope",
        });

    [Theory]
    [InlineData("scope-1", "scope-2")]
    [InlineData("scope-2", "scope-1")]
    public void When_contains_only_expected_Does_not_throw_exception(params String[] scopes)
    {
        var act = () => _flow.Should().OnlyContainScopes(scopes);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_other_exists_Throws_exception()
    {
        var act = () => _flow.Should().OnlyContainScopes(["scope-a", "scope-b"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _flow to only contain scopes {"scope-a", "scope-b"} because reasons, but found {"scope-1", "scope-2"}.""");
    }

    [Fact]
    public void When_empty_and_asserting_as_empty_Does_not_throw_exception()
    {
        var flow = OpenApiOAuthFlowHelper.Create();

        var act = () => flow.Should().OnlyContainScopes([], "because {0}", "reasons");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_and_asserting_as_not_empty_Throws_exception()
    {
        var flow = OpenApiOAuthFlowHelper.Create();

        var act = () => flow.Should().OnlyContainScopes(["scope-a", "scope-b"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected flow to only contain scopes {"scope-a", "scope-b"} because reasons, but found no scopes.""");
    }
}
