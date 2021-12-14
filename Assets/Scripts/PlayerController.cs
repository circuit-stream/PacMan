using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.5f;
    public CharacterController characterController;

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        characterController.SimpleMove(new Vector3(xAxis, 0, zAxis) * velocity);
    }
}
