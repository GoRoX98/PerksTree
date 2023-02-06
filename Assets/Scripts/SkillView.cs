using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class SkillView : MonoBehaviour
{
    private StatusSV _status;
    [SerializeField] private Skill _skill;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _cost;
    private Image _ui;
    public Skill Skill => _skill;
    public StatusSV Status => _status;

    #region Events

    private void OnEnable()
    {
        Skill.ReserchedEvent += ReserchedAction;
        GameContoller.EventRestart += OnRestart;
        Player.EventChangedPoints += OnChangedPoints;
    }

    private void OnChangedPoints(int obj)
    {
        if (_status == StatusSV.Reserched)
            return;

        if (obj >= _skill.Cost && _skill.Avaible)
            _status = StatusSV.Avaible;
        else
            _status = StatusSV.NotAvaible;

        ChangeColor();
    }

    private void OnDisable()
    {
        Skill.ReserchedEvent -= ReserchedAction;
        GameContoller.EventRestart -= OnRestart;
        Player.EventChangedPoints -= OnChangedPoints;
    }

    private void OnRestart()
    {
        Init();
    }

    private void ReserchedAction(Skill obj)
    {
        if(obj == _skill)
        {
            if (_skill.Reserched)
                _status = StatusSV.Reserched;
            else
                _status = StatusSV.Avaible;
        }
        ChangeColor();
    }

    #endregion

    private void Awake()
    {
        _ui = GetComponent<Image>();
        _title.text = _skill.Name;
        _cost.text = _skill.Cost.ToString();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _status = _skill.BaseSkill ? 
            StatusSV.Reserched : _skill.Avaible && !_skill.Reserched ? 
            StatusSV.Avaible : _skill.Reserched ? 
            StatusSV.Reserched : StatusSV.NotAvaible;

        ChangeColor();
    }

    private void ChangeColor()
    {
        switch(_status)
        {
            case StatusSV.NotAvaible:
                _ui.color = Color.gray;
                break;
            case StatusSV.Avaible:
                _ui.color = Color.blue;
                break;
            case StatusSV.Reserched:
                _ui.color = Color.green;
                break;
        }
    }

    public void Reserch()
    {
        Skill.Reserch();
    }

    public void Forget()
    {
        Skill.Forget();
    }
}

public enum StatusSV
{
    NotAvaible,
    Avaible,
    Reserched
}