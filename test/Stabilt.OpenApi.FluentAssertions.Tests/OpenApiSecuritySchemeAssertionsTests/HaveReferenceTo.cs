namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveReferenceTo
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateApiKeyScheme()
        .WithReference("test");

    [Fact]
    public void When_scheme_has_reference_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveReferenceTo("test");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_scheme_does_not_have_reference_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveReferenceTo("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _securityScheme to have reference with ID "foo" because reasons, but found "test".""");
    }
}
