using TMPro;
using UnityEngine;

public class UIPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPoints;
    private int _points;
    public void IncreasePointsPoints()
    {
        _points++;
        textPoints.SetText($"Points: {_points}");
    }
}
