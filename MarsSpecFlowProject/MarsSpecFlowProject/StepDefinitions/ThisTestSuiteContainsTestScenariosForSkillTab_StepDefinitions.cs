using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utils;
using MarsSpecFlowProject.Page;
using MarsSpecFlowProject.Helpers;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using TechTalk.SpecFlow.Assist;
using System.Reflection.Emit;

namespace MarsSpecFlowProject.StepDefinitions
{

    [Binding]
    public class ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions : CommonDriver
    {
        Login loginobj = new Login();
        SkillWorkflow skillobj = new SkillWorkflow();
        SkillAssertionHelper skillAssertionobj = new SkillAssertionHelper();
       [BeforeScenario]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            driver.Url = "http://localhost:5000/";
            driver.Manage().Window.Maximize();


        }


        
       

        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, string tab)
        {
            loginobj.loginAction(driver, UserName, Password);
            loginobj.GoToTab(driver,tab);

        }



        [Given(@"User has no skill in their profile")]
        public void GivenUserHasNoSkillInTheirProfile()
        {
            skillobj.DeleteAllElements(driver);
            Thread.Sleep(3000);
        }

        [When(@"I create a new skill record '([^']*)' '([^']*)'")]
        public void WhenICreateANewSkillRecord(string Skill, string SkillLevel)
        {
            skillobj.AddSkill(driver,Skill, SkillLevel);

        }

        [Then(@"the skill should be saved as '([^']*)'")]
        public void ThenTheSkillShouldBeSavedAs(string Skill)
        {
            Thread.Sleep(3000);
            skillAssertionobj.AddDeleteSkillAssert(driver,Skill);
        }

        [Then(@"the skill should not be saved '([^']*)'")]
        public void ThenTheSkillShouldNotBeSaved(string Skill)
        {
            Thread.Sleep(3000);
            skillAssertionobj.AddDeleteSkillAssert(driver, Skill);


        }

        [Given(@"the user profile is set up with the Skills:")]
        public void GivenTheUserProfileIsSetUpWithTheSkills(Table table)
        {
            skillobj.DeleteAllElements(driver);
            Thread.Sleep(3000);
            var skills = table.CreateSet<Skilltable>();
            foreach (var skill in skills)
            {
                // Code to add the language and level to the user's profile
                skillobj.AddSkill(driver, skill.Skillvalue, skill.Level);
                Thread.Sleep(3000);
            }



        }

        public class Skilltable
        {
            public string Skillvalue { get; set; }
            public string Level { get; set; }
        }


        [When(@"the user wants to update the Skill or level from ""([^""]*)"",""([^""]*)"" to ""([^""]*)"",""([^""]*)""")]
        public void WhenTheUserWantsToUpdateTheSkillOrLevelFromTo(string skill, string level, string newSkill, string newLevel)
        {
            Thread.Sleep(3000);
            skillobj.UpdateSkill(driver, skill, newSkill, newLevel);
        }
        [Then(@"the update from skill  ""([^""]*)"",""([^""]*)"" to skill ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromSkillToSkillIsPossible(string skill, string level, string newSkill, string newLevel)
        {

            skillAssertionobj.UpdateAssertions(driver,skill,newSkill);
            
        }


        [When(@"the user wants to delete the Skill  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheSkill(string skill)
        {
        SkillWorkflow.deletelanguage(driver, skill);
        
        }

        [Then(@"the Skill ""([^""]*)"" should be deleted\.")]
        public void ThenTheSkillShouldBeDeleted_(string skill)
        {
            skillAssertionobj.AddDeleteSkillAssert(driver, skill);
        }

        [When(@"I try to create another record with same skills '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameSkills(string Skill, string SkillLevel)
        {
            Thread.Sleep(3000);
            skillobj.AddSkill(driver, Skill, SkillLevel);
        }

        [Then(@"Adding of second record for skill '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordForSkillFails(string Skill, string beginner)
        {
            skillAssertionobj.AddDeleteSkillAssert(driver, Skill);
        }

        [Then(@"the system should block the skill updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheSkillUpdationFromTo_(string skill, string NewSkill)
        {

            skillAssertionobj.UpdateAssertions(driver,skill,NewSkill);
        }

        [When(@"I create a new Skill with (.*) random characters and level '([^']*)'")]
        public void WhenICreateANewSkillWithRandomCharactersAndLevel(int p, string Level)
        {
            string skill = GlobalVariables.GenerateRandomString(p);
            skillobj.AddSkill(driver,skill,Level);

        }

        [Then(@"the addition of Skill with more than (.*) characters should fail")]
        public void ThenTheAdditionOfSkillWithMoreThanCharactersShouldFail(int p0)
        {
            skillAssertionobj.StringLengthAssertion(driver);

        }

        [When(@"I create a  (.*) new random skill set for the user\.")]
       

        [When(@"I create a  (.*) new random skill with level '([^']*)' set for the user\.")]
        public void WhenICreateANewRandomSkillWithLevelSetForTheUser_(int p0, string Level)
        {
            for (int i = 0; i < p0; i++)
            {
                string skill = GlobalVariables.GenerateRandomString(50);
                skillobj.AddSkill(driver, skill, Level);
            }
        }


        [Then(@"verify if all the (.*) elements is added to the system")]
        public void ThenVerifyIfAllTheElementsIsAddedToTheSystem(int OGElementCount)
        {
            Thread.Sleep(3000);
            skillAssertionobj.Stability(driver, OGElementCount);
        }



       [AfterScenario]
        public void Cleanup()
        {
            driver.Close();
        }

    }
}
