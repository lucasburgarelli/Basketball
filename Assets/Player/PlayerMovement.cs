using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform transformCamera;
    [Range(0, 5)] 
    [SerializeField] private float MouseSensibility = 0.5f;
    [Range(1, 5)] 
    [SerializeField] private float moveSpeed = 2.5f;
    private Rigidbody _rigidbody;
    private Vector2 _moveCameraAxis, _moveAxis;
    private float _mouseAxisY, _actualSpeed;
    private bool _onGround;

    private void Move()
    {
        var adjustedSpeed = _actualSpeed * moveSpeed * 200;

        _rigidbody.AddForce(transform.forward * (adjustedSpeed * _moveAxis.y * Time.deltaTime * 10));

        _rigidbody.AddForce(transform.right * (adjustedSpeed * _moveAxis.x * Time.deltaTime * 10));
    }
    private void MoveCamera()
    {
        var mouseActualSpeed = (MouseSensibility * 255) / 10;
        var rotation = new Vector3(0.0f, mouseActualSpeed * _moveCameraAxis.x, 0.0f);
        transform.Rotate(rotation);

        rotation = new Vector3(-(mouseActualSpeed * _moveCameraAxis.y), 0.0f, 0.0f);

        var isAxisYAtLimit = rotation.x + _mouseAxisY > 90 || rotation.x + _mouseAxisY < -90;
        if (isAxisYAtLimit) return;

        _mouseAxisY += rotation.x;
        transformCamera.Rotate(rotation);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Time.deltaTime * 5000 * Vector3.up, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _onGround = collision.gameObject.CompareTag("Ground");
    }
    private void OnCollisionExit(Collision collision)
    {
        _onGround = !collision.gameObject.CompareTag("Ground");
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _moveAxis = new Vector2
        {
            x = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0),
            y = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0)
        };

        _moveCameraAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _actualSpeed = Input.GetKey(KeyCode.LeftShift) ? 1.25f : 1;
        if (Input.GetKey(KeyCode.Space) && _onGround) Jump();

        Move();
        MoveCamera();
    }
}
