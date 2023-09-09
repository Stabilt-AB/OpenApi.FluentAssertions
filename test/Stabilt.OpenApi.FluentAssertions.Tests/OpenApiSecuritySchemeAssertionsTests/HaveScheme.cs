namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSecuritySchemeAssertionsTests;

public class HaveScheme
{
    private readonly OpenApiSecurityScheme _securityScheme = OpenApiSecuritySchemeHelper.CreateJwtBearerScheme();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _securityScheme.Should().HaveScheme("bearer");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_incorrect_Throws_exception()
    {
        var act = () => _securityScheme.Should().HaveScheme("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _securityScheme to have scheme "foo" because reasons, but found "bearer".""");
    }
}
