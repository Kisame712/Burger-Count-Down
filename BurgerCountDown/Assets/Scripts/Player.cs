using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private PlayerInputActions playerInputActions;

    private Vector2 moveVector;

    private Vector2 rotateVector;

    private void Start()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        moveVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector3 moveDir = new Vector3(moveVector.x, 0f, moveVector.y).normalized * Time.deltaTime;

        transform.position += moveDir * moveSpeed;
    }

    private void RotatePlayer()
    {
        rotateVector = playerInputActions.Player.Rotate.ReadValue<Vector2>();

        transform.forward = Vector3.Slerp(transform.forward, rotateVector * rotateSpeed, Time.deltaTime);

    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }
}
