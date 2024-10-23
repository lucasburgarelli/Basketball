using UnityEngine;

public class VoidFallSecurity : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -3)
        {
            transform.position = new Vector3(0, 1, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
