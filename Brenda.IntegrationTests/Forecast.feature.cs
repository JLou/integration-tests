﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.4.0.0
//      SpecFlow Generator Version:3.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Brenda.IntegrationTests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Xunit.TraitAttribute("Category", "ignore")]
    public partial class JokeFeature : object, Xunit.IClassFixture<JokeFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "ignore"};
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Forecast.feature"
#line hidden
        
        public JokeFeature(JokeFeature.FixtureData fixtureData, Brenda_IntegrationTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Joke", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, new string[] {
                        "ignore"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Forecast for 3 days")]
        [Xunit.TraitAttribute("FeatureTitle", "Joke")]
        [Xunit.TraitAttribute("Description", "Forecast for 3 days")]
        [Xunit.TraitAttribute("Category", "ignore")]
        public virtual void ForecastFor3Days()
        {
            string[] tagsOfScenario = new string[] {
                    "ignore"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Forecast for 3 days", null, tagsOfScenario, argumentsOfScenario);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "ID",
                            "Date",
                            "TemperatureC"});
                table1.AddRow(new string[] {
                            "1",
                            "2020-05-01",
                            "20"});
                table1.AddRow(new string[] {
                            "2",
                            "2020-01-01",
                            "15"});
                table1.AddRow(new string[] {
                            "3",
                            "2020-03-01",
                            "17"});
                table1.AddRow(new string[] {
                            "4",
                            "2020-02-01",
                            "16"});
                table1.AddRow(new string[] {
                            "5",
                            "2020-06-01",
                            "21"});
#line 9
 testRunner.Given("I know about the following forecast", ((string)(null)), table1, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "joke"});
                table2.AddRow(new string[] {
                            "joke1"});
                table2.AddRow(new string[] {
                            "joke2"});
                table2.AddRow(new string[] {
                            "joke3"});
                table2.AddRow(new string[] {
                            "joke4"});
                table2.AddRow(new string[] {
                            "joke5"});
#line 16
 testRunner.And("And my joke provider has given me these jokes", ((string)(null)), table2, "And ");
#line hidden
#line 23
 testRunner.When("I ask Brenda for a forecast", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Date",
                            "TemperatureC",
                            "TemperatureF",
                            "Summary"});
                table3.AddRow(new string[] {
                            "2020-01-01",
                            "15",
                            "58",
                            "joke1"});
                table3.AddRow(new string[] {
                            "2020-02-01",
                            "16",
                            "60",
                            "joke2"});
                table3.AddRow(new string[] {
                            "2020-03-01",
                            "17",
                            "62",
                            "joke3"});
                table3.AddRow(new string[] {
                            "2020-05-01",
                            "20",
                            "67",
                            "joke4"});
                table3.AddRow(new string[] {
                            "2020-06-01",
                            "21",
                            "69",
                            "joke5"});
#line 24
 testRunner.Then("the results are", ((string)(null)), table3, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                JokeFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                JokeFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
