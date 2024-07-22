using NUnit.Framework;
using MarsSpecFlowProject.Context;
using MarsSpecFlowProject.Pages;
using MarsSpecFlowProject.Utilities;
using System;
using TestContext = MarsSpecFlowProject.Context.TestContext;
using System.Reflection.Emit;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Security.Cryptography;
using Gherkin;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageFeature_StepDefinitions;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForLanguageFeature_StepDefinitions
    {

        private Login loginPage;
        private LanguageProfile languageworkflow;
        private string tab;
        private AssertionUtils Assertions;
        private readonly TestContext testContext;
        private readonly ScenarioContext scenario_Context;
        public ThisTestSuiteContainsTestScenariosForLanguageFeature_StepDefinitions(TestContext context, ScenarioContext scenarioContext)
        {
            loginPage = new Login();
            languageworkflow = new LanguageProfile();
            testContext = context;
            Assertions = new AssertionUtils();
            scenario_Context = scenarioContext;

        }

     
        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and  navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, string languages)
        {
            loginPage.loginPage(UserName, Password);
            
        }

        [Given(@"User has no language in their profile")]
        public void GivenUserHasNoLanguageInTheirProfile()
        {
            languageworkflow.DeleteAllElements();

            Thread.Sleep(2000);
        }

        [When(@"I create a new language record '([^']*)' '([^']*)'")]
        public void WhenICreateANewLanguageRecord(string Language, string Level)
        {
            languageworkflow.Add(Language, Level);
            Thread.Sleep(1000);
            testContext.Updated_Language.Add(Language);
            scenario_Context["LanguageAdded"] = Language;
        }

        [Then(@"the record should be saved '([^']*)'")]
        public void ThenTheRecordShouldBeSaved(string language)
        {
            Assertions.AddDeleteLanguageAssert(language);
        }

        [Then(@"the record should not be saved '([^']*)'")]
        public void ThenTheRecordShouldNotBeSaved(string language)
        {
            Assertions.AddDeleteLanguageAssert(language);
            scenario_Context["LanguageAdded"] = language;
        }

        [Given(@"the user profile is set up with the languages:")]
        public void GivenTheUserProfileIsSetUpWithTheLanguages(Table table)
        {
            Thread.Sleep(1000);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile

                languageworkflow.Add(language.Languages, language.Level);
                testContext.Added_Language.Add(language.Languages);
                scenario_Context["LanguageAdded"] = language.Languages;
                Thread.Sleep(3000);
                
            }



        }

        public class Language
        {
            public string Languages { get; set; }
            public string Level { get; set; }
        }

        [When(@"the user wants to update the language or level from ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)""")]
        public void WhenTheUserWantsToUpdateTheLanguageOrLevelFromTo(string Language, string Level, string NewLanguage, string NewLevel)
        {
            languageworkflow.Update(Language, NewLanguage, NewLevel);
            testContext.AddLanguageUpdated(Language, NewLanguage);
            testContext.RemoveLanguage(Language);
            scenario_Context["LanguageAdded"] = Language;

        }

        [Then(@"the update from  ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromToIsPossible(string language, string level, string newlanguage, string newlevel)
        {
            Assertions.UpdateAssertionsLanguage(language, newlanguage);
            scenario_Context["LanguageAdded"] = language;

        }

        [When(@"the user wants to delete the language  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheLanguage(string language)
        {
            Thread.Sleep(4000);
            languageworkflow.delete(language);
            testContext.RemoveLanguage(language);

        }

        [Then(@"the language ""([^""]*)"" should be deleted\.")]
        public void ThenTheLanguageShouldBeDeleted_(string language)
        {
            Assertions.AddDeleteLanguageAssert(language);
            scenario_Context["LanguageAdded"] = language;
        }

        [When(@"I try to create another record with same value '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameValue(string language, string level)
        {
            Thread.Sleep(3000);
            languageworkflow.Add(language, level);
            testContext.Added_Language.Add(language);
            scenario_Context["LanguageAdded"] = language;
        }

        [Then(@"Adding of second record '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordFails(string language, string level)
        {
            Assertions.AddDeleteLanguageAssert(language);
            scenario_Context["LanguageAdded"] = language;

        }



        [Then(@"the system should block the updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheUpdationFromTo_(string Language, string newlanguage)
        {
            Assertions.UpdateAssertionsLanguage(Language, newlanguage);

            scenario_Context["LanguageAdded"] = Language;
        }

        [Given(@"I open a second session in tab (.*)\.")]
        public void GivenIOpenASecondSessionInTab_(int SID)
        {
            WindowHandlers.NewTab();

            languageworkflow.Sessions(SID);
            languageworkflow.Url();
        }

        [Given(@"the user profile is set up with the languages in Session (.*):")]
        public void GivenTheUserProfileIsSetUpWithTheLanguagesInSession(int SID, Table table)
        {
            languageworkflow.Sessions(SID);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile
                languageworkflow.Add(language.Languages, language.Level);

                testContext.Added_Language.Add(language.Languages);
                scenario_Context["LanguageAdded"] = language.Languages;
                Thread.Sleep(1000);

            }
        }


        [When(@"I create a new language record '([^']*)' '([^']*)' in Session (.*)")]
        public void WhenICreateANewLanguageRecordInSession(string language, string level, int SID)
        {
            languageworkflow.Sessions(SID);
            Thread.Sleep(2000);
            languageworkflow.Add(language, level);
            testContext.Added_Language.Add(language);
            scenario_Context["LanguageAdded"] = language;
        }


        [Then(@"the entry of '([^']*)','([^']*)' should be blocked\.")]
        public void ThenTheEntryOfShouldBeBlocked_(string Language, string Level)
        {
            Assertions.AddDeleteLanguageAssert(Language);

        }

        [When(@"I create a new language with (.*) random charcaters and level '([^']*)'")]
        public void WhenICreateANewLanguageWithRandomCharcatersAndLevel(int length, string Level)
        {

            string randomString = StringUtilities.GenerateRandomString(length);
            languageworkflow.Add(randomString, Level);
            testContext.Added_Language.Add(randomString);
            scenario_Context["LanguageAdded"] = randomString;
        }



        [Then(@"the addition of language with more than (.*) characters should fail")]
        public void ThenTheAdditionOfLanguageWithMoreThanCharactersShouldFail(int p0)
        {

            Assertions.StringLengthAssertion_Language();
            

        }


        }
    }

