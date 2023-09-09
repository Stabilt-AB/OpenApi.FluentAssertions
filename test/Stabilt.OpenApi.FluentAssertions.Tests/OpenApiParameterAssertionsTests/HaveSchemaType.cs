namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveSchemaType
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_schema_type_Does_not_throw_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().HaveSchemaType("string");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().HaveSchemaType("integer", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema type "integer" because reasons, but found "string".""");
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().HaveSchemaType("integer", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema type "integer" because reasons, but the schema was <null>.""");
    }
}
