using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utils;
using MarsSpecFlowProject.Page;
using MarsSpecFlowProject.Helpers;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForLanguageTab_StepDefinitions;
using TechTalk.SpecFlow.Assist;
using System.Reflection.Emit;
using NUnit.Framework;

namespace MarsSpecFlowProject.StepDefinitions
{

    [Binding]
    public class ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions : CommonDriver
    {
        
        
        

        private Login loginPage;
        private ProfilePage feature;
        private string tab;
        private Assertions Assertions;

        public ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions()
        {
            loginPage = new Login();
            feature = new ProfilePage();
            Assertions = new Assertions();
        }


        
        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, string tab)
        {
           
             
            loginPage.loginPage(UserName, Password);
             
            feature.GoToTab (tab);
            this.tab = tab;
            feature.InitChoice(tab);
                    }





        [Given(@"User has no skill in their profile")]
        public void GivenUserHasNoSkillInTheirProfile()
        {
            
            
            feature.DeleteAllElements();
            Thread.Sleep(3000);
        }

        [When(@"I create a new skill record '([^']*)' '([^']*)'")]
        public void WhenICreateANewSkillRecord(string Skill, string SkillLevel)
        {
            feature.Add(Skill, SkillLevel);

        }

        [Then(@"the skill should be saved as '([^']*)'")]
        public void ThenTheSkillShouldBeSavedAs(string Skill)
        {
            Thread.Sleep(3000);
            Assertions.InitChoice(tab);
            Assertions.AddDeleteSkillAssert(Skill);
        }

       

        [Then(@"Reset Data")]
        public void ThenResetData()
        {
            feature.DeleteAllElements();


        }


        [Then(@"the skill should not be saved '([^']*)'")]
        public void ThenTheSkillShouldNotBeSaved(string Skill)
        {
            Thread.Sleep(3000);
              Assertions.InitChoice(tab);
            Assertions.AddDeleteSkillAssert(Skill);


        }



        [Given(@"the user profile is set up with the Skills:")]
        public void GivenTheUserProfileIsSetUpWithTheSkills(Table table)
        {
           
            Thread.Sleep(3000);
            var skills = table.CreateSet<Skilltable>();
            foreach (var skill in skills)
            {
                // Code to add the language and level to the user's profile
                feature.Add(skill.Skillvalue, skill.Level);
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
            feature.Update(skill, newSkill, newLevel);
        }
        [Then(@"the update from skill  ""([^""]*)"",""([^""]*)"" to skill ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromSkillToSkillIsPossible(string skill, string level, string newSkill, string newLevel)
        {

            
            Assertions.InitChoice(tab);

            Assertions.UpdateAssertionsSkill(skill, newSkill);


        }


        [When(@"the user wants to delete the Skill  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheSkill(string skill)
        {
        feature.delete(skill);
        
        }

        [Then(@"the Skill ""([^""]*)"" should be deleted\.")]
        public void ThenTheSkillShouldBeDeleted_(string skill)
        {
            Assertions.InitChoice(tab);
            
            Assertions.AddDeleteSkillAssert(skill);
        }

        [When(@"I try to create another record with same skills '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameSkills(string Skill, string SkillLevel)
        {
            Thread.Sleep(3000);
            feature.Add(Skill, SkillLevel);
        }

        [Then(@"Adding of second record for skill '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordForSkillFails(string Skill, string beginner)
        {
            Assertions.InitChoice(tab);
            Assertions.AddDeleteSkillAssert(Skill);
        }

        [Then(@"the system should block the skill updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheSkillUpdationFromTo_(string skill, string NewSkill)
        {

            Assertions.InitChoice(tab);
            Assertions.UpdateAssertionsSkill(skill, NewSkill);

        }

        [When(@"I create a new Skill with (.*) random characters and level '([^']*)'")]
        public void WhenICreateANewSkillWithRandomCharactersAndLevel(int p, string Level)
        {
            string skill = GlobalVariables.GenerateRandomString(p);
            feature.Add(skill,Level);

        }

        [Then(@"the addition of Skill with more than (.*) characters should fail")]
        public void ThenTheAdditionOfSkillWithMoreThanCharactersShouldFail(int p0)
        {
            Assertions.InitChoice(tab);
            Assertions.StringLengthAssertion();

        }

        
       

        [When(@"I create a  (.*) new random skill with level '([^']*)' set for the user\.")]
        public void WhenICreateANewRandomSkillWithLevelSetForTheUser_(int p0, string Level)
        {
            for (int i = 0; i < p0; i++)
            {
                string skill = GlobalVariables.GenerateRandomString(50);
                feature.Add(skill, Level);
            }
        }


        [Then(@"verify if all the (.*) elements is added to the system")]
        public void ThenVerifyIfAllTheElementsIsAddedToTheSystem(int OGElementCount)
        {
            Thread.Sleep(3000);
            Assertions.InitChoice(tab);

            Assertions.Stability(OGElementCount);
        }



       

    }
}
