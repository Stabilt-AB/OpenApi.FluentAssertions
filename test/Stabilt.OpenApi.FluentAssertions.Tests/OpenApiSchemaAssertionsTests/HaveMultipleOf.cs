namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMultipleOf
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMultipleOf(2.5m);

        var act = () => _schema.Should().HaveMultipleOf(2.5m);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_multiple_of_is_wrong_Throws_exception()
    {
        _schema.WithMultipleOf(2.5m);

        var act = () => _schema.Should().HaveMultipleOf(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have multiple of 5.5 because reasons, but found 2.5.");
    }

    [Fact]
    public void When_multiple_of_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMultipleOf(5.5m, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have multiple of 5.5 because reasons, but found <null>.");
    }
}
