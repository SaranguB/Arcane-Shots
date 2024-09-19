using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float speed = 5f;

    private int health = 100;

    private bool isInvincible;

    private PlayerState playerState;
    [SerializeField] private Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    [SerializeField] private HealthSlider healthSlider;
    [SerializeField] private PlatformSystem platformSystem;

    [SerializeField] private GameFinishController gameFinish;
    [SerializeField] private GameOverController GameOver;
    private int enemiesKilled;
    private int enemiesToBeKilled = 3;

    private PlatformType currentPlatform = PlatformType.PLATFORM1;

    private void Start()
    {
        playerState = PlayerState.ALIVE;
        healthSlider.SetMaxHealth(health);
    }

    private void Update()
    {
        if (SettingsController.Instance.GetGameState() == GameState.PLAY_MODE && playerState == PlayerState.ALIVE)
        {
            GetInput();
        }


    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (SettingsController.Instance.GetGameState() == GameState.PLAY_MODE && playerState == PlayerState.ALIVE)
        {
            Vector2 targetPosition = rb.position + movement * Time.fixedDeltaTime * speed;
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            PreventFromGoingOffScreen(targetPosition);
        }

    }

    private void PreventFromGoingOffScreen(Vector2 targetPosition)
    {
        Vector2 minScreenBounds = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 maxScreenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        minScreenBounds.x += 1f;
        maxScreenBounds.x -= 1f;
        minScreenBounds.y += 1f;
        maxScreenBounds.y -= 1f;

        // Debug.Log(minScreenBounds.x);

        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(targetPosition.x, minScreenBounds.x, maxScreenBounds.x),
            Mathf.Clamp(targetPosition.y, minScreenBounds.y, maxScreenBounds.y)
            );

        rb.MovePosition(clampedPosition);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if (health > 0)
            {
                health -= damage;
                healthSlider.SetHealth(health);
                StartCoroutine(ManageInviniciblity());
            }
            else
            {
                playerState = PlayerState.DEAD;
                GameOver.PlayerLose();
                Destroy(this.gameObject);


            }
        }
    }

    private IEnumerator ManageInviniciblity()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isInvincible = false;
    }

    public void EnemiesKilled()
    {
        enemiesKilled++;
    }

    public void SetEnemiesToBeKilled(int killed)
    {
        enemiesToBeKilled = killed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemiesKilled >= enemiesToBeKilled)
        {

            if (other.gameObject.CompareTag("CheckPoint"))
            {

                platformSystem.SetNextPlatform(currentPlatform);
                currentPlatform = platformSystem.GetCurrentPlatform();
                enemiesKilled = 0;
            }
        }


    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enemiesKilled >= enemiesToBeKilled)
        {
            if (other.gameObject.GetComponent<GameFinishController>() != null)
            {
                gameFinish.PlayerWon();
            }
        }
    }

    public void SetPosition(Vector2 position)
    {
        rb.MovePosition(position);
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }


}
