namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveAdditionalProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_contains_Does_not_throw_exception()
    {
        _schema.WithAdditionalProperties(OpenApiSchemaHelper.Create());

        var act = () => _schema.Should().HaveAdditionalProperties();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _schema.Should().HaveAdditionalProperties("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have additional properties because reasons, but found <null>.");
    }
}
