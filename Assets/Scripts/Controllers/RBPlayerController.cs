using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;
public class RBPlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 input = new Vector3();
    private bool _isGrounded = true;
    private Renderer _renderer;
    private float _groundCheckRadius = 0.5f;
    private Vector3 _groundedCheckMiddle;
    private bool _canDoubleJump = false;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _jumpCount = 2;
    AudioSource audioData;
    
    private Vector3 vertInput;
    private Vector3 horiInput;

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        if (_camera == null)
        {
            _camera = Camera.main;
            return;
        }
        if (_camera == null)
        {
            _camera = FindObjectOfType<Camera>();
        }
    }
    void Update()
    {
        vertInput = new Vector3(0, 0, Input.GetAxis("Vertical"));
        horiInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y)
                , ForceMode.VelocityChange);
            _canDoubleJump = true; // Allows for a double jump
        }
        else

        if (Input.GetButtonDown("Jump"))
        {            
            if (_canDoubleJump == true)
            {
                _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y)
            , ForceMode.VelocityChange);
                audioData.Play();
                _canDoubleJump = false;
            }
        }

        vertInput = _camera.transform.TransformDirection(vertInput);
        horiInput = transform.TransformDirection(horiInput);
        input = vertInput + horiInput;
        input.y = 0f;
        input.Normalize();
    }
    private void FixedUpdate()
    {
        _groundedCheckMiddle = _renderer.bounds.center;
        _groundedCheckMiddle.y -= _renderer.bounds.extents.y - (_renderer.bounds.extents.y * 0.20f);
        _isGrounded = Physics.CheckSphere(_groundedCheckMiddle, _groundCheckRadius * transform.localScale.y
            , groundMask, QueryTriggerInteraction.Ignore);
        
        _rigidbody.MovePosition(_rigidbody.position + input.normalized * (speed * Time.deltaTime));
        
        Vector3 flattenedInput = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up);
        Quaternion toRotation = Quaternion.LookRotation(flattenedInput, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
            rotationSpeed * Time.deltaTime);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(_groundedCheckMiddle, _groundCheckRadius * transform.localScale.y);
    }
}
