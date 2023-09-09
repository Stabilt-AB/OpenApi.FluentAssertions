namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveBearerFormat
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateJwtBearerScheme();

    [Fact]
    public void When_scheme_has_bearer_format_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveBearerFormat("JWT");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_scheme_does_not_have_bearer_format_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveBearerFormat("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _securityScheme to have bearer format "foo" because reasons, but found "JWT".""");
    }
}
