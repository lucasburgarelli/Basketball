using UnityEngine;

public class PlayerBallInteraction : MonoBehaviour
{
    [SerializeField] private Transform transformCamera;
    [SerializeField] private Rigidbody rigidbodyBall;
    [SerializeField] private float throwForce = 2;
    private Transform _transformBall;
    private bool _hasBall;
    private void TryInteraction()
    {
        if (!_hasBall)
        {
            TryCatchBall();
            return;
        }

        ThrowBall();
    }

    private void TryCatchBall ()
    {
        if (!Physics.Raycast(transformCamera.position, transformCamera.forward, out var hitInfo, 0.8f)) return;
        if (!hitInfo.collider.gameObject.CompareTag("Ball")) return;

        _transformBall = hitInfo.collider.transform;
        _transformBall.parent = transformCamera;
        _hasBall= true;

        rigidbodyBall.velocity = Vector3.zero;
        rigidbodyBall.angularVelocity = Vector3.zero;
    }
    private void ThrowBall()
    {
        _transformBall.parent = null;
        _hasBall= false;
        _transformBall.rotation = transformCamera.rotation;
        _transformBall.Rotate(new Vector3(0, 60));
        rigidbodyBall.AddForce(throwForce * 0.02f * transformCamera.forward.normalized, ForceMode.Impulse);
    }
    private void Update()
    {
        if (_hasBall) _transformBall.position = transformCamera.position + transformCamera.forward.normalized * 0.5f;
        if (Input.GetMouseButtonDown(1)) TryInteraction();
    }
}