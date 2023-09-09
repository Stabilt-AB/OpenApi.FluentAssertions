namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiParameterAssertionsTests;

public class NotAllowEmptyValues
{
    private readonly OpenApiParameter _parameter = OpenApiParameterHelper.Create();

    [Fact]
    public void When_empty_values_is_not_allowed_Does_not_throw_exception()
    {
        var act = () => _parameter.Should().NotAllowEmptyValues();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_empty_values_is_allowed_Throws_exception()
    {
        _parameter.WithAllowEmptyValue();

        var act = () => _parameter.Should().NotAllowEmptyValues("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("Expected _parameter to not allow empty values because reasons, but it did.");
    }
}
