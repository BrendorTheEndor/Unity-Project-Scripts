using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float playerMoveSpeed;
    [SerializeField] bool usingController = true;
    Rigidbody playerRigidbody;

    float horizontalThrow = 0f;
    float verticalThrow = 0f;
    Camera mainCamera;

    Vector3 currentPos;
    Vector3 posLastFrame;

    bool isDead = false;

    // Start is called before the first frame update
    void Start() {
        playerRigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentPos = transform.position;
        posLastFrame = currentPos;

        //Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update() {

        // My idea for making the player face the direction they're moving; kinda works
        //currentPos = transform.position;
        //Vector3 directionMoving = currentPos - posLastFrame;
        if(isDead) { return; }

        ProcessMovement();

        playerRigidbody.angularVelocity = new Vector3(0f, 0f, 0f); // Used becasue sometimes bumping into enemies caused this weird rotation effect

        //transform.LookAt(directionMoving);


        if(usingController) {
            RotateUsingController();
        }
        else {
            FaceMouse();
        }


        //posLastFrame = currentPos;
    }

    public void FaceMouse() {
        Ray rayFromCamera = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenth;

        if(groundPlane.Raycast(rayFromCamera, out rayLenth)) {
            Vector3 pointToLookAt = rayFromCamera.GetPoint(rayLenth); // Get the point on the plane
            Debug.DrawLine(rayFromCamera.origin, pointToLookAt, Color.blue);
            transform.LookAt(new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z));
        }
    }

    public void RotateUsingController() {
        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
        if(playerDirection.sqrMagnitude > 0.0f) {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
    }

    private void ProcessMovement() {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        float horizontalOffset = horizontalThrow * playerMoveSpeed * Time.deltaTime;
        float verticalOffset = verticalThrow * playerMoveSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + horizontalOffset, transform.position.y, transform.position.z + verticalOffset);
    }

    public bool GetControllerBool() { return usingController; }

    public void SetControllerBool(bool b) { usingController = b; }

    public void PlayerHasDied() {
        // TODO set death animation
        isDead = true;
    }
}
