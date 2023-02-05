using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Game/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private bool _BaseSkill;
    [SerializeField] private int _cost;
    [SerializeField] private bool _avaible;
    [SerializeField] private bool _reserched;
    [SerializeField] private List<Skill> _parents;

    public event Action<Skill> Reserched;

    private void OnEnable()
    {
        Reserched += ReserchedAction;
    }
    private void OnDisable()
    {
        Reserched -= ReserchedAction;
    }

    private void ReserchedAction(Skill obj)
    {
        if(!_avaible && !_reserched)
        {
            foreach(Skill sk in _parents)
            {
                if(sk == obj)
                {
                    _avaible = true;
                }
            }
        }
    }


    public void Reserch()
    {
        if (!_avaible)
            return;

        _avaible = false;
        _reserched = true;
        Reserched?.Invoke(this);
    }
}
