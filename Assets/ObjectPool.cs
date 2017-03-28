using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab;

    [SerializeField]
    string objectResourcePath;

    [SerializeField]
    List<GameObject> pool;

    [SerializeField]
    int poolMaxSize;

    [SerializeField]
    int initPoolSize;

    int currentPoolSize
    {
        get
        {
            return pool.Count;
        }
    }

    Transform parentTransform;

    // Use this for initialization
    void Start () {

    }

    public void Initialize(string resourcePath = null, Transform parentTransform = null, int initSize = 0, int maxSize = 9999)
    {
        this.objectResourcePath = resourcePath;
        this.poolMaxSize = maxSize;
        this.initPoolSize = initSize;
        this.parentTransform = parentTransform;

        Initialize();
    }

    public void Initialize()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < initPoolSize; i++)
        {
            var obj = CreateNewObject();
            obj.SetActive(false);
        }
    }

    public T Get<T>()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if(pool[i].activeSelf == false)
            {
                pool[i].SetActive(true);
                return pool[i].GetComponent<T>();
            }
        }

        if(poolMaxSize >= currentPoolSize)
        {
            var obj = CreateNewObject();
            obj.SetActive(true);
            return obj.GetComponent<T>();
        }

        return default(T);
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    GameObject CreateNewObject()
    {
        GameObject obj;
        if (string.IsNullOrEmpty(objectResourcePath))
            obj = Instantiate(objectPrefab);
        else
            obj = Instantiate(Resources.Load<GameObject>(objectResourcePath));

        if(parentTransform != null)
            obj.transform.SetParent(parentTransform);
        else
            obj.transform.SetParent(this.transform);

        pool.Add(obj);

        return obj;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
