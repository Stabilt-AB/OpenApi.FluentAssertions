namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMaximum
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMaximum(30);

        var act = () => _schema.Should().HaveMaximum(30);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_maximum_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMaximum(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have maximum value of 5.5 because reasons, but found <null>.");
    }

    [Fact]
    public void When_maximum_is_wrong_Throws_exception()
    {
        _schema.WithMaximum(30.5m);

        var act = () => _schema.Should().HaveMaximum(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have maximum value of 5.5 because reasons, but found 30.5.");
    }
}
