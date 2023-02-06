using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceUI;
    [SerializeField] private SkillTree _tree;
    private Player _player;

    public int Balance => _player.CurrentPoints;

    #region Events

    public static event Action EventRestart;

    private void OnEnable()
    {
        Player.EventChangedPoints += OnChangedPoints;
    }

    public void OnRestart()
    {
        int points = _player.CurrentPoints - _player.StartPoints;
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
        _tree.SetTree(skillList, skillsView);
        _player = new Player(reserchedSkills);
    }

    public void Earn() => _player.Earn(Amount.CurrentEarn);
    public void EarnForget(Skill sk = null) => _player.Earn(0, sk);
    public void Spend(Skill sk) => _player.Spend(sk);
}
