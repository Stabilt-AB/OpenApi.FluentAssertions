namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMaxProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMaxProperties(8);

        var act = () => _schema.Should().HaveMaxProperties(8);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_max_properties_is_wrong_Throws_exception()
    {
        _schema.WithMaxProperties(8);

        var act = () => _schema.Should().HaveMaxProperties(13, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have maximum properties of 13 because reasons, but found 8.");
    }

    [Fact]
    public void When_max_properties_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMaxProperties(13, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have maximum properties of 13 because reasons, but found <null>.");
    }
}
