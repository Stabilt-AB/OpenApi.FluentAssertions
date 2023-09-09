namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HavePattern
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithPattern("a*");

        var act = () => _schema.Should().HavePattern("a*");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_pattern_is_wrong_Throws_exception()
    {
        _schema.WithPattern("a*");

        var act = () => _schema.Should().HavePattern("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to have pattern "foo" because reasons, but found "a*".""");
    }

    [Fact]
    public void When_pattern_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HavePattern("foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to have pattern "foo" because reasons, but found <null>.""");
    }
}
