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
using MarsSpecFlowProject.Page;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions : CommonDriver
    {

  

        private Login loginPage;
        private ProfilePage feature;
        private string tab;
        private Assertions Assertions;

        public ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions()
        {
            loginPage = new Login();
            feature = new ProfilePage();
            Assertions = new Assertions();
        }
                

        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and  navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, String tab)
        {
            loginPage.loginPage(UserName, Password);

            this.tab = tab;
            feature.InitChoice(tab);
        }



        [Given(@"User has no language in their profile")]
        public void GivenUserHasNoLanguageInTheirProfile()
        {
            feature.DeleteAllElements();
            Thread.Sleep(2000);

            
        }

        [When(@"I create a new language record '([^']*)' '([^']*)'")]
        public void WhenICreateANewLanguageRecord(string Language, string Level)
        {
            feature.Add(Language, Level);
            Thread.Sleep(1000);
        }

        [Then(@"the record should be saved '([^']*)'")]
        public void ThenTheRecordShouldBeSaved(string language)
        {
            Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(language);



        }
        [Then(@"the record should not be saved '([^']*)'")]
        public void ThenTheRecordShouldNotBeSaved(string Language)
        {
            Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(Language);
        }
        [Then(@"Reset record")]
        public void ThenResetRecord()
        {
            feature.DeleteAllElements();

        }

        [Given(@"the user profile is set up with the languages:")]
        public void GivenTheUserProfileIsSetUpWithTheLanguages(Table table)
        {
            
            Thread.Sleep(3000);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile
                
                feature.Add(language.language, language.Level);
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
            //langobj.UpdateLanguage(driver, language, newlanguage, newlanguagelevel);
            feature.Update(language, newlanguage, newlanguagelevel);
        }


       

        [Then(@"the update from  ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromToIsPossible(string language, string languagelevel, string newlanguage, string newlanguagelevel)
        {
            Assertions.InitChoice(tab);
            //assertobj.UpdateAssertions(language, newlanguage);
            Assertions.UpdateAssertionsLanguage(language, newlanguage);
        }


        [When(@"the user wants to delete the language  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheLanguage(string language)
        {
            Thread.Sleep(1000);
            feature.delete(language);
        }

        [Then(@"the language ""([^""]*)"" should be deleted\.")]
        public void ThenTheLanguageShouldBeDeleted_(string language)
        {
            Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(language);

        }

        [When(@"I try to create another record with same value '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameValue(string language, string LanguageLevel)
        {
            Thread.Sleep(5000);
            feature.Add(language, LanguageLevel);


        }

        [Then(@"Adding of second record '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordFails(string language, string LanguageLevel)
        {
            
            Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(language);


        }

        [Then(@"the system should block the updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheUpdationFromTo_(string Language, string newlanguage)
        {
            Assertions.InitChoice(tab);
            Assertions.UpdateAssertionsLanguage(Language, newlanguage);
        }

        [Given(@"I open a second session in tab (.*)\.")]
        public void GivenIOpenASecondSessionInTab_(int SID)
        {
            WindowHandlers.NewTab();

            feature.Sessions(SID);
            feature.Url();
        }

        [Given(@"the user profile is set up with the languages in Session (.*):")]
        public void GivenTheUserProfileIsSetUpWithTheLanguagesInSession(int SID, Table table)
        {

            feature.Sessions(SID);
            var languages = table.CreateSet<Language>();
            foreach (var language in languages)
            {
                // Code to add the language and level to the user's profile
                feature.Add(language.language, language.Level);
                Thread.Sleep(3000);
            }
        }

        [When(@"I create a new language record '([^']*)' '([^']*)' in Session (.*)")]
        public void WhenICreateANewLanguageRecordInSession(string language, string languageLevel, int SID)
        {
            feature.Sessions(SID);
            Thread.Sleep(2000);
            feature.Add(language, languageLevel);
        }



        [Then(@"the entry of '([^']*)','([^']*)' should be blocked\.")]
        public void ThenTheEntryOfShouldBeBlocked_(string Language, string LanguageLevel)
        {
            Assertions.InitChoice(tab);
            Assertions.AddDeleteLanguageAssert(Language);
        }


        [When(@"I create a new language with (.*) random charcaters and level '([^']*)'")]
        public void WhenICreateANewLanguageWithRandomCharcatersAndLevel(int length, string Level)
        {
            string randomString = GlobalVariables.GenerateRandomString(length);
            feature.Add(randomString, Level);
        }




        [Then(@"the addition of language with more than (.*) characters should fail")]
        public void ThenTheAdditionOfLanguageWithMoreThanCharactersShouldFail(int p0)
        {

            Assertions.InitChoice(tab);
            Assertions.StringLengthAssertion();

        }

       



    }
}
