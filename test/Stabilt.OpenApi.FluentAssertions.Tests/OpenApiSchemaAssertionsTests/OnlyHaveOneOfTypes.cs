﻿namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiSchemaAssertionsTests;

public class OnlyHaveOneOfTypes
{
    private readonly OpenApiSchema _schema = OpenApiSchemaHelper.CreateObject();

    [Fact]
    public void When_all_matches_Does_not_throw_exception()
    {
        _schema.WithOneOfType("Item");

        var act = () => _schema.Should().OnlyHaveOneOfTypes(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_also_containing_schema_with_reference_Does_not_throw_exception()
    {
        _schema
            .WithOneOfType("Item")
            .WithOneOfReference("MyReference");

        var act = () => _schema.Should().OnlyHaveOneOfTypes(["Item"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_value_Throws_exception()
    {
        _schema.WithOneOfType("Item");

        var act = () => _schema.Should().OnlyHaveOneOfTypes(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have OneOf types {"Foo"} because reasons, but found {"Item"}.""");
    }

    [Fact]
    public void When_empty_Throws_exception()
    {
        var act = () => _schema.Should().OnlyHaveOneOfTypes(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _schema to only have OneOf types {"Foo"} because reasons, but found {empty}.""");
    }
}