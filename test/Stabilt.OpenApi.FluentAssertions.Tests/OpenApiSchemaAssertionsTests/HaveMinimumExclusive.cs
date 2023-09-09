namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMinimumExclusive
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("number");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMinimumExlusive(10.5m);

        var act = () => _schema.Should().HaveMinimumExclusive(10.5m);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_minimum_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMinimumExclusive(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive minimum value of 5.5 because reasons, but found <null>.");
    }

    [Fact]
    public void When_minimum_is_wrong_Throws_exception()
    {
        _schema.WithMinimumExlusive(10.5m);

        var act = () => _schema.Should().HaveMinimumExclusive(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive minimum value of 5.5 because reasons, but found 10.5.");
    }

    [Fact]
    public void When_not_exclusive_Throws_exception()
    {
        _schema.WithMinimum(10.5m);

        var act = () => _schema.Should().HaveMinimumExclusive(10.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive minimum value of 10.5 because reasons, but it wasn't exclusive.");
    }
}
