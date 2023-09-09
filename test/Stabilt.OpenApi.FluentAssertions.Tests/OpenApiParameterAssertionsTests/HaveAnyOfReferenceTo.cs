namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveAnyOfReferenceTo
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_AnyOf_reference_Does_not_throw_exception()
    {
        _parameter.WithAnyOf("ItemType");

        var act = () => _parameter.Should().HaveAnyOfReferenceTo(["ItemType"]);

        act.Should().NotThrow();
    }

    [Fact]
    public void When_all_of_is_null_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().HaveAnyOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have AnyOf reference {"Foo"} because reasons, but found {empty}.""");
    }

    [Fact]
    public void When_reference_is_wrong_Throws_exception()
    {
        _parameter.WithAnyOf("ItemType");

        var act = () => _parameter.Should().HaveAnyOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have AnyOf reference {"Foo"} because reasons, but found {"ItemType"}.""");
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().HaveAnyOfReferenceTo(["Foo"], "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have AnyOf reference {"Foo"} because reasons, but the schema was <null>.""");
    }
}
