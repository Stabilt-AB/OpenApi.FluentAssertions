using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiResponseAssertions(OpenApiResponse value)
    : ObjectAssertions<OpenApiResponse, OpenApiResponseAssertions>(value)
{
    [CustomAssertion]
    public AndWhichConstraint<OpenApiResponseAssertions, OpenApiMediaType> ContainContent(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain response content type {0}{reason}", expected)
            .ForCondition(Subject.Content.Any())
            .FailWith(", but it didn't have any content.")
            .Then
            .ForCondition(Subject.Content.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.Content.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiResponseAssertions, OpenApiMediaType>(this, Subject.Content[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiResponseAssertions> NotContainAnyContent(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any response content{reason}")
            .ForCondition(!Subject.Content.Any())
            .FailWith(", but found {0}.", Subject.Content.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiResponseAssertions>(this);
    }
}
