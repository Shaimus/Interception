using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public Vector3 velocity;

    [SerializeField]
    private Rigidbody playerRigidbody;

    private Vector3 inputMovement;


    public void OnMovement(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        inputMovement = new Vector3(input.x, 0, input.y);
    }

    void FixedUpdate()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        Vector3 movement = inputMovement * movementSpeed * Time.deltaTime;
        
        if (inputMovement != Vector3.zero)
            velocity = inputMovement;
        else
            velocity = Vector3.zero;

        playerRigidbody.MovePosition(transform.position + movement);

    }
}
