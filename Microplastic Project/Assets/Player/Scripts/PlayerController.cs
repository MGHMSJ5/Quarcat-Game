using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Values")]
    public static float Speed = 5;
    public static float Turnspeed = 850;

    [SerializeField]
    public float speed
    {
        get { return Speed; }
        set { Speed = value; }
    }

    public float turnspeed
    {
        get { return Turnspeed; }
        set { Turnspeed = value; }
    }

    [Header("References")]
    [SerializeField]
    private FixedJoystick _joystick;

    private Rigidbody _rb;
    private Vector3 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {

            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Turnspeed * Time.deltaTime);
        }
    }

    void Move()
    {
        _rb. MovePosition(transform.position + (transform.forward * _input.magnitude) * Speed * Time.deltaTime);
    }
}