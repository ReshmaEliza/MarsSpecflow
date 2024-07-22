using NUnit.Framework;
using MarsSpecFlowProject.Pages;
using System;
using MarsSpecFlowProject.Utilities;
using TechTalk.SpecFlow;
using MarsSpecFlowProject.Context;

using TestContext = MarsSpecFlowProject.Context.TestContext;
using TechTalk.SpecFlow.Assist;
using Gherkin;
using static MarsSpecFlowProject.StepDefinitions.ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions;


namespace MarsSpecFlowProject.StepDefinitions
{
    [Binding]
    public class ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions
    {
        private Login loginPage;
        private SkillProfile skillprofile;
        private AssertionUtils assertionHelper;
        private readonly TestContext testContext;
        private readonly ScenarioContext scenario_Context;

       
        public ThisTestSuiteContainsTestScenariosForSkillTab_StepDefinitions(TestContext context, ScenarioContext scenarioContext)
        {
            loginPage = new Login();
            skillprofile = new SkillProfile();
            testContext = context;
            assertionHelper = new AssertionUtils();
            scenario_Context = scenarioContext;


        }
        [Given(@"I log into the portal with UserName '([^']*)' and Password '([^']*)' and navigate to '([^']*)' Tab")]
        public void GivenILogIntoThePortalWithUserNameAndPasswordAndNavigateToTab(string UserName, string Password, string tab)
        {
            loginPage.loginPage(UserName, Password);

            skillprofile.GoToTab(tab);
        }

        [Given(@"User has no skill in their profile")]
        public void GivenUserHasNoSkillInTheirProfile()
        {
            Thread.Sleep(1000);
            skillprofile.DeleteAllElements();

        }

        [When(@"I create a new skill record '([^']*)' '([^']*)'")]
        public void WhenICreateANewSkillRecord(string Skill, string Level)
        {
            Thread.Sleep(3000);
            skillprofile.Add(Skill, Level);
            testContext.SkillsAdded.Add(Skill);
            scenario_Context["SkillAdded"] = Skill;
        }

        [Then(@"the skill should be saved as '([^']*)'")]
        public void ThenTheSkillShouldBeSavedAs(string Skill)
        {
            assertionHelper.AddDeleteSkillAssert(Skill);
        }


        [Then(@"the skill should not be saved '([^']*)'")]
        public void ThenTheSkillShouldNotBeSaved(string Skill)
        {
            Thread.Sleep(3000);
            assertionHelper.AddDeleteSkillAssert(Skill);
        }

        [Given(@"the user profile is set up with the Skills:")]
        public void GivenTheUserProfileIsSetUpWithTheSkills(Table table)
        {
            Thread.Sleep(3000);
            var skills = table.CreateSet<Skilltable>();
            foreach (var skillset in skills)
            {
                skillprofile.Add(skillset.Skillvalue, skillset.Level);
                testContext.SkillsAdded.Add(skillset.Skillvalue);
                scenario_Context["SkillAdded"] = skillset.Skillvalue;
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
            Thread.Sleep(2000);
            skillprofile.Update(skill, newSkill, newLevel);
            testContext.AddUpdatedSkill(skill, newSkill);
            testContext.RemoveLanguage(skill);

        }

        [Then(@"the update from skill  ""([^""]*)"",""([^""]*)"" to skill ""([^""]*)"",""([^""]*)"" is possible")]
        public void ThenTheUpdateFromSkillToSkillIsPossible(string skill, string level, string newSkill, string newlevel)
        {
            assertionHelper.UpdateAssertionsSkill(skill, newSkill);
        }

        [When(@"the user wants to delete the Skill  ""([^""]*)""")]
        public void WhenTheUserWantsToDeleteTheSkill(string skill)
        {
            skillprofile.delete(skill);
            testContext.RemoveSkill(skill);
        }

        [Then(@"the Skill ""([^""]*)"" should be deleted\.")]
        public void ThenTheSkillShouldBeDeleted_(string skill)
        {
            assertionHelper.AddDeleteSkillAssert(skill);

        }

        [When(@"I try to create another record with same skills '([^']*)' '([^']*)'")]
        public void WhenITryToCreateAnotherRecordWithSameSkills(string Skill, string SkillLevel)
        {
            Thread.Sleep(3000);
            skillprofile.Add(Skill, SkillLevel);
            testContext.SkillsAdded.Add(Skill);
        }

        [Then(@"Adding of second record for skill '([^']*)' '([^']*)' fails")]
        public void ThenAddingOfSecondRecordForSkillFails(string Skill, string beginner)
        {
            assertionHelper.AddDeleteSkillAssert(Skill);


        }



        [Then(@"the system should block the skill updation from '([^']*)' to '([^']*)'\.")]
        public void ThenTheSystemShouldBlockTheSkillUpdationFromTo_(string skill, string NewSkill)
        {
            assertionHelper.UpdateAssertionsSkill(skill, NewSkill);


        }

        [When(@"I create a new Skill with (.*) random characters and level '([^']*)'")]
        public void WhenICreateANewSkillWithRandomCharactersAndLevel(int p, string Level)
        {
            string skill = StringUtilities.GenerateRandomString(p);
            skillprofile.Add(skill, Level);
            testContext.SkillsAdded.Add(skill);
            scenario_Context["SkillAdded"] = skill;
        }

        [Then(@"the addition of Skill with more than (.*) characters should fail")]
        public void ThenTheAdditionOfSkillWithMoreThanCharactersShouldFail(int p0)
        {
            assertionHelper.StringLengthAssertion_Skill();
        }

        [When(@"I create a  (.*) new random skill with level '([^']*)' set for the user\.")]
        public void WhenICreateANewRandomSkillWithLevelSetForTheUser_(int p0, string Level)
        {
            for (int i = 0; i < p0; i++)
            {
                string skill = StringUtilities.GenerateRandomString(50);
                skillprofile.Add(skill, Level);
                testContext.SkillsAdded.Add(skill);
                scenario_Context["SkillAdded"] = skill;
            }
        }

        [Then(@"verify if all the (.*) elements is added to the system")]
        public void ThenVerifyIfAllTheElementsIsAddedToTheSystem(int OGElementCount)
        {
            Thread.Sleep(3000);
            assertionHelper.Stability(OGElementCount);
        }



   
        }
    }

