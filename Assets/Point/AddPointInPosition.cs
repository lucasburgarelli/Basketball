using System.Collections;
using UnityEngine;

public class AddPointInPosition : MonoBehaviour
{
    [SerializeField] private UIPoints uiPoints;
    private Light _light;

    private void OnTriggerEnter(Collider other)
    {
        uiPoints.IncreasePointsPoints();
        StartCoroutine(BlinkLightRoutine());
    }

    private IEnumerator BlinkLightRoutine()
    {
        _light.enabled = true;
        yield return new WaitForSeconds(1);
        _light.enabled = false;
    }

    private void Start()
    {
        _light = GetComponent<Light>();
    }
}
