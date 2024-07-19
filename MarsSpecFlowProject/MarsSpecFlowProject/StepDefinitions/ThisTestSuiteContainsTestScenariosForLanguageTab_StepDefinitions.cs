using System;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utils;
using MarsSpecFlowProject.Page;
using OpenQA.Selenium.Chrome;
using System.Reflection.Emit;
using TechTalk.SpecFlow.Assist;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using OpenQA.Selenium;
using System.Security.Policy;
using NUnit.Framework.Internal.Execution;
using Gherkin;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions : BasePage
    {



        private Login loginPage;
        private LanguageWorkFlow languageworkflow;
        private string tab;
        private Assertions Assertions;

        public ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions()
        {
            loginPage = new Login();
            languageworkflow = new LanguageWorkFlow();
            Assertions = new Assertions();
        }



        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and  navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, String tab)
        {
            loginPage.loginPage(UserName, Password);

            //this.tab = tab;
            //languageworkflow.InitChoice(tab);

        }



        [Given(@"User has no language in their profile")]
        public void GivenUserHasNoLanguageInTheirProfile()
        {
            LanguageWorkFlow.DeleteElements();

            Thread.Sleep(2000);


        }


        [When(@"I create a new language record '([^']*)' '([^']*)'")]
        public void WhenICreateANewLanguageRecord(string Language, string Level)
        {
            languageworkflow.Add(Language, Level);
            Thread.Sleep(1000);


        }

        [Then(@"the record should be saved '([^']*)'")]
        public void ThenTheRecordShouldBeSaved(string language)
        {

            Assertions.AddDeleteLanguageAssert(language);



        }
        [Then(@"the record should not be saved '([^']*)'")]
        public void ThenTheRecordShouldNotBeSaved(string Language)
        {
            //Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(Language);
        }


        [Given(@"the user profile is set up with the languages:")]
        public void GivenTheUserProfileIsSetUpWithTheLanguages(Table table)
        {

            Thread.Sleep(1000);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile

                languageworkflow.Add(language.language, language.Level);
                Thread.Sleep(3000);
            }



        }

        public class Language
        {
            public string language { get; set; }
            public string Level { get; set; }
        }

        [When(@"the user wants to update the language or level from ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)""")]
        public void WhenTheUserWantsToUpdateTheLanguageOrLevelFromTo(string language, string languagelevel, string newlanguage, string newlanguagelevel)
        {
            Thread.Sleep(3000);

            languageworkflow.Update(language, newlanguage, newlanguagelevel);
        }




        [Then(@"the update from  ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromToIsPossible(string language, string languagelevel, string newlanguage, string newlanguagelevel)
        {

            Assertions.UpdateAssertionsLanguage(language, newlanguage);
        }


        [When(@"the user wants to delete the language  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheLanguage(string language)
        {
            Thread.Sleep(2000);
            languageworkflow.delete(language);
        }

        [Then(@"the language ""([^""]*)"" should be deleted\.")]
        public void ThenTheLanguageShouldBeDeleted_(string language)
        {
            //Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(language);

        }

        [When(@"I try to create another record with same value '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameValue(string language, string LanguageLevel)
        {
            Thread.Sleep(5000);
            languageworkflow.Add(language, LanguageLevel);


        }

        [Then(@"Adding of second record '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordFails(string language, string LanguageLevel)
        {

            //Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(language);


        }

        [Then(@"the system should block the updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheUpdationFromTo_(string Language, string newlanguage)
        {

            Assertions.UpdateAssertionsLanguage(Language, newlanguage);
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
                languageworkflow.Add(language.language, language.Level);
                Thread.Sleep(3000);
            }
        }

        [When(@"I create a new language record '([^']*)' '([^']*)' in Session (.*)")]
        public void WhenICreateANewLanguageRecordInSession(string language, string languageLevel, int SID)
        {

            languageworkflow.Sessions(SID);
            Thread.Sleep(2000);
            languageworkflow.Add(language, languageLevel);
            
        }



        [Then(@"the entry of '([^']*)','([^']*)' should be blocked\.")]
        public void ThenTheEntryOfShouldBeBlocked_(string Language, string LanguageLevel)
        {
            //Assertions.InitChoice(tab);

            Assertions.AddDeleteLanguageAssert(Language);
        }


        [When(@"I create a new language with (.*) random charcaters and level '([^']*)'")]
        public void WhenICreateANewLanguageWithRandomCharcatersAndLevel(int length, string Level)
        {
            string randomString = StringUtilities.GenerateRandomString(length);
            languageworkflow.Add(randomString, Level);
        }




        [Then(@"the addition of language with more than (.*) characters should fail")]
        public void ThenTheAdditionOfLanguageWithMoreThanCharactersShouldFail(int p0)
        {

            //Assertions.InitChoice(tab);
            Assertions.StringLengthAssertion_Language();

        }



        [AfterScenario]
        public void CleanupData()
        {

            LanguageWorkFlow.DeleteElements();
            driver.Quit();


            driver = null;

        }

    }
}
