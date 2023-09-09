namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeDeprecated
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_deprecated_Does_not_throw_exception()
    {
        _schema.WithDeprecated();

        var act = () => _schema.Should().BeDeprecated();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_deprecated_Throws_exception()
    {
        var act = () => _schema.Should().BeDeprecated("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to be deprecated because reasons, but it wasn't.");
    }
}
