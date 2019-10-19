using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 에디터에서만 디버그 로그를 찍는다.
    /// </summary>
    public static void WriteLog(object message, Object context = null)
    {
#if UNITY_EDITOR
        Debug.Log(message, context);
#endif
    }
}
