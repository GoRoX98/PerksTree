using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    private Image _ui;
    public Skill Skill => _skill;

    private void OnEnable()
    {
        Skill.Reserched += ReserchedAction;
    }

    private void ReserchedAction(Skill obj)
    {
        if(obj == _skill)
        {
            _ui.color = Color.green;
        }
    }
}
