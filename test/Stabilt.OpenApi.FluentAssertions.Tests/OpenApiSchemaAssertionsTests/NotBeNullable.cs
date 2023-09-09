namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class NotBeNullable
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateType("string");

    [Fact]
    public void When_not_nullable_Does_not_throw_exception()
    {
        var act = () => _schema.Should().NotBeNullable();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_nullable_Throws_exception()
    {
        _schema.WithNullable();

        var act = () => _schema.Should().NotBeNullable("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to not be nullable because reasons, but it was.");
    }
}
