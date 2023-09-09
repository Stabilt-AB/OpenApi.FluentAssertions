namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveMinItems
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateArray().WithMinItems(3);

    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var act = () => _schema.Should().HaveMinItems(3);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_min_items_is_wrong_Throws_exception()
    {
        var act = () => _schema.Should().HaveMinItems(5, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _schema to have minimum items of 5 because reasons, but found 3.");
    }

    [Fact]
    public void When_min_items_is_null_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateArray();

        var act = () => schema.Should().HaveMinItems(5, "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected schema to have minimum items of 5 because reasons, but found <null>.");
    }
}
