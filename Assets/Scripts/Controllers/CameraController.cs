using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    public Transform _camTarget;

    public Transform _player;

    [SerializeField] public float distance = 10.0f;
    [SerializeField] private float currentX = 0.0f;
    [SerializeField] private float currentY = 0.0f;
    [SerializeField] public float sensivity = 4.0f;

      

    
    void LateUpdate()
    {

        currentX += Input.GetAxis("Mouse X") * sensivity;
        currentY += -Input.GetAxis("Mouse Y") * sensivity;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = _camTarget.position + rotation * Direction;

        transform.LookAt(_camTarget.position); //Focuses on the object



    }


}
