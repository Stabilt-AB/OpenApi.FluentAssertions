namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class OnlyHaveOneOfReferences
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_all_matches_Does_not_throw_exception()
    {
        _schema.WithOneOfReference("Item");

        var act = () => _schema.Should().OnlyHaveOneOfReferences(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_also_containing_schema_with_reference_Does_not_throw_exception()
    {
        _schema
            .WithOneOfReference("Item")
            .WithOneOfType("MyType");

        var act = () => _schema.Should().OnlyHaveOneOfReferences(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_value_Throws_exception()
    {
        _schema.WithOneOfReference("Item");

        var act = () => _schema.Should().OnlyHaveOneOfReferences(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have OneOf references to {"Foo"} because reasons, but found {"Item"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _schema.Should().OnlyHaveOneOfReferences(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have OneOf references to {"Foo"} because reasons, but found {empty}.""");
    }
}
