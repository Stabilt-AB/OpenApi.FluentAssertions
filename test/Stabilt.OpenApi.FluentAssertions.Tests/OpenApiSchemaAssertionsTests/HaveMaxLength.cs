namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMaxLength
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMaxLength(100);

        var act = () => _schema.Should().HaveMaxLength(100);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_max_length_is_wrong_Throws_exception()
    {
        _schema.WithMaxLength(100);

        var act = () => _schema.Should().HaveMaxLength(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have max length value of 1337 because reasons, but found 100.");
    }

    [Fact]
    public void When_max_length_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMaxLength(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have max length value of 1337 because reasons, but found <null>.");
    }
}
