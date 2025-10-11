using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 2f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}
