namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMinProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithMinProperties(1);

        var act = () => _schema.Should().HaveMinProperties(1);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_min_properties_is_wrong_Throws_exception()
    {
        _schema.WithMinProperties(1);

        var act = () => _schema.Should().HaveMinProperties(2, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have minimum properties of 2 because reasons, but found 1.");
    }

    [Fact]
    public void When_min_properties_is_null_Throws_exception()
    {
        var act = () => _schema.Should().HaveMinProperties(2, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have minimum properties of 2 because reasons, but found <null>.");
    }
}
