using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;
    [SerializeField] Vector3 cameraOffset;
    [Range(0f,1f)]
    [SerializeField] float t = .5f;

    private void Start() {
        transform.position = cameraOffset + objectToFollow.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, objectToFollow.position + cameraOffset, t);
    }
}
