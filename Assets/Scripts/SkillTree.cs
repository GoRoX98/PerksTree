using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private ToggleGroup _group;
    [SerializeField] private Button _btnForget;
    [SerializeField] private Button _btnReserch;
    [SerializeField] private TextMeshProUGUI _infoSelectSV;
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private List<SkillView> _skillsView;
    public List<Skill> AllSkills => _skills;
    private SkillView _selectedSV;
    private GameContoller _controller;

    #region Event

    private void OnEnable()
    {
        GameContoller.EventRestart += OnRestart;
    }

    private void OnRestart()
    {
        _btnForget.interactable = false; _btnReserch.interactable = false;
    }

    private void OnDisable()
    {
        GameContoller.EventRestart -= OnRestart;
    }

    #endregion

    private void Awake()
    {
        _controller = FindObjectOfType<GameContoller>();
        _infoSelectSV.text = "";
        var toggles = _group.gameObject.GetComponentsInChildren<Toggle>();
        foreach (var t in toggles)
        {
            t.onValueChanged.AddListener(on => { if (on) UpdateUI(t.gameObject.GetComponent<SkillView>()); });
        }
    }

    private void OnDestroy()
    {
        _controller.OnRestart();
    }

    private void UpdateUI(SkillView sv)
    {
        _selectedSV = sv;

        if (sv.Skill.BaseSkill)
        {
            _btnForget.interactable = false;
            _btnReserch.interactable = false;
        }
        else
        {
            switch (sv.Status)
            {
                case StatusSV.NotAvaible:
                    _btnForget.interactable = false; _btnReserch.interactable = false;
                    break;
                case StatusSV.Avaible:
                    _btnForget.interactable = false; _btnReserch.interactable = true;
                    break;
                case StatusSV.Reserched:
                    _btnReserch.interactable = false; _btnForget.interactable = CheckCanForget(sv.Skill);
                    break;
            }
        }

        _infoSelectSV.text = $"Name: {sv.Skill.Name}\nCost: {sv.Skill.Cost}";
    }

    private bool CheckCanForget(Skill sk)
    {
        foreach(Skill skill in _skills)
        {
            if(skill.Reserched)
            {
                if (skill.Parents.Find(match => match == sk && !match.BaseSkill))
                {
                    if(skill.Parents.Count > 1)
                    {
                        int reserched = 0;
                        foreach(Skill parent in skill.Parents)
                        {
                            if (parent.Reserched)
                                reserched += 1;
                        }
                        return reserched >= 2;
                    }
                    else
                        return false;
                }
            }
        }
        return true;
    }

    public void SetTree(List<Skill> skills, List<SkillView> view)
    {
        _skills = skills;
        _skillsView = view;
    }

    public void Reserch()
    {
        _selectedSV.Reserch();
        _controller.Spend(_selectedSV.Skill);
        UpdateUI(_selectedSV);
    }

    public void Forget()
    {
        _selectedSV.Forget();
        _controller.EarnForget(_selectedSV.Skill);
        UpdateUI(_selectedSV);
    }

}
