using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject player;
    public PlayerController playerController;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null && playerController.GetPlayerState() == PlayerState.ALIVE)
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(10);

        }

    }

}
