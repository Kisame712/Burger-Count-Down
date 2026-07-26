using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Evo.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float upDownRange;

    [SerializeField] private CinemachineCamera playerCamera;

    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask tableLayer;

    [SerializeField] private Transform sauceBottle;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Slider mouseSentivitySlider;


    private CharacterController playerController;

    private PlayerInputActions playerInputActions;

    private Vector2 moveVector;

    private Vector3 currentMovement;

    private float verticalRotation;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController = GetComponent<CharacterController>();

        playerInputActions.Player.Interact.performed += PlayerInputActions_Interact;

        playerInputActions.Player.Pause.performed += PlayerInputActions_Pause;

        resumeButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
        });
    }

    private void PlayerInputActions_Pause(InputAction.CallbackContext obj)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    private void PlayerInputActions_Interact(InputAction.CallbackContext obj)
    {

        RaycastHit raycastHit;
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if(GameManager.Instance.GetCurrentTask() == 0)
        {
            if(Physics.Raycast(ray, out raycastHit, interactRadius, interactableLayer))
            {
                if(raycastHit.transform.TryGetComponent<TrashObject>(out TrashObject trashObject))
                {
                    Destroy(trashObject.gameObject);
                }
            }
        }

        else if(GameManager.Instance.GetCurrentTask() == 1)
        {
            if (Physics.Raycast(ray, out raycastHit, interactRadius, tableLayer))
            {
                if(raycastHit.transform.TryGetComponent<SauceSpawnPoint>(out SauceSpawnPoint sauceSpawnPoint))
                {
                    if (sauceSpawnPoint.TableHasSauce())
                    {
                        return;
                    }
                    sauceSpawnPoint.SetHasSauce(true);
                    Instantiate(sauceBottle, sauceSpawnPoint.transform);
                    sauceSpawnPoint.BottlePlaced();
                }
            }
        }

        else
        {
            if (Physics.Raycast(ray, out raycastHit, interactRadius, interactableLayer))
            {
                if(raycastHit.transform.TryGetComponent<TrayObject>(out TrayObject trayObject))
                {
                    Destroy(trayObject.gameObject);
                }
            }

        }
        
    }

    private Vector3 GetWorldDirection()
    {
        moveVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3(moveVector.x, 0f, moveVector.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();

    }

    private void MovePlayer()
    {
        Vector3 worldDirection = GetWorldDirection();
        currentMovement.x = worldDirection.x * moveSpeed;
        currentMovement.z = worldDirection.z * moveSpeed;
        ApplyGravity();

        playerController.Move(currentMovement * Time.deltaTime);
 
    }

    private void ApplyGravity()
    {
        currentMovement.y += Physics.gravity.y * Time.deltaTime;
    }

    private void RotatePlayer()
    {
        Vector2 rotateVector = playerInputActions.Player.Rotate.ReadValue<Vector2>();

        float rotationAmountX = rotateVector.x * mouseSensitivity;
        float rotationAmountY = rotateVector.y * mouseSensitivity;

        ApplyHorizontalRotation(rotationAmountX);
        ApplyVerticalRotation(rotationAmountY);
        
    }

    private void ApplyHorizontalRotation(float rotateAmount)
    {
        transform.Rotate(0, rotateAmount , 0);
    }

    private void ApplyVerticalRotation(float rotateAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotateAmount, -upDownRange, upDownRange);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
        Time.timeScale = 1;
    }

    public void ChangeMouseSensitivity()
    {
        mouseSensitivity = mouseSentivitySlider.value;
    }
}
