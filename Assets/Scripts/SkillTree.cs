using System.Collections.Generic;
using UnityEngine;

public class SkillTree
{
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private List<SkillView> _skillsView;
    public List<Skill> AllSkills => _skills;

    public SkillTree(List<Skill> skills, List<SkillView> view)
    {
        _skills = skills;
        _skillsView = view;
    }

}
