using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.5f;
    public Rigidbody characterRigidbody;

    private Vector3 movementDirection;

    private void SetMovementDirection()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        if (xAxis == 0 && zAxis == 0) return;

        if (Mathf.Abs(xAxis) >= Mathf.Abs(zAxis))
            movementDirection = new Vector3(xAxis, 0, 0).normalized;
        else
            movementDirection = new Vector3(0, 0, zAxis).normalized;
    }

    private void Update()
    {
        SetMovementDirection();
        characterRigidbody.MovePosition(characterRigidbody.position + (movementDirection * velocity * Time.deltaTime));

        if (movementDirection != Vector3.zero)
            characterRigidbody.rotation = Quaternion.LookRotation(movementDirection);
    }

    private void DotCollected(Collider other)
    {
        Debug.Log("DotCollected");
    }

    private void PowerPelletCollected(Collider other)
    {
        Debug.Log("PowerPelletCollected");
    }

    private void CherryCollected(Collider other)
    {
        Debug.Log("CherryCollected");
    }

    private void EnemyCollision(Collider other)
    {
        Debug.Log("EnemyCollision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dot"))
            DotCollected(other);
        else if (other.CompareTag("PowerPellet"))
            PowerPelletCollected(other);
        else if (other.CompareTag("Cherry"))
            CherryCollected(other);
        else if (other.CompareTag("Enemy"))
            EnemyCollision(other);
    }
}
