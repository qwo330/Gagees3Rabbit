using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static ObjectPool instance;
    public static ObjectPool Get
    {
        get
        {
            if (instance == null)
                instance = new ObjectPool();
            return instance;
        }
    }


}
