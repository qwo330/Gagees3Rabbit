using UnityEngine;
using UnityEngine.UI;

public class AimController : MonoBehaviour
{
    Image imgAim;
    float offset = 0f;

    void Awake()
    {
        imgAim = GetComponent<Image>();
#if UNITY_EDITOR || UNITY_STANDALONE
        Cursor.visible = false;
#endif
    }

    void Update()
    {
        Vector2 pos = Input.mousePosition;
        imgAim.transform.position = pos;
    }
}
