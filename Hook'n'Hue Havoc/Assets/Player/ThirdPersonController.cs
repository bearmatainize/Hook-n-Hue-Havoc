using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    [Header("Reference Fields")]
    public LayerMask camRaycastMask;
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform cameraPivot;

    [Header("Numeric Fields")]
    public float controllerLookSmoothTime = 0.1f;
    public float lookSmoothTime = 0.1f;
    public float cameraDistance = 4f;
    [Space]
    public Vector3 pivotOffset = new Vector3(0f, 1f, 0f);
    public Vector2 xRotationLimits = new Vector2(-45, 45);

    public float gravity = 9.81f;
    public float sensitivity = 12f;
    public float moveSpeed = 6f;

    // Used on the Player Gameobject with the character controller for smooth rotating
    private float yVelocity;
    private float yAngleOffset;
    private float currentControllerYRotation;
    private float targetControllerYRotation;
    private float controllerYRotationVelocity;

    // Used on the Player Camera for smooth rotating
    private float currentCameraXRotation;
    private float currentCameraYRotation;
    private float xRotationVelocity;
    private float yRotationVelocity;
    private float targetXRotation;
    private float targetYRotation;

    public float deadZone = 0.1f;
    public float xInputMovement;
    public float yInputMovement;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        setCameraLook();
        setControllerLook();

        xInputMovement = Input.GetAxisRaw("Horizontal");
        yInputMovement = Input.GetAxisRaw("Vertical");
    }

    void setControllerLook() {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        if (xInput * xInput + yInput * yInput > deadZone) {
            targetControllerYRotation = Mathf.Atan2(xInput, yInput) * Mathf.Rad2Deg;
            targetControllerYRotation -= yAngleOffset;
        }

        currentControllerYRotation = Mathf.SmoothDampAngle(currentControllerYRotation, targetControllerYRotation, ref controllerYRotationVelocity, controllerLookSmoothTime);
        controller.transform.eulerAngles = new Vector3(0f, currentControllerYRotation, 0f);
    }

    void setCameraLook() {
        if (Input.GetMouseButton(1)) {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetYRotation += mouseX * sensitivity;
            targetXRotation += mouseY * sensitivity;
            targetXRotation = Mathf.Clamp(targetXRotation, xRotationLimits.x, xRotationLimits.y);
        }

        currentCameraXRotation = Mathf.SmoothDampAngle(currentCameraXRotation, targetXRotation, ref xRotationVelocity, lookSmoothTime);
        currentCameraYRotation = Mathf.SmoothDampAngle(currentCameraYRotation, targetYRotation, ref yRotationVelocity, lookSmoothTime);
        cameraPivot.eulerAngles = new Vector3(currentCameraXRotation, currentCameraYRotation, 0f);

        Ray camRay = new Ray(cameraPivot.position, -cameraPivot.forward);
        float maxDistance = cameraDistance;
        if (Physics.SphereCast(camRay, 0.25f, out RaycastHit hitInfo, cameraDistance, camRaycastMask)) {
            maxDistance = (hitInfo.point - cameraPivot.position).magnitude - 0.25f;
        }

        cameraTransform.localPosition = Vector3.forward * -(maxDistance - 0.1f);
        yAngleOffset = Mathf.Atan2(cameraPivot.forward.z, cameraPivot.forward.x) * Mathf.Rad2Deg - 90f;
    }

    public void movePivot() {
        cameraPivot.position = pivotOffset + controller.transform.position;
    }

    public void moveController() {
        controller.Move(controller.transform.forward * moveSpeed * Time.deltaTime);

        yVelocity = controller.isGrounded ? 0f : yVelocity - gravity * Time.deltaTime;
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}
