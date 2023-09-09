namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class BeNullable
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_nullable_Does_not_throw_exception()
    {
        _schema.WithNullable();

        var act = () => _schema.Should().BeNullable();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_nullable_Throws_exception()
    {
        var act = () => _schema.Should().BeNullable("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to be nullable because reasons, but it wasn't.");
    }
}
