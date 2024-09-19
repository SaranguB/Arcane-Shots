using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        DestroyBullet();

    }

    private void Update()
    {
        DestroyOutOfBounds();
    }

    private void DestroyOutOfBounds()
    {
        Vector2 minScreenBounds = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 maxScreenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        if(this.transform.position.x < minScreenBounds.x || this.transform.position.x > maxScreenBounds.x ||
            this.transform.position.y < minScreenBounds.y || this.transform.position.y > maxScreenBounds.y)
        {
            DestroyBullet() ;
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
