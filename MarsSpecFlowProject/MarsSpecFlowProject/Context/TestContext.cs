using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsSpecFlowProject.Context
{

    public class TestContext
    {
        // Lists to keep track of added and updated skills
        public List<string> SkillsAdded { get; } = new List<string>();
        public List<string> UpdatedSkills { get; } = new List<string>();

        public void AddUpdatedSkill(string oldSkill, string newSkill)
        {
            UpdatedSkills.Add(newSkill);
        }

        // Lists to keep track of added and updated languages
        public List<string> Added_Language { get; } = new List<string>();
        public List<string> Updated_Language { get; } = new List<string>();

        public void AddLanguageUpdated(string OldValue, string NewValue)
        {
            Updated_Language.Add(NewValue);
        }

        public void RemoveSkill(string skill)
        {
            SkillsAdded.Remove(skill);
            UpdatedSkills.Remove(skill);
        }

        public void RemoveLanguage(string skill)
        {
            Added_Language.Remove(skill);
            Updated_Language.Remove(skill);
        }
        // Constructor
        public TestContext()
        {
            // Initialization logic if needed
        }



    }
}