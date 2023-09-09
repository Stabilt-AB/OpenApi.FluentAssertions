namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotBeDeprecated
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_not_deprecated_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotBeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_deprecated_Throws_exception()
    {
        _schema.WithDeprecated();

        var act = () => _schema.Should().NotBeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to not be deprecated because reasons, but it was.");
    }
}
