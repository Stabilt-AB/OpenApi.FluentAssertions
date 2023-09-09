namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class BeDateType
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_parameter_is_date_type_Does_not_throw_exception()
    {
        _parameter.WithSchema("string", "date");

        var act = () => _parameter.Should().BeDateType();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_schema_is_null_Throws_exception()
    {
        var act = () => _parameter.Should().BeDateType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with format "date" because reasons, but the schema was <null>.""");
    }

    [Fact]
    public void When_wrong_format_Throws_exception()
    {
        _parameter.WithSchema("string");

        var act = () => _parameter.Should().BeDateType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with format "date" because reasons, but found format <null>.""");
    }

    [Fact]
    public void When_wrong_type_Throws_exception()
    {
        _parameter.WithSchema("number");

        var act = () => _parameter.Should().BeDateType("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _parameter to be of type "string" with format "date" because reasons, but found type "number".""");
    }
}
