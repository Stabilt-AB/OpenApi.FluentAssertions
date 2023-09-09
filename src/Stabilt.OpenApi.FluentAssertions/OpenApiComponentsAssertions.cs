using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiComponentsAssertions(OpenApiComponents value)
    : ObjectAssertions<OpenApiComponents, OpenApiComponentsAssertions>(value)
{
    [CustomAssertion]
    public AndWhichConstraint<OpenApiComponentsAssertions, OpenApiSchema> ContainSchema(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a schema {0}{reason}", expected)
            .ForCondition(Subject.Schemas.Any())
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(Subject.Schemas.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.Schemas.Keys.Order())
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiComponentsAssertions, OpenApiSchema>(this, Subject.Schemas[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> ContainSchemas(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain schemas {0}{reason}", expected)
            .ForCondition(Subject.Schemas.Any())
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(expected.All(Subject.Schemas.ContainsKey))
            .FailWith(", but found {0}.", Subject.Schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiComponentsAssertions, OpenApiSecurityScheme> ContainSecurityScheme(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a security scheme {0}{reason}", expected)
            .ForCondition(Subject.SecuritySchemes.Any())
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(Subject.SecuritySchemes.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.SecuritySchemes.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiComponentsAssertions, OpenApiSecurityScheme>(this, Subject.SecuritySchemes[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> ContainSecuritySchemes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain security schemes {0}{reason}", expected)
            .ForCondition(Subject.SecuritySchemes.Any())
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(expected.All(Subject.SecuritySchemes.ContainsKey))
            .FailWith(", but found {0}.", Subject.SecuritySchemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> HaveSchemaCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} schemas{reason}", expected)
            .ForCondition(Subject.Schemas.Any() || expected == 0)
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(Subject.Schemas.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.Schemas.Count, Subject.Schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> HaveSecuritySchemeCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} security schemes{reason}", expected)
            .ForCondition(Subject.SecuritySchemes.Any() || expected == 0)
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(Subject.SecuritySchemes.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.SecuritySchemes.Count, Subject.SecuritySchemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> NotContainAnySecuritySchemes(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any security schemes{reason}")
            .ForCondition(!Subject.SecuritySchemes.Any())
            .FailWith(", but found {0}.", Subject.SecuritySchemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> NotContainAnySchemas(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to not contain any schemas{reason}")
            .ForCondition(!Subject.Schemas.Any())
            .FailWith(", but found {0}.", Subject.Schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> OnlyContainSchemas(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain schemas {0}{reason}", expected)
            .Given(expected.ToList)
            .ForCondition(x => Subject.Schemas.Any() || x.Count == 0)
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(x => Subject.Schemas.Keys.Order().SequenceEqual(x.Order()))
            .FailWith(", but found {0}.", Subject.Schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiComponentsAssertions> OnlyContainSecuritySchemes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain security schemes {0}{reason}", expected)
            .Given(expected.ToList)
            .ForCondition(x => Subject.SecuritySchemes.Any() || x.Count == 0)
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(x => Subject.SecuritySchemes.Keys.Order().SequenceEqual(x.Order()))
            .FailWith(", but found {0}.", Subject.SecuritySchemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiComponentsAssertions>(this);
    }
}
