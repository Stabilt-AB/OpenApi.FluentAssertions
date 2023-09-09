namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotHaveAdditionalProperties
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotHaveAdditionalProperties();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _schema.WithAdditionalProperties(OpenApiSchemaHelper.Create());

        var act = () => _schema.Should().NotHaveAdditionalProperties("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to not have additional properties because reasons, but it had.");
    }
}
