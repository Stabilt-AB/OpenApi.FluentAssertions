using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiPathItemAssertions(OpenApiPathItem value)
    : ObjectAssertions<OpenApiPathItem, OpenApiPathItemAssertions>(value)
{
    [CustomAssertion]
    public AndWhichConstraint<OpenApiPathItemAssertions, OpenApiOperation> ContainOperation(
        OperationType expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain an operation of type {0}{reason}", expected.ToString())
            .ForCondition(Subject.Operations.Any())
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(Subject.Operations.ContainsKey(expected))
            .FailWith(", but found {0}.", Subject.Operations.Keys.Select(x => x.ToString()))
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiPathItemAssertions, OpenApiOperation>(this, Subject.Operations[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiPathItemAssertions> ContainOperations(
        IEnumerable<OperationType> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<OperationType> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain operations {0}{reason}", expected.Select(x => x.ToString()))
            .ForCondition(Subject.Operations.Any() || !expected.Any())
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(expected.All(Subject.Operations.ContainsKey))
            .FailWith(", but found {0}.", Subject.Operations.Keys.Select(x => x.ToString()))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiPathItemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiPathItemAssertions> HaveOperationCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain {0} operations{reason}", expected)
            .ForCondition(Subject.Operations.Any() || expected == 0)
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(Subject.Operations.Keys.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.Operations.Keys.Count, Subject.Operations.Keys.Select(x => x.ToString()))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiPathItemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiPathItemAssertions> OnlyContainOperations(
        IEnumerable<OperationType> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<OperationType> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain operations {0}{reason}", expected.Select(x => x.ToString()))
            .ForCondition(Subject.Operations.Any() || !expected.Any())
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(Subject.Operations.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", Subject.Operations.Keys.Select(x => x.ToString()))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiPathItemAssertions>(this);
    }
}
