using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 3.5f;
    public int scoreForDot = 10;
    public int scoreForEnemy = 100;
    public float pelletPowerUpDuration = 8;
    public Rigidbody characterRigidbody;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    private Vector3 movementDirection;
    private int currentScore;
    private float remainingPowerUpDuration;

    private bool PowerUpActive => remainingPowerUpDuration > 0;

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
        remainingPowerUpDuration -= Time.deltaTime;

        SetMovementDirection();
        characterRigidbody.MovePosition(characterRigidbody.position + (movementDirection * velocity * Time.deltaTime));

        if (movementDirection != Vector3.zero)
            characterRigidbody.rotation = Quaternion.LookRotation(movementDirection);
    }

    private void IncreaseScore(int delta)
    {
        currentScore += delta;
        scoreText.text = $"Score: {currentScore}";
    }

    private void DotCollected(Collider other)
    {
        IncreaseScore(scoreForDot);
        Destroy(other.gameObject);

        // TODO: Play Animation / Sounds
        // TODO: Win Game logic
    }

    private void PowerPelletCollected(Collider other)
    {
        remainingPowerUpDuration = pelletPowerUpDuration;
        Destroy(other.gameObject);

        // TODO: Play Animation / Sounds / Change enemies color
    }

    private void CherryCollected(Collider other)
    {
        Debug.Log("CherryCollected");
        // TODO: Cherry collection logic
    }

    private void EnemyCollision(Collider other)
    {
        if (PowerUpActive)
        {
            IncreaseScore(scoreForEnemy);
            Destroy(other.gameObject);

            // TODO: Return enemy to center instead of killing it
        }
        else
        {
            enabled = false;
            gameOverText.gameObject.SetActive(true);
        }
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
