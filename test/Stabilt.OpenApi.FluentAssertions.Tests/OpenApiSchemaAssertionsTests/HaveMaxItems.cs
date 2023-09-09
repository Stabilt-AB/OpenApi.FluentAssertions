namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMaxItems
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray()
        .WithMaxItems(10);

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _schema.Should().HaveMaxItems(10);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_max_items_is_wrong_Throws_exception()
    {
        var act = () => _schema.Should().HaveMaxItems(5, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have maximum items of 5 because reasons, but found 10.");
    }

    [Fact]
    public void When_max_items_is_null_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateArray();

        var act = () => schema.Should().HaveMaxItems(5, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected schema to have maximum items of 5 because reasons, but found <null>.");
    }
}
