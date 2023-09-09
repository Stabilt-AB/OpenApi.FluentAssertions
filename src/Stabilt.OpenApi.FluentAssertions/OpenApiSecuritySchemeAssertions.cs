using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.OpenApi.Models;

namespace Stabilt.OpenApi.FluentAssertions;

public class OpenApiSecuritySchemeAssertions(OpenApiSecurityScheme value)
    : ObjectAssertions<OpenApiSecurityScheme, OpenApiSecuritySchemeAssertions>(value)
{
    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeApiKeyType(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeType(SecuritySchemeType.ApiKey, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeHttpType(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeType(SecuritySchemeType.Http, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeIn(
        ParameterLocation expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to be in {0}{reason}", expected.ToString())
            .ForCondition(Subject.In == expected)
            .FailWith(", but found {0}.", Subject.In.ToString())
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeInCookie(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeIn(ParameterLocation.Cookie, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeInHeader(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeIn(ParameterLocation.Header, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeInPath(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeIn(ParameterLocation.Path, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeInQuery(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeIn(ParameterLocation.Query, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeOAuth2Type(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeType(SecuritySchemeType.OAuth2, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeOpenIdConnectType(
        String because = "",
        params Object[] becauseArgs)
    {
        return BeType(SecuritySchemeType.OpenIdConnect, because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> BeType(
        SecuritySchemeType expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have type {0}{reason}", expected.ToString())
            .ForCondition(Subject.Type == expected)
            .FailWith(", but found {0}.", Subject.Type.ToString())
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveBearerFormat(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have bearer format {0}{reason}", expected)
            .ForCondition(Subject.BearerFormat == expected)
            .FailWith(", but found {0}.", Subject.BearerFormat)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveName(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have name {0}{reason}", expected)
            .ForCondition(Subject.Name == expected)
            .FailWith(", but found {0}.", Subject.Name)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow> HaveOAuth2AuthorizationCodeFlow(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OAuth2 authorization code flow{reason}")
            .ForCondition(Subject.Flows != null)
            .FailWith(", but found no OAuth2 flows.")
            .Then
            .ForCondition(Subject.Flows?.AuthorizationCode != null)
            .FailWith(", but it hadn't.")
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow>(this, Subject.Flows!.AuthorizationCode);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow> HaveOAuth2ClientCredentialsFlow(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OAuth2 client credentials flow{reason}")
            .ForCondition(Subject.Flows != null)
            .FailWith(", but found no OAuth2 flows.")
            .Then
            .ForCondition(Subject.Flows?.ClientCredentials != null)
            .FailWith(", but it hadn't.")
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow>(this, Subject.Flows!.ClientCredentials);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow> HaveOAuth2ImplicitFlow(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OAuth2 implicit flow{reason}")
            .ForCondition(Subject.Flows != null)
            .FailWith(", but found no OAuth2 flows.")
            .Then
            .ForCondition(Subject.Flows?.Implicit != null)
            .FailWith(", but it hadn't.")
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow>(this, Subject.Flows!.Implicit);
    }

    [CustomAssertion]
    public AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow> HaveOAuth2PasswordFlow(
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OAuth2 password flow{reason}")
            .ForCondition(Subject.Flows != null)
            .FailWith(", but found no OAuth2 flows.")
            .Then
            .ForCondition(Subject.Flows?.Password != null)
            .FailWith(", but it hadn't.")
            .Then
            .ClearExpectation();

        return new AndWhichConstraint<OpenApiSecuritySchemeAssertions, OpenApiOAuthFlow>(this, Subject.Flows!.Password);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveOpenIdConnectUrl(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        return HaveOpenIdConnectUrl(new Uri(expected), because, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveOpenIdConnectUrl(
        Uri expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have OpenID Connect URL {0}{reason}", expected)
            .ForCondition(Subject.OpenIdConnectUrl == expected)
            .FailWith(", but found {0}.", Subject.OpenIdConnectUrl)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveReferenceTo(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have reference with ID {0}{reason}", expected)
            .Given(() => Subject.Reference?.Id)
            .ForCondition(referenceId => referenceId == expected)
            .FailWith(", but found {0}.", referenceId => referenceId)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<OpenApiSecuritySchemeAssertions> HaveScheme(
        String expected,
        String because = "",
        params Object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .WithExpectation("Expected {context:value} to have scheme {0}{reason}", expected)
            .ForCondition(Subject.Scheme == expected)
            .FailWith(", but found {0}.", Subject.Scheme)
            .Then
            .ClearExpectation();

        return new AndConstraint<OpenApiSecuritySchemeAssertions>(this);
    }
}
