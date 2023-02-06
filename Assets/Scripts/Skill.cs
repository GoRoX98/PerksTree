using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Game/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private bool _baseSkill;
    [SerializeField] private int _cost;
    [SerializeField] private bool _avaible;
    [SerializeField] private bool _reserched;
    [SerializeField] private List<Skill> _parents;

    public bool Reserched => _reserched;
    public bool BaseSkill => _baseSkill;
    public bool Avaible => _avaible;
    public int Cost => _cost;
    public string Name => _name;
    public List<Skill> Parents => _parents;

    #region Events

    public static event Action<Skill> ReserchedEvent;

    private void OnEnable()
    {
        if (_baseSkill)
            _reserched = true;
        ReserchedEvent += ReserchedAction;
        GameContoller.EventRestart += OnRestart;
    }
    private void OnDisable()
    {
        ReserchedEvent -= ReserchedAction;
        GameContoller.EventRestart -= OnRestart;
    }
    private void ReserchedAction(Skill obj)
    {
        if(!_reserched)
        {
            foreach(Skill sk in _parents)
            {
                if(sk == obj)
                {
                    if (sk.Reserched)
                        _avaible = true;
                    else
                        _avaible = false;
                }
            }
        }
    }

    #endregion

    private void Awake()
    {
        _avaible = _parents.Find(match => match.BaseSkill == true) && !_reserched;
    }

    public void Reserch()
    {
        if (!_avaible)
            return;

        _avaible = false;
        _reserched = true;
        ReserchedEvent?.Invoke(this);
    }

    public void Forget()
    {
        if (!_reserched)
            return;

        _avaible = true;
        _reserched = false;
        ReserchedEvent?.Invoke(this);
    }

    public void OnRestart()
    {
        _reserched = false;
        _avaible = _parents.Find(match => match.BaseSkill) && !_reserched;
    }
}
