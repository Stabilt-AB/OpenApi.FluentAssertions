namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveReferenceTo
{
    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObjectReference("Item");

        var act = () => schema.Should().HaveReferenceTo("Item");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_reference_is_wrong_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObjectReference("Another");

        var act = () => schema.Should().HaveReferenceTo("Item", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have reference to "Item" because reasons, but found "Another".""");
    }

    [Fact]
    public void When_reference_is_null_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateType("string");

        var act = () => schema.Should().HaveReferenceTo("Item", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have reference to "Item" because reasons, but found <null>.""");
    }
}
