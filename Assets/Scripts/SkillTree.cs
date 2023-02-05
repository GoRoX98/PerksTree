using System.Collections.Generic;
using UnityEngine;

public class SkillTree
{
    [SerializeField] private List<Skill> _skills;
    public List<Skill> AllSkills => _skills;

    public void SetSkills(List<Skill> list)
    {
        _skills = list;
    }


}
