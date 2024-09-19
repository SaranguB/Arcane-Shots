
using System.Collections;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private float duration;

    public void MoveToCurrentPlatform(Vector2 platformPosition)
    {
        Vector3 targetposition = new Vector3(platformPosition.x, platformPosition.y, transform.position.z);
        StartCoroutine(MoveCameraCoroutine(targetposition));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
