namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class OnlyHaveAnyOfReferences
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_all_matches_Does_not_throw_exception()
    {
        _schema.WithAnyOfReference("Item");

        var act = () => _schema.Should().OnlyHaveAnyOfReferences(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_also_containing_schema_with_reference_Does_not_throw_exception()
    {
        _schema
            .WithAnyOfReference("Item")
            .WithAnyOfType("MyType");

        var act = () => _schema.Should().OnlyHaveAnyOfReferences(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_value_Throws_exception()
    {
        _schema.WithAnyOfReference("Item");

        var act = () => _schema.Should().OnlyHaveAnyOfReferences(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have AnyOf references to {"Foo"} because reasons, but found {"Item"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _schema.Should().OnlyHaveAnyOfReferences(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have AnyOf references to {"Foo"} because reasons, but found {empty}.""");
    }
}
