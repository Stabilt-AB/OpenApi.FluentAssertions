namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotBeWriteOnly
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_not_write_only_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotBeWriteOnly();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_write_only_Throws_exception()
    {
        _schema.WithWriteOnly();

        var act = () => _schema.Should().NotBeWriteOnly("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to not be write-only because reasons, but it was.");
    }
}
