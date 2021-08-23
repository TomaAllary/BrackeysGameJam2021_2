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
    [SerializeField] float rotateCamSpeed = 100.0f;

    private float forwardOffset;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 Offset = transform.position - playerTarget.position;
        Vector3 normal = transform.forward;
        normal.y = 0.0f;

        forwardOffset = Vector3.Project(Offset, normal).magnitude;

    }

    private void FixedUpdate() {

        SyncPosWithTarget();

        //Rotate around player on second mouse button
        if (Input.GetKey(KeyCode.Mouse1)) {

            Cursor.lockState = CursorLockMode.Locked;

            transform.RotateAround(playerTarget.position, Vector3.up, rotateCamSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }
        else {
            Cursor.lockState = CursorLockMode.None;

        }
    }

    private void SyncPosWithTarget() {
        //This sync work with the camera right and forward reference
        //in other words, the difference of position is projected on the RIGHT and FORWARD of the cam

        //Sync with forward of the camera
        Vector3 actualOffset = transform.position - playerTarget.position;
        Vector3 normal = transform.forward;
        normal.y = 0.0f;

        actualOffset = Vector3.Project(actualOffset, normal);
        if (actualOffset.magnitude != forwardOffset) {
            float targetOffsetDiff = (actualOffset.normalized * forwardOffset).magnitude - actualOffset.magnitude;
            transform.position += actualOffset.normalized * targetOffsetDiff;
        }

        //Sync with side movement
        actualOffset = transform.position - playerTarget.position;
        normal = transform.right;
        normal.y = 0.0f;

        actualOffset = Vector3.Project(actualOffset, normal);
        if (actualOffset.magnitude > 0.1f) {
            transform.position -= actualOffset;
        }
    }
}
