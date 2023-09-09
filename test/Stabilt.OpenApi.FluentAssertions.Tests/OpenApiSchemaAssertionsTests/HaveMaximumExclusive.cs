namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMaximumExclusive
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMaximumExlusive(30.5m);

        var act = () => _schema.Should().HaveMaximumExclusive(30.5m);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_maximum_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMaximumExclusive(13.37m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive maximum value of 13.37 because reasons, but found <null>.");
    }

    [Fact]
    public void When_maximum_is_wrong_Throws_exception()
    {
        _schema.WithMaximumExlusive(30.5m);

        var act = () => _schema.Should().HaveMaximumExclusive(13.37m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive maximum value of 13.37 because reasons, but found 30.5.");
    }

    [Fact]
    public void When_not_exclusive_Throws_exception()
    {
        _schema.WithMaximum(30.5m);

        var act = () => _schema.Should().HaveMaximumExclusive(30.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have exclusive maximum value of 30.5 because reasons, but it wasn't exclusive.");
    }
}
