using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.5f;
    public CharacterController characterController;

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 movement;

        if (xAxis == 0 && zAxis == 0) return;

        if (Mathf.Abs(xAxis) >= Mathf.Abs(zAxis))
            movement = new Vector3(xAxis, 0, 0).normalized;
        else
            movement = new Vector3(0, 0, zAxis).normalized;

        characterController.SimpleMove(movement * velocity);
    }
}
