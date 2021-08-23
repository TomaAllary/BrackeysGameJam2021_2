using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [SerializeField] float camSpeed = 8.0f;
    [SerializeField] float rotateCamSpeedH = 25.0f;
    [SerializeField] float rotateCamSpeedV = 35.0f;

    private Camera cam;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        yaw = cam.transform.eulerAngles.y;
        pitch = cam.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float sideSpeed = Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;
        cam.transform.position += cam.transform.right * sideSpeed;

        float frontSpeed = Input.GetAxis("Vertical") * camSpeed * Time.deltaTime;
        Vector3 frontVector = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;

        cam.transform.position += frontVector * frontSpeed;


        if (Input.GetKey(KeyCode.LeftAlt))
        {

            Cursor.lockState = CursorLockMode.Locked;

            yaw += rotateCamSpeedH * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= rotateCamSpeedV * Input.GetAxis("Mouse Y") * Time.deltaTime;

            cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;

        }

    }
}
