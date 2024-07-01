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
using MarsSpecFlowProject.Helpers;
using SpecFlowMVPMARS.Page;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions : CommonDriver
    {

        Login loginobj = new Login();

    LanguageWorkFlow  langobj = new LanguageWorkFlow();


        [BeforeScenario]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Url = "http://localhost:5000/";
            driver.Manage().Window.Maximize();
            

        }


        
        
       

        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)'")]
        public void GivenILogIntoThePortalWithUserNameAndPassword(string UserName, string Password)
        {
            loginobj.loginAction(driver, UserName, Password);
        }




        [Given(@"User has no language in their profile")]
        public void GivenUserHasNoLanguageInTheirProfile()
        {
            langobj.DeleteAllElements(driver);
            Thread.Sleep(3000);
        }

        [When(@"I create a new language record '([^']*)' '([^']*)'")]
        public void WhenICreateANewLanguageRecord(string Language, string Level)
        {
            LanguageWorkFlow.Addlanguage(driver, Language, Level);
            Thread.Sleep(1000);
        }

        [Then(@"the record should be saved '([^']*)'")]
        public void ThenTheRecordShouldBeSaved(string language)
        {
            AssertionHelpers.AddDeleteLanguageAssert(driver, language);


        }
        [Then(@"the record should not be saved '([^']*)'")]
        public void ThenTheRecordShouldNotBeSaved(string Language)
        {
            AssertionHelpers.AddDeleteLanguageAssert(driver, Language);
        }

        [Given(@"the user profile is set up with the languages:")]
        public void GivenTheUserProfileIsSetUpWithTheLanguages(Table table)
        {
            langobj.DeleteAllElements(driver);
            Thread.Sleep(3000);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile
                LanguageWorkFlow.Addlanguage(driver, language.language, language.Level);
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
            LanguageWorkFlow.UpdateLanguage(driver, language, newlanguage, newlanguagelevel);

        }


        [Then(@"the result of update from  ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheResultOfUpdateFromToIsPossible(string language, string languagelevel, string newlanguage, string newlanguagelevel)
        {
            AssertionHelpers.UpdateAssertions(driver, language, newlanguage);
        }


        [When(@"the user wants to delete the language  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheLanguage(string language)
        {
            langobj.deletelanguage(driver, language);
        }

        [Then(@"the language ""([^""]*)"" should be deleted\.")]
        public void ThenTheLanguageShouldBeDeleted_(string language)
        {
            AssertionHelpers.AddDeleteLanguageAssert(driver, language);
        }

        [When(@"I try to create another record with same value '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameValue(string language, string LanguageLevel)
        {
            Thread.Sleep(5000);
            LanguageWorkFlow.Addlanguage(driver, language, LanguageLevel);


        }

        [Then(@"Adding of second record '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordFails(string language, string LanguageLevel)
        {
            //LanguageWorkFlow.DuplicateEntriesAssertion(driver, language, LanguageLevel);
            AssertionHelpers.AddDeleteLanguageAssert(driver, language);

        }

        [Then(@"the system should block the updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheUpdationFromTo_(string Language, string newlanguage)
        {
            AssertionHelpers.UpdateAssertions(driver, Language, newlanguage);
        }

        [Given(@"I open a second session in tab (.*)\.")]
        public void GivenIOpenASecondSessionInTab_(int SID)
        {
            Window_Sessions.NewTab(driver);

            langobj.Sessions(driver, SID);

            langobj.Url(driver);
        }

        [Given(@"the user profile is set up with the languages in Session (.*):")]
        public void GivenTheUserProfileIsSetUpWithTheLanguagesInSession(int SID, Table table)
        {

            langobj.Sessions(driver, SID);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile
                LanguageWorkFlow.Addlanguage(driver, language.language, language.Level);
                Thread.Sleep(3000);
            }
        }

        [When(@"I create a new language record '([^']*)' '([^']*)' in Session (.*)")]
        public void WhenICreateANewLanguageRecordInSession(string language, string languageLevel, int SID)
        {
            langobj.Sessions(driver, SID);
            Thread.Sleep(2000);
            LanguageWorkFlow.Addlanguage(driver, language, languageLevel);
        }



        [Then(@"the entry of '([^']*)','([^']*)' should be blocked\.")]
        public void ThenTheEntryOfShouldBeBlocked_(string Language, string LanguageLevel)
        {
            AssertionHelpers.AddDeleteLanguageAssert(driver, Language);
        }


        [When(@"I create a new language with (.*) random charcaters and level '([^']*)'")]
        public void WhenICreateANewLanguageWithRandomCharcatersAndLevel(int length, string Level)
        {
            string randomString = LanguageWorkFlow.GenerateRandomString(length);
            LanguageWorkFlow.Addlanguage(driver, randomString, Level);
        }




        [Then(@"the addition of language with more than (.*) characters should fail")]
        public void ThenTheAdditionOfLanguageWithMoreThanCharactersShouldFail(int p0)
        {
            AssertionHelpers.StringLengthAssertion(driver);
        }

        [AfterScenario]
        public void Cleanup()
        {
            driver.Quit();
        }



    }
}
