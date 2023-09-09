using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiOAuthFlowAssertions(OpenApiOAuthFlow value)
    : ObjectAssertions<OpenApiOAuthFlow, OpenApiOAuthFlowAssertions>(value)
{
    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> ContainScopes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain scopes {0}{reason}", expected)
            .ForCondition(Subject.Scopes.Any())
            .FailWith(", but found no scopes.")
            .Then
            .ForCondition(expected.All(Subject.Scopes.ContainsKey))
            .FailWith(", but found {0}.", Subject.Scopes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOAuthFlowAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveAuthorizationUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveAuthorizationUrl(new Uri(expected), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveAuthorizationUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have authorization URL {0}{reason}", expected)
            .ForCondition(Subject.AuthorizationUrl == expected)
            .FailWith(", but found {0}.", Subject.AuthorizationUrl)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOAuthFlowAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveRefreshUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveRefreshUrl(new Uri(expected), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveRefreshUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have refresh URL {0}{reason}", expected)
            .ForCondition(Subject.RefreshUrl == expected)
            .FailWith(", but found {0}.", Subject.RefreshUrl)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOAuthFlowAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveTokenUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveTokenUrl(new Uri(expected), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> HaveTokenUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have token URL {0}{reason}", expected)
            .ForCondition(Subject.TokenUrl == expected)
            .FailWith(", but found {0}.", Subject.TokenUrl)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOAuthFlowAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiOAuthFlowAssertions> OnlyContainScopes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain scopes {0}{reason}", expected)
            .Given(expected.ToList)
            .ForCondition(e => Subject.Scopes.Count > 0 || e.Count == 0)
            .FailWith(", but found no scopes.")
            .Then
            .ForCondition(e => Subject.Scopes.Keys.Order().SequenceEqual(e.Order()))
            .FailWith(", but found {0}.", Subject.Scopes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiOAuthFlowAssertions>(this);
    }
}
