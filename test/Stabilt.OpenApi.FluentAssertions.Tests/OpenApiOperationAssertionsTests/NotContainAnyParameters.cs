namespace Stabilt.OpenApi.FluentAssertions.Tests.OpenApiOperationAssertionsTests;

public class NotContainAnyParameters
{
    private readonly OpenApiOperation _operation = OpenApiOperationHelper.Create();

    [Fact]
    public void When_empty_Does_not_throw_exception()
    {
        var act = () => _operation.Should().NotContainAnyParameters();

        act.Should().NotThrow();
    }

    [Fact]
    public void When_not_empty_Throws_exception()
    {
        _operation
            .WithParameter(OpenApiParameterHelper.Create("itemId"));

        var act = () => _operation.Should().NotContainAnyParameters("because {0}", "reasons");

        act.Should().Throw<XunitException>()
            .WithMessage("""Expected _operation to not contain any parameters because reasons, but found {"itemId"}.""");
    }
}
