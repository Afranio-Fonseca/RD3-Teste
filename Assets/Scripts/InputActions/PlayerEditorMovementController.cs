using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEditorMovementController : MonoBehaviour
{
    [SerializeField] float moveValueMult = 1;
#if UNITY_EDITOR
    EditorPlayerInput editorPlayerInput;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        editorPlayerInput = new EditorPlayerInput();
        characterController = GetComponent<CharacterController>();

        editorPlayerInput.CharacterControls.Move.started += OnMovementInput;
        editorPlayerInput.CharacterControls.Move.canceled += OnMovementInput;
    }

    void OnMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementPressed)
        {
            MakeCurrentMovementForward();

            characterController.Move(currentMovement * Time.deltaTime * moveValueMult);
        }
    }

    void MakeCurrentMovementForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentMovement = transform.TransformDirection(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentMovement = transform.TransformDirection(Vector3.forward * -1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentMovement = transform.TransformDirection(Vector3.right * -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentMovement = transform.TransformDirection(Vector3.right);
        }
    }
    private void OnEnable()
    {
        editorPlayerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        editorPlayerInput.CharacterControls.Disable();
    }

#endif
}
