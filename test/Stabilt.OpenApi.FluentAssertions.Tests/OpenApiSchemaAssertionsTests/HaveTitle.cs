namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class HaveTitle
{
    [Fact]
    public void When_correct_Does_not_throw_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject("Item");

        var act = () => schema.Should().HaveTitle("Item");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_title_is_wrong_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject("Item");

        var act = () => schema.Should().HaveTitle("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have title "Foo" because reasons, but found "Item".""");
    }

    [Fact]
    public void When_title_is_null_Throws_exception()
    {
        var schema = OpenApiSchemaHelper.CreateObject();

        var act = () => schema.Should().HaveTitle("Foo", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected schema to have title "Foo" because reasons, but found <null>.""");
    }
}
