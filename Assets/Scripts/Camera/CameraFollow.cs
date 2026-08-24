using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private float smoothSpeed = 5f;

    private Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition =
            target.position + offset;

        Vector3 smoothPosition =
            Vector3.Lerp(
                transform.position,
                targetPosition,
                smoothSpeed * Time.deltaTime);

        transform.position = smoothPosition;
    }
}