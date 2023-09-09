using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiServerAssertions(OpenApiServer value)
    : ObjectAssertions<OpenApiServer, OpenApiServerAssertions>(value)
{
    [CustomAssertion]
    public AndConstraint<OpenApiServerAssertions> HaveDescription(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have description {0}{reason}", expected)
            .ForCondition(Subject.Description == expected)
            .FailWith(", but found {0}.", Subject.Description)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiServerAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiServerAssertions> HaveUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have URL {0}{reason}", expected)
            .ForCondition(Subject.Url == expected)
            .FailWith(", but found {0}.", Subject.Url)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiServerAssertions>(this);
    }
}
