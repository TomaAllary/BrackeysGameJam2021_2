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
    [SerializeField] float rotateCamSpeed = 50.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
            

        //Rotate around player on second mouse button
        if (Input.GetKey(KeyCode.Mouse1)) {

            Cursor.lockState = CursorLockMode.Locked;

            transform.RotateAround(playerTarget.position, Vector3.up, rotateCamSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);
        }
        else {
            Cursor.lockState = CursorLockMode.None;

        }
    }
}
