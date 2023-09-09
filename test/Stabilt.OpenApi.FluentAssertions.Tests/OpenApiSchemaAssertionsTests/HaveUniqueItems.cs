namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveUniqueItems
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray();

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        _schema.WithUniqueItems();

        var act = () => _schema.Should().HaveUniqueItems();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_unique_items_Throws_exception()
    {
        var act = () => _schema.Should().HaveUniqueItems("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have unique items because reasons, but it didn't.");
    }
}
