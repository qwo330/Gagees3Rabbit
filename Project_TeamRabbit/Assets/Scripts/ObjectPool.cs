using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //const string _prefapPath = "/";
    const int defaultCount = 30;

    static ObjectPool instance;
    public static ObjectPool Get
    {
        get
        {
            //if (instance == null)
            //    instance = new ObjectPool();
            return instance;
        }
    }

    Dictionary<string, Stack<GameObject>> Pool;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CreateObjectPool()
    {
        Pool = new Dictionary<string, Stack<GameObject>>();
    }

    /// <summary>
    /// 불러올 프리팹 이름으로 오브젝트풀 생성
    /// </summary>
    public void AddObjectPool(string name, int count = defaultCount)
    {
        Stack<GameObject> stack;

        if (Pool.ContainsKey(name))
        {
            stack = Pool[name];
        }
        else
        {
            stack = new Stack<GameObject>();
            Pool.Add(name, stack);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Resources.Load(name) as GameObject;
            GameObject obj = Instantiate(prefab, transform);
            obj.name = name;
            stack.Push(obj);
        }
    }
    
    /// <summary>
    /// 오브젝트를 풀에서 가져간다. 다 사용하면 풀을 count만큼 추가 생성한다.
    /// </summary>
    public GameObject GetObject(string name)
    {
        var stack = Pool[name];
        if (stack.Count == 0)
        {
            AddObjectPool(name);
            GameManager.WriteLog("======= " + name + " Object is increased. =======");
        }
        return stack.Pop();
    }

    /// <summary>
    /// 사용한 오브젝트를 다시 풀로 반환한다. obj의 이름을 key로 사용한다.
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        string name = obj.name;
        var stack = Pool[name];
        stack.Push(obj);
    }
}