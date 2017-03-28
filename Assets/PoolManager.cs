using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehaviour<PoolManager> {

    public Dictionary<string,ObjectPool> pools;

    [SerializeField]
    ObjectPool objectPool;

	// Use this for initialization
	protected override void Awake () {

        pools = new Dictionary<string, ObjectPool>();
        var poolsInChild = this.GetComponentsInChildren<ObjectPool>();

        for (int i = 0; i < poolsInChild.Length; i++)
        {
            var pool = poolsInChild[i];
            if (string.IsNullOrEmpty(pool.name) == false && pools.ContainsKey(pool.name) == false)
            {
                pool.Initialize();
                pools.Add(pool.name, pool);
            }
        }
    }

    public ObjectPool GetObjectPool(string poolName)
    {
        if (pools.ContainsKey(poolName))
            return pools[poolName];

        return null;
    }

    public void CreateNewObjectPool(string poolName)
    {
        var pool = Instantiate(objectPool);
        pool.name = poolName;
        pool.transform.SetParent(this.transform);
        pools.Add(poolName, pool);
    }

    // Update is called once per frame
    void Update () {

	}
}
