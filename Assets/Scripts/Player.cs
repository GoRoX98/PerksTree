using System;
using System.Collections.Generic;

public class Player
{
    private int _startPoints = 10;
    private int _currentPoints;
    private List<Skill> _playerSkills;

    public int StartPoints => _startPoints;
    public int CurrentPoints => _currentPoints;
    public List<Skill> PlayerSkills => _playerSkills;

    public static event Action<int> EventChangedPoints;

    public Player(List<Skill> reserchedSkills)
    {
        _currentPoints = _startPoints;
        _playerSkills = reserchedSkills;
        EventChangedPoints?.Invoke(_currentPoints);
    }

    public void Earn(int amount, Skill sk = null)
    {
        if (sk != null)
        {
            _playerSkills.Remove(sk);
            _currentPoints += sk.Cost;
        }
        else
            _currentPoints += amount;
        EventChangedPoints?.Invoke(_currentPoints);
    }

    public void Spend(Skill sk)
    {
        _currentPoints -= sk.Cost;
        _playerSkills.Add(sk);
        EventChangedPoints?.Invoke(_currentPoints);
    }
}
