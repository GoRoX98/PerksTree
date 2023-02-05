using UnityEngine;
using UnityEngine.UI;


public class SkillView : MonoBehaviour
{
    private StatusSV _status;
    [SerializeField] private Skill _skill;
    private Image _ui;
    private Button _btn;
    public Skill Skill => _skill;

    #region Events

    private void OnEnable()
    {
        Skill.ReserchedEvent += ReserchedAction;
        Player.EventChangedPoints += OnChangedPoints;
        GameContoller.EventRestart += OnRestart;
    }


    private void OnDisable()
    {
        Skill.ReserchedEvent -= ReserchedAction;
        Player.EventChangedPoints -= OnChangedPoints;
        GameContoller.EventRestart -= OnRestart;
    }

    private void OnRestart()
    {
        Init();
    }

    private void OnChangedPoints(int obj)
    {
        if (_status == StatusSV.Avaible && obj >= _skill.Cost)
            _btn.interactable = true;
        else
            _btn.interactable = false;
    }

    private void ReserchedAction(Skill obj)
    {
        if(obj == _skill)
        {
            _status = StatusSV.reserched;
        }
        else if (_skill.Avaible)
        {
            _status = StatusSV.Avaible;
        }
        ChangeColor();
    }

    #endregion

    private void Awake()
    {
        _ui = GetComponent<Image>();
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _status = _skill.Avaible && !_skill.Reserched ? StatusSV.Avaible : _skill.Reserched ? StatusSV.reserched : StatusSV.NotAvaible;
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
            case StatusSV.reserched:
                _ui.color = Color.green;
                break;
        }
    }
}

public enum StatusSV
{
    NotAvaible,
    Avaible,
    reserched
}