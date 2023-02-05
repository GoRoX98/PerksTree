using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceUI;
    private Player _player;
    private SkillTree _tree;

    #region Events

    public static event Action EventRestart;

    private void OnEnable()
    {
        Player.EventChangedPoints += OnChangedPoints;
    }

    public void OnRestart()
    {
        int points = 0;
        foreach(var rs in _player.PlayerSkills)
            points += rs.Cost;

        EventRestart?.Invoke();
        Init();
        _player.Earn(points);
    }

    private void OnChangedPoints(int obj)
    {
        _balanceUI.text = $"Points: {obj}";
    }

    private void OnDisable()
    {
        Player.EventChangedPoints -= OnChangedPoints;
    }

    #endregion

    private void Awake()
    {
        Init();
        _balanceUI.text = $"Points: {_player.CurrentPoints}";
    }

    private void Init()
    {
        List<SkillView> skillsView = new();
        List<Skill> skillList = new();
        List<Skill> reserchedSkills = new();
        foreach (var sv in FindObjectsOfType<SkillView>())
        {
            skillsView.Add(sv);
            skillList.Add(sv.Skill);

            if (sv.Skill.Reserched)
                reserchedSkills.Add(sv.Skill);
        }
        _tree = new SkillTree(skillList, skillsView);
        _player = new Player(reserchedSkills);
    }

    public void Earn() => _player.Earn(Amount.CurrentEarn);

}
