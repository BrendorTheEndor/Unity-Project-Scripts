using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // set up CameraController attributes
    public float panSpeed = 30f;
    private bool doMovement = false;
    public float scrollSpeed = 4f;
    public float minY = 20f;
    public float maxY = 150f;

    // Update is called once per frame
    void Update()
    {

        // disable camera movement if the game is lost or won
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        // unlock or relock camera movement
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        // don't allow the camera to moved if camera is locked
        if (!doMovement)
        {
            return;
        }

        // allow for WASD, shift, space, and scroll wheel to control camera

        if ((Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)) && transform.position.z <= 93.5)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) && transform.position.z >= -18.1)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) && transform.position.x <= 80.3)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x >= -3.7)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            pos.y += (panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            pos.y -= (panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }

}
