﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Fiap.BDD.Tests.Features.Auth
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class UserLoginFeature : object, Xunit.IClassFixture<UserLoginFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Login.feature"
#line hidden
        
        public UserLoginFeature(UserLoginFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly();
            global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Auth", "User login", "  Validates different login behaviors based on provided credentials and user type" +
                    ".", global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            await testRunner.OnFeatureEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
            testRunner = null;
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with valid admin credentials")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with valid admin credentials")]
        public async System.Threading.Tasks.Task LoginWithValidAdminCredentials()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with valid admin credentials", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 5
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 6
    await testRunner.GivenAsync("the user provided the username \"admin\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 7
    await testRunner.AndAsync("the password \"123456\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 8
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 9
    await testRunner.ThenAsync("the result should contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with valid regular user credentials")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with valid regular user credentials")]
        public async System.Threading.Tasks.Task LoginWithValidRegularUserCredentials()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with valid regular user credentials", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 11
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 12
    await testRunner.GivenAsync("the user provided the username \"user\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 13
    await testRunner.AndAsync("the password \"654321\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 14
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 15
    await testRunner.ThenAsync("the result should contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with invalid password for admin")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with invalid password for admin")]
        public async System.Threading.Tasks.Task LoginWithInvalidPasswordForAdmin()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with invalid password for admin", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 18
    await testRunner.GivenAsync("the user provided the username \"admin\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 19
    await testRunner.AndAsync("the password \"wrong\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 20
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 21
    await testRunner.ThenAsync("the result should not contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with invalid password for regular user")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with invalid password for regular user")]
        public async System.Threading.Tasks.Task LoginWithInvalidPasswordForRegularUser()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with invalid password for regular user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 23
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 24
    await testRunner.GivenAsync("the user provided the username \"user\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 25
    await testRunner.AndAsync("the password \"wrong\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 26
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 27
    await testRunner.ThenAsync("the result should not contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with non-existent user")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with non-existent user")]
        public async System.Threading.Tasks.Task LoginWithNon_ExistentUser()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with non-existent user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 29
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 30
    await testRunner.GivenAsync("the user provided the username \"doesNotExist\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 31
    await testRunner.AndAsync("the password \"any\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 32
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 33
    await testRunner.ThenAsync("the result should not contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login with empty fields")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login with empty fields")]
        public async System.Threading.Tasks.Task LoginWithEmptyFields()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login with empty fields", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 35
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 36
    await testRunner.GivenAsync("the user provided the username \"\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 37
    await testRunner.AndAsync("the password \"\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 38
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 39
    await testRunner.ThenAsync("the result should not contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Internal error during login")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Internal error during login")]
        public async System.Threading.Tasks.Task InternalErrorDuringLogin()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Internal error during login", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 41
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 42
    await testRunner.GivenAsync("the user provided the username \"error\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 43
    await testRunner.AndAsync("the password \"123\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 44
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 45
    await testRunner.ThenAsync("an exception should be thrown", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Login attempt with disabled account")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Login attempt with disabled account")]
        public async System.Threading.Tasks.Task LoginAttemptWithDisabledAccount()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Login attempt with disabled account", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 47
   this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 48
     await testRunner.GivenAsync("the user provided the username \"inactive\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 49
     await testRunner.AndAsync("the password \"123456\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 50
     await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 51
     await testRunner.ThenAsync("the result should not contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
#line 52
     await testRunner.AndAsync("an error notification with message \"Your account is disabled. Please contact supp" +
                        "ort.\" should be recorded", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Token expiration should be in the future")]
        [Xunit.TraitAttribute("FeatureTitle", "User login")]
        [Xunit.TraitAttribute("Description", "Token expiration should be in the future")]
        public async System.Threading.Tasks.Task TokenExpirationShouldBeInTheFuture()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Token expiration should be in the future", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 55
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 56
    await testRunner.GivenAsync("the user provided the username \"admin\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 57
    await testRunner.AndAsync("the password \"123456\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 58
    await testRunner.WhenAsync("the user requests login", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 59
    await testRunner.ThenAsync("the result should contain a JWT token", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
#line 60
    await testRunner.AndAsync("the token expiration should be in the future", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await UserLoginFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await UserLoginFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
