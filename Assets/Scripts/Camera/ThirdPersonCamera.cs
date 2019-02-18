using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;
    //public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothtime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    [SerializeField] float yaw;
    [SerializeField] float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void LateUpdate () {
       //yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
       //pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
       //pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothtime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
	}
}
