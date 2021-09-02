using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    /// <summary>
    /// PUT THIS SCRIPT IN THE CAMERA OBJECT
    /// This script make the camera rotate around the player WITH the player
    /// It means that the camera always look at the back of the player.
    /// 
    /// The offset between cam and target stay the same as in the editor
    /// </summary>
    [SerializeField] Transform playerTarget;
    [SerializeField] float rotateCamSpeed = 5.0f;

    private Vector3 offset;

    void Start() {
        offset = new Vector3(playerTarget.position.x, playerTarget.position.y + 8.0f, playerTarget.position.z + 7.0f);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Mouse1)) {
            Cursor.lockState = CursorLockMode.Locked;
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateCamSpeed, Vector3.up) * offset;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }

        transform.position = playerTarget.position + offset;
        transform.LookAt(playerTarget.position);
    }
}
