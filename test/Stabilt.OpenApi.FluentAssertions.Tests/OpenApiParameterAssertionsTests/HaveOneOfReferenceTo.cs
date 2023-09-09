namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveOneOfReferenceTo
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_OneOf_reference_Does_not_throw_exception()
    {
        _parameter.WithOneOf("ItemType");

        var act = () => _parameter.Should().HaveOneOfReferenceTo(["ItemType"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_all_of_is_null_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().HaveOneOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have OneOf reference {"Foo"} because reasons, but found {empty}.""");
    }

    [Fact]
    public void When_reference_is_wrong_Throws_exception()
    {
        _parameter.WithOneOf("ItemType");

        var act = () => _parameter.Should().HaveOneOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have OneOf reference {"Foo"} because reasons, but found {"ItemType"}.""");
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().HaveOneOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have OneOf reference {"Foo"} because reasons, but the schema was <null>.""");
    }
}
