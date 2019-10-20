using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 offset = new Vector3(25.0f, 12.0f, -86.4f);

    void LateUpdate()
    {
        Vector3 pos = GameManager.Get.player.transform.position;
        transform.position = new Vector3(pos.x, 0, pos.z) + offset;
    }
}
