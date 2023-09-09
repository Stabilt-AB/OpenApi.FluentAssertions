namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMinLength
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMinLength(1);

        var act = () => _schema.Should().HaveMinLength(1);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_min_length_is_wrong_Throws_exception()
    {
        _schema.WithMinLength(1);

        var act = () => _schema.Should().HaveMinLength(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have min length value of 1337 because reasons, but found 1.");
    }

    [Fact]
    public void When_min_length_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMinLength(1337, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have min length value of 1337 because reasons, but found <null>.");
    }
}
