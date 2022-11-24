using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]

//Unused script but kept as a back up

public class OrbitCamera : MonoBehaviour
{

    public Transform player;

    #region Focus Variables
    [Header("Focus")]
    [SerializeField] private Transform _focus;
    [SerializeField] private Vector3 _focusOffset;

    [SerializeField, Min(0f)] private float _focusRadius = 1f;
    [SerializeField, Range(0f, 1f)] private float _focusCentering = 0.5f;

    private Vector3 _focusPoint, _previousFocusPoint;
    #endregion

    #region Orbit Variables

    public bool _disableOrbit = true;
    private Vector2 _orbitAngles = new Vector2(45f, 0);
    [Header("Orbit")]
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private float _orbitDistance = 5f;
    [SerializeField] private float _orbitHeight = 2f;
    [SerializeField] private float _minVertAngle = -30f;
    [SerializeField] private float _maxVertAngle = 60f;
    #endregion

    #region Align
    [Header("Align")]
    [SerializeField, Min(0f)] private float alignDelay = 2f;
    private float lastManualRotationTime = 0f;
    #endregion

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = player.transform.position + new Vector3(0, 1, -5);
            Cursor.lockState = CursorLockMode.Locked;
            _disableOrbit = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            _disableOrbit = true;
        }
    }

    private void Awake()
    {
        _focusPoint = _focus.position + _focusOffset;
        transform.localRotation = Quaternion.Euler(_orbitAngles);
    }

    //late update runs after Update/Fixed Update (but works exactly like update)
    private void LateUpdate()
    {
        Quaternion lookRotation = transform.localRotation;

        UpdateFocusPoint();

        if (ManualRotation() || AutomaticRotation())
        {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(_orbitAngles);
        }

        //If we multiply a Quaternion by a vector we get a new vector
        //The new vector the old vector rotated by the Quaternion
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = _focusPoint - lookDirection * _orbitDistance;

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    private bool ManualRotation()
    {
        if (_disableOrbit) return false;
        Vector2 input = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        float e = 0.001f;
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            _orbitAngles += _rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }
        return false;
    }

    private bool AutomaticRotation()
    {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay) return false;

        //How much the focus has moved this frame
        Vector2 moveDelta = new Vector2(_focusPoint.x - _previousFocusPoint.x,
                                        _focusPoint.z - _previousFocusPoint.z);
        if (moveDelta.sqrMagnitude < 0.0001f)
        {
            return false;
        }
        float headingAngle = GetAngle(moveDelta.normalized);
        _orbitAngles.y = Mathf.MoveTowardsAngle(_orbitAngles.y, headingAngle, _rotationSpeed * Time.unscaledDeltaTime);
        return true;
    }

    private void UpdateFocusPoint()
    {
        _previousFocusPoint = _focusPoint;
        Vector3 targetPoint = _focus.position + _focusOffset;

        if (_focusRadius > 0f)
        {
            
            float moveDistance = Vector3.Distance(targetPoint, _previousFocusPoint);
            float t = 1f;

            if (moveDistance > 0.01f && _focusCentering > 0f)
            {
                t = Mathf.Pow(1f - _focusCentering, Time.unscaledDeltaTime);
            }

            if (moveDistance > _focusRadius)
            {
                t = Mathf.Min(t, _focusRadius / moveDistance);
            }

            _focusPoint = Vector3.Lerp(targetPoint, _previousFocusPoint, t);
        }
        else
        {
            _focusPoint = targetPoint;
        }


    }

    float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;

        if (direction.x < 0f)
        {
            return 360f - angle;
        }
        else
        {
            return angle;
        }
    }
      
    void ConstrainAngles()
    {
        _orbitAngles.x = Mathf.Clamp(_orbitAngles.x, _minVertAngle, _maxVertAngle);

        if (_orbitAngles.y < 0f)
        {
            _orbitAngles.y += 360f;
        }
        else if (_orbitAngles.y < 360f)
        {
            _orbitAngles.y -= 360f;
        }
    }

    private void OnValidate()
    {
        if (_maxVertAngle < _minVertAngle)
        {
            _maxVertAngle = _minVertAngle;
        }
    }
}
