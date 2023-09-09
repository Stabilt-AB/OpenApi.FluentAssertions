using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiDocumentAssertions(OpenApiDocument value)
    : ObjectAssertions<OpenApiDocument, OpenApiDocumentAssertions>(value)
{
    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiOperation> ContainOperation(
        OperationType expectedOperationType,
        String expectedPath,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a path {0} with operation {1}{reason}", expectedPath, expectedOperationType.ToString())
            .Given(() => Subject.Paths)
            .ForCondition(paths => paths?.Count > 0)
            .FailWith(", but found no paths.")
            .Then
            .ForCondition(paths => paths.ContainsKey(expectedPath))
            .FailWith(", but found paths {0}.", Subject.Paths.Keys.Select(x => x.ToString()))
            .Then
            .Given(paths => paths[expectedPath].Operations)
            .ForCondition(operations => operations?.Count > 0)
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(operations => operations.ContainsKey(expectedOperationType))
            .FailWith(", but found operations {0}.", operations => operations.Keys.Select(x => x.ToString()))
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiOperation>(this, Subject.Paths[expectedPath].Operations[expectedOperationType]);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiPathItem> ContainPath(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a path {0}{reason}", expected)
            .Given(() => Subject.Paths)
            .ForCondition(paths => paths?.Count > 0)
            .FailWith(", but found no paths.")
            .Then
            .ForCondition(paths => paths.ContainsKey(expected))
            .FailWith(", but found {0}.", paths => paths.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiPathItem>(this, Subject.Paths[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> ContainPaths(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        expected = expected as ICollection<String> ?? expected.ToList();

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain paths {0}{reason}", expected)
            .Given(() => Subject.Paths ?? new OpenApiPaths())
            .ForCondition(paths => paths.Count > 0 || !expected.Any())
            .FailWith(", but found no paths.")
            .Then
            .ForCondition(paths => expected.All(paths.ContainsKey))
            .FailWith(", but found {0}.", schemas => schemas!.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSchema> ContainSchema(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a schema {0}{reason}", expected)
            .Given(() => Subject.Components?.Schemas)
            .ForCondition(schemas => schemas?.Count > 0)
            .FailWith(", but found no schemas.")
            .Then
            .Given(schemas => schemas!)
            .ForCondition(schemas => schemas.ContainsKey(expected))
            .FailWith(", but found {0}.", schemas => schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSchema>(this, Subject.Components.Schemas[expected]!);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> ContainSchemas(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain schemas {0}{reason}", expected)
            .Given(() => Subject.Components?.Schemas)
            .ForCondition(schemas => schemas?.Count > 0)
            .FailWith(", but found no schemas.")
            .Then
            .Given(schemas => schemas!)
            .ForCondition(schemas => expected.All(schemas.ContainsKey))
            .FailWith(", but found {0}.", schemas => schemas!.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSecurityScheme> ContainSecurityRequirementReference(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a security requirement {0}{reason}", expected)
            .ForCondition(Subject.SecurityRequirements.Count > 0)
            .FailWith(", but found no security requirements.")
            .Then
            .Given(() => Subject.SecurityRequirements.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .ForCondition(ids => ids.Contains(expected))
            .FailWith(", but found {0}.", ids => ids)
            .Then
            .ClearExpectation();

        var scheme = Subject.SecurityRequirements.SelectMany(x => x.Keys).First(x => x.Reference.Id == expected);

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSecurityScheme>(this, scheme);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> ContainSecurityRequirementReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain security requirements {0}{reason}", expected)
            .Given(() => Subject.SecurityRequirements)
            .ForCondition(requirements => requirements.Count > 0)
            .FailWith(", but found no security requirements.")
            .Then
            .Given(requirements => requirements.SelectMany(x => x.Keys).Select(x => x.Reference.Id))
            .ForCondition(referenceIds => expected.All(referenceIds.Contains))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSecurityScheme> ContainSecurityScheme(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a security scheme {0}{reason}", expected)
            .Given(() => Subject.Components?.SecuritySchemes)
            .ForCondition(schemes => schemes?.Count > 0)
            .FailWith(", but found no security schemes.")
            .Then
            .Given(schemes => schemes!)
            .ForCondition(schemes => schemes.ContainsKey(expected))
            .FailWith(", but found {0}.", schemes => schemes.Keys)
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiSecurityScheme>(this, Subject.Components!.SecuritySchemes[expected]);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> ContainSecuritySchemes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain security schemes {0}{reason}", expected)
            .Given(() => Subject.Components?.SecuritySchemes)
            .ForCondition(schemes => schemes?.Count > 0)
            .FailWith(", but found no security schemes.")
            .Then
            .Given(schemes => schemes!)
            .ForCondition(schemes => expected.All(schemes.ContainsKey))
            .FailWith(", but found {0}.", schemes => schemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiServer> ContainServerDescription(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        var server = Subject.Servers.FirstOrDefault(x => x.Description == expected);

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have a server with description {0}{reason}", expected)
            .ForCondition(Subject.Servers.Count > 0)
            .FailWith(", but found no servers.")
            .Then
            .ForCondition(server != null)
            .FailWith(", but found {0}.", Subject.Servers.Select(x => x.Description))
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiServer>(this, server!);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiDocumentAssertions, OpenApiServer> ContainServerUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to contain a server with URL {0}{reason}", expected)
            .ForCondition(Subject.Servers.Count > 0)
            .FailWith(", but found no servers.")
            .Then
            .ForCondition(Subject.Servers.Any(x => x.Url == expected))
            .FailWith(", but found {0}.", Subject.Servers.Select(x => x.Url))
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiDocumentAssertions, OpenApiServer>(this, Subject.Servers.First(x => x.Url == expected));
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveContactEmail(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have contact email {0}{reason}", expected)
            .Given(() => Subject.Info?.Contact?.Email)
            .ForCondition(email => email == expected)
            .FailWith(", but found {0}.", email => email)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveContactName(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have contact name {0}{reason}", expected)
            .Given(() => Subject.Info?.Contact?.Name)
            .ForCondition(name => name == expected)
            .FailWith(", but found {0}.", name => name)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveContactUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
        => HaveContactUrl(new Uri(expected), because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveContactUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have contact URL {0}{reason}", expected)
            .Given(() => Subject.Info?.Contact?.Url)
            .ForCondition(url => url == expected)
            .FailWith(", but found {0}.", url => url)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveLicenseName(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have license name {0}{reason}", expected)
            .Given(() => Subject.Info?.License?.Name)
            .ForCondition(name => name == expected)
            .FailWith(", but found {0}.", name => name)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveLicenseUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
        => HaveLicenseUrl(new Uri(expected), because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveLicenseUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have license URL {0}{reason}", expected)
            .Given(() => Subject.Info?.License?.Url)
            .ForCondition(url => url == expected)
            .FailWith(", but found {0}.", url => url)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveOperationCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} paths and operations{reason}", expected)
            .Given(() => Subject.Paths ?? new OpenApiPaths())
            .Given(paths => paths.SelectMany(p => p.Value.Operations.Select(o => $"{o.Key.ToString().ToUpperInvariant()} {p.Key}")).Order().ToList())
            .ForCondition(operations => operations.Count > 0 || expected == 0)
            .FailWith(", but found no operations.")
            .Then
            .ForCondition(operations => operations.Count == expected)
            .FailWith(", but found {0}: {1}.", operations => operations.Count, operations => operations)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HavePathCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} paths{reason}", expected)
            .Given(() => Subject.Paths ?? new OpenApiPaths())
            .ForCondition(paths => paths.Count > 0 || expected == 0)
            .FailWith(", but found no paths.")
            .Then
            .ForCondition(paths => paths.Count == expected)
            .FailWith(", but found {0}: {1}.", paths => paths.Count, paths => paths.Keys.Order())
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveSchemaCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} schemas{reason}", expected)
            .Given(() => Subject.Components?.Schemas ?? new Dictionary<String, OpenApiSchema>())
            .ForCondition(schemas => schemas.Count > 0 || expected == 0)
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(schemas => schemas.Count == expected)
            .FailWith(", but found {0}: {1}.", schemas => schemas.Count, schemas => schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveSecuritySchemeCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} security schemes{reason}", expected)
            .Given(() => Subject.Components?.SecuritySchemes ?? new Dictionary<String, OpenApiSecurityScheme>())
            .ForCondition(schemes => schemes.Count > 0 || expected == 0)
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(schemes => schemes.Count == expected)
            .FailWith(", but found {0}: {1}.", schemes => schemes.Count, schemes => schemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveServerCount(
        Int32 expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have {0} servers{reason}", expected)
            .ForCondition(Subject.Servers.Count > 0 || expected == 0)
            .FailWith(", but found no servers.")
            .Then
            .ForCondition(Subject.Servers.Count == expected)
            .FailWith(", but found {0}: {1}.", Subject.Servers.Count, Subject.Servers.Select(s => s.Url))
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveTermsOfService(
        String expected,
        String because = "",
        params Object[] becauseArgs) =>
        HaveTermsOfService(new Uri(expected), because, becauseArgs);

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveTermsOfService(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have terms of service URL {0}{reason}", expected)
            .Given(() => Subject.Info?.TermsOfService)
            .ForCondition(uri => uri == expected)
            .FailWith(", but found {0}.", uri => uri)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveTitle(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have title {0}{reason}", expected)
            .Given(() => Subject.Info?.Title)
            .ForCondition(title => title == expected)
            .FailWith(", but found {0}.", title => title)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> HaveVersion(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have version {0}{reason}", expected)
            .Given(() => Subject.Info?.Version)
            .ForCondition(version => version == expected)
            .FailWith(", but found {0}.", version => version)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> OnlyContainPaths(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain paths {0}{reason}", expected)
            .Given(() => (Paths: Subject.Paths ?? new OpenApiPaths(), Expected: expected.ToList()))
            .ForCondition(x => x.Paths.Count > 0 || x.Expected.Count == 0)
            .FailWith(", but found no paths.")
            .Then
            .ForCondition(x => x.Paths.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", x => x.Paths.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> OnlyContainSchemas(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain schemas {0}{reason}", expected)
            .Given(() => (Schemas: Subject.Components?.Schemas ?? new Dictionary<String, OpenApiSchema>(), Expected: expected.ToList()))
            .ForCondition(x => x.Schemas.Count > 0 || x.Expected.Count == 0)
            .FailWith(", but found no schemas.")
            .Then
            .ForCondition(x => x.Schemas.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", x => x.Schemas.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> OnlyContainSecurityRequirementReferences(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain security requirements {0}{reason}", expected)
            .Given(() => (Requirements: Subject.SecurityRequirements, Expected: expected.ToList()))
            .ForCondition(x => x.Requirements.Count > 0 || x.Expected.Count == 0)
            .FailWith(", but found no security requirements.")
            .Then
            .Given(x => x.Requirements.SelectMany(y => y.Keys).Select(y => y.Reference.Id))
            .ForCondition(referenceIds => referenceIds.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", referenceIds => referenceIds)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> OnlyContainSecuritySchemes(
        IEnumerable<String> expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain security schemes {0}{reason}", expected)
            .Given(() => (Schemes: Subject.Components?.SecuritySchemes ?? new Dictionary<String, OpenApiSecurityScheme>(), Expected: expected.ToList()))
            .ForCondition(x => x.Schemes.Count > 0 || x.Expected.Count == 0)
            .FailWith(", but found no security schemes.")
            .Then
            .ForCondition(x => x.Schemes.Keys.Order().SequenceEqual(expected.Order()))
            .FailWith(", but found {0}.", x => x.Schemes.Keys)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiDocumentAssertions> OnlyHaveUniqueServerUrls(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to only contain unique server URLs{reason}")
            .Given(() => Subject.Servers
                .GroupBy(x => x.Url)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList())
            .ForCondition(duplicates => duplicates.Count == 0)
            .FailWith(", but found multiple of {0}.", duplicates => duplicates)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiDocumentAssertions>(this);
    }
}
