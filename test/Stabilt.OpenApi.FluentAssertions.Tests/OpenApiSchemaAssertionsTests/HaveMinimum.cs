namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMinimum
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("number");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMinimum(10.5m);

        var act = () => _schema.Should().HaveMinimum(10.5m);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_minimum_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMinimum(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have minimum value of 5.5 because reasons, but found <null>.");
    }

    [Fact]
    public void When_minimum_is_wrong_Throws_exception()
    {
        _schema.WithMinimum(10.5m);

        var act = () => _schema.Should().HaveMinimum(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have minimum value of 5.5 because reasons, but found 10.5.");
    }
}
