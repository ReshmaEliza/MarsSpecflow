using TechTalk.SpecFlow;
using MarsSpecFlowProject.Utilities; 
using MarsSpecFlowProject.Pages; 
using MarsSpecFlowProject.Context;

namespace MarsSpecFlowProject.Hooks
{
    [Binding]
    public class CommonHooks
    {
        private readonly ScenarioContext scenario_context;
        private readonly TestContext test_context;
        private readonly SkillProfile skill_Profile;
        private readonly LanguageProfile language_Profile;

        public CommonHooks(ScenarioContext scenarioContext, TestContext testContext)
        {
            scenario_context = scenarioContext;
            test_context = testContext;
            skill_Profile = new SkillProfile();
            language_Profile = new LanguageProfile();
        }

        [AfterScenario]
        public void CleanupData()
        {
            // Cleanup Skill Data
            if (scenario_context.ContainsKey("SkillAdded"))
            {
                var skill = (string)scenario_context["SkillAdded"];

                // Delete all added skills
                foreach (var skillset in test_context.SkillsAdded)
                {
                    Thread.Sleep(1000);
                    skill_Profile.delete(skillset); // deletion of added elements for the particular scenario
                }

                // Delete all updated skills
                foreach (var skillset in test_context.UpdatedSkills)
                {
                    skill_Profile.delete(skillset); // deletion of updated elements for the particular scenario
                }

                // Clear the lists in the test context for the next scenario
                test_context.SkillsAdded.Clear();
                test_context.UpdatedSkills.Clear();

                // Remove the "SkillAdded" entry from ScenarioContext
                scenario_context.Remove("SkillAdded");
            }

            // Cleanup Language Data
            if (scenario_context.ContainsKey("LanguageAdded"))
            {
                var language = (string)scenario_context["LanguageAdded"];

                // Delete all added languages
                foreach (var languageset in test_context.Added_Language)
                {
                    Thread.Sleep(1000);
                    language_Profile.delete(languageset); //  deletion of added elements for the particular scenario
                }

                // Delete all updated languages
                foreach (var languageset in test_context.Updated_Language)
                {
                    Thread.Sleep(1000);
                    language_Profile.delete(languageset); //  deletion of updated elements for the particular scenario
                }

                // Clear the lists in the test context for the next scenario
                test_context.Added_Language.Clear();
                test_context.Updated_Language.Clear();

                // Remove the "LanguageAdded" entry from ScenarioContext
                scenario_context.Remove("LanguageAdded");
            }

            // Quit WebDriver 
            WebdriverManager.QuitDriver();
        }
    }
}
