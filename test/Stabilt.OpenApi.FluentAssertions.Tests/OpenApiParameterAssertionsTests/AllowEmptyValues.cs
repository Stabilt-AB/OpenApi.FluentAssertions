namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class AllowEmptyValues
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_empty_values_is_allowed_Does_not_throw_exception()
    {
        _parameter.WithAllowEmptyValue();

        var act = () => _parameter.Should().AllowEmptyValues();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_values_is_not_allowed_Throws_exception()
    {
        var act = () => _parameter.Should().AllowEmptyValues("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _parameter to allow empty values because reasons, but it didn't.");
    }
}
