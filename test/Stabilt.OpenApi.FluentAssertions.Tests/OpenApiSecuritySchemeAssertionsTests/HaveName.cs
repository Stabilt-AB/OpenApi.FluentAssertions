namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveName
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateApiKeyScheme("test-key");

    [Fact]
    public void When_scheme_has_name_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveName("test-key");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_scheme_does_not_have_name_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveName("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _securityScheme to have name "foo" because reasons, but found "test-key".""");
    }
}
