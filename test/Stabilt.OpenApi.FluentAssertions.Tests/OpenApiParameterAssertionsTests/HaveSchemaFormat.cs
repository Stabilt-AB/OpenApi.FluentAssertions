namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class HaveSchemaFormat
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_has_schema_format_Does_not_throw_exception()
    {
        _parameter.WithSchema("string", "uuid");

        var act = () => _parameter.Should().HaveSchemaFormat("uuid");

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().HaveSchemaFormat("int32", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema format "int32" because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_format_Throws_exception()
    {
        _parameter.WithSchema("string", "uuid");

        var act = () => _parameter.Should().HaveSchemaFormat("int32", "because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to have schema format "int32" because reasons, but found "uuid".""");
    }
}
