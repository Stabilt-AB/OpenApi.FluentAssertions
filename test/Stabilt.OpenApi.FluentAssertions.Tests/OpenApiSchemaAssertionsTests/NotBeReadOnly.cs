namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotBeReadOnly
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_not_read_only_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotBeReadOnly();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_read_only_Throws_exception()
    {
        _schema.WithReadOnly();

        var act = () => _schema.Should().NotBeReadOnly("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to not be read-only because reasons, but it was.");
    }
}
