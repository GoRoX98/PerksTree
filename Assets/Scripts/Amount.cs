using TMPro;
using UnityEngine;

public class Amount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amount;
    private static int _currentEarn;

    public static int CurrentEarn => _currentEarn;

    private void Awake()
    {
        _currentEarn = int.Parse(_amount.text);
    }

    public void More()
    {
        _currentEarn += 1;
        _amount.text = _currentEarn.ToString();
    }

    public void Less()
    {
        if (_currentEarn == 1)
            return;

        _currentEarn -= 1;
        _amount.text = _currentEarn.ToString();
    }
}
