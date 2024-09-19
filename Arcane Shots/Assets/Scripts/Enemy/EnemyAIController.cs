using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D enemyRB;
    public float speed;
    private float health = 30;
    private Camera cam;

    private float wanderRadius = 3f;
    private Vector2 wanderTarget;

    private float wanderTimer = 0;
    private float wanderTime = 2f;
    private float detectionRadius = 5f;
    private Animator animator;
    private Collider2D enemyCollider;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();

        enemyRB = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetRandomWanderDirecton();

    }
    private void FixedUpdate()
    {
        if (SettingsController.Instance.GetGameState() == GameState.PLAY_MODE &&
            playerController.GetPlayerState() == PlayerState.ALIVE)
        {

            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < detectionRadius)
            {
                
                MoveToPlayer();
            }
            else
            {
                //Debug.Log("Wandering");
               
                Wander();
            }
        }


    }

    private void Wander()
    {
        wanderTimer -= Time.deltaTime;
        if (Vector2.Distance(transform.position, wanderTarget) < 0.1f || wanderTimer < 0)
        {
            SetRandomWanderDirecton();
            wanderTimer = wanderTime;
        }


        Vector2 direction = (wanderTarget - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;


        Vector2 targetPosition = Vector2.MoveTowards(this.transform.position, wanderTarget,
            speed * Time.fixedDeltaTime);
        enemyRB.MovePosition(targetPosition);
        enemyRB.rotation = angle;
    }

    private void SetRandomWanderDirecton()
    {

        float randomAngle = Random.Range(0f, 360f);

        float randomAngleInRadians = randomAngle * Mathf.Deg2Rad;

        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngleInRadians), Mathf.Sin(randomAngleInRadians));

        wanderTarget = (Vector2)transform.position + randomDirection * wanderRadius;

        Vector2 minScreenBounds = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 maxScreenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        minScreenBounds.x += 1f;
        maxScreenBounds.x -= 1f;
        minScreenBounds.y += 1f;
        maxScreenBounds.y -= 1f;


        wanderTarget = new Vector2(Mathf.Clamp(wanderTarget.x, minScreenBounds.x, maxScreenBounds.x),
            Mathf.Clamp(wanderTarget.y, minScreenBounds.y, maxScreenBounds.y));
    }

    private void MoveToPlayer()
    {
        Vector2 playerEnemyDirection = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(playerEnemyDirection.y, playerEnemyDirection.x) * Mathf.Rad2Deg - 90f;

        Vector2 targetPosition = Vector2.MoveTowards(this.transform.position, player.transform.position,
            speed * Time.fixedDeltaTime);

        enemyRB.MovePosition(targetPosition);
        enemyRB.rotation = angle;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BulletController>() != null)
        {
            if (health > 0)
            {

                health = health - 10;
                //Debug.Log("health = " + health);

            }
            else
            {
                playerController.EnemiesKilled();
                enemyCollider.enabled = false;
                animator.SetTrigger("Dead");
            }
        }


    }
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
