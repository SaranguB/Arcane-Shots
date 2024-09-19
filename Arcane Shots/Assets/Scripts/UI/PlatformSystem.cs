using UnityEngine;

public class PlatformSystem : MonoBehaviour
{
    private PlatformType currentPlatform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Transform platform2Position;
    [SerializeField] private Transform platform3Position;

    [SerializeField] private cameraController cameraController;

    [SerializeField] private GameObject platform1EnemySystem;
    [SerializeField] private GameObject platform2EnemySystem;
    [SerializeField] private GameObject platform3EnemySystem;

    private EnemySpawner platform1EnemySpawner;
    private EnemySpawner platform2EnemySpawner;
    private EnemySpawner platform3EnemySpawner;

    private void Awake()
    {
        platform1EnemySpawner = platform1EnemySystem.GetComponent<EnemySpawner>();
        platform2EnemySpawner = platform2EnemySystem.GetComponent<EnemySpawner>();
        platform3EnemySpawner = platform3EnemySystem.GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        platform1EnemySpawner.SpawnEnemy(3);
    }
    private void Update()
    {

    }

    public void SetNextPlatform(PlatformType current)
    {
        switch (current)
        {
            case PlatformType.PLATFORM1:
                currentPlatform = PlatformType.PLATFORM2;
                platform1EnemySystem.SetActive(false);

                playerController.SetEnemiesToBeKilled(6);
                cameraController.MoveToCurrentPlatform(platform2Position.position);
                platform2EnemySystem.SetActive(true);
                platform2EnemySpawner.SpawnEnemy(6);
                SetPlayerPosition(platform2Position.position);

                break;

            case PlatformType.PLATFORM2:
                currentPlatform = PlatformType.PLATFORM3;
                platform2EnemySystem.SetActive(false);

                playerController.SetEnemiesToBeKilled(10);
                cameraController.MoveToCurrentPlatform(platform3Position.position);
                platform3EnemySpawner.SpawnEnemy(10);

                platform3EnemySystem.SetActive(true);
                SetPlayerPosition(platform3Position.position);

                break;

            case PlatformType.PLATFORM3:
                Debug.Log("Last Platform");
                break;

        }
    }

    private void SetPlayerPosition(Vector2 platform2Position)
    {
        playerController.SetPosition(platform2Position);
    }

    public PlatformType GetCurrentPlatform()
    {
        return currentPlatform;
    }


}

