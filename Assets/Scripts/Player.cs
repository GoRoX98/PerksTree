using System;
using System.Collections.Generic;

public class Player
{
    private int _startPoints = 10;
    private int _currentPoints;
    private List<Skill> _playerSkills;

    public int CurrentPoints => _currentPoints;
    public List<Skill> PlayerSkills => _playerSkills;

    public static event Action<int> EventChangedPoints;

    public Player(List<Skill> reserchedSkills)
    {
        _currentPoints = _startPoints;
        _playerSkills = reserchedSkills;
    }

    public void Earn(int amount)
    {
        _currentPoints += amount;
        EventChangedPoints?.Invoke(_currentPoints);
    }

    public void Spend(int amount)
    {
        _currentPoints -= amount;
        EventChangedPoints?.Invoke(_currentPoints);
    }
}
