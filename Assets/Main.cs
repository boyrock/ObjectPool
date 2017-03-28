using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public string ballPoolName01;
    public string ballPoolName02;
    ObjectPool objectPool01;
    ObjectPool objectPool02;

    // Use this for initialization
    void Start () {
        objectPool01 = PoolManager.Instance.GetObjectPool(ballPoolName01);
        objectPool02 = PoolManager.Instance.GetObjectPool(ballPoolName02);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.A))
        {
            DrawBall01();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DrawBall02();
        }
    }

    void DrawBall01()
    {
        var ball01 = objectPool01.Get<Ball>();
        if(ball01 != null)
        {
            ball01.transform.localPosition = Random.insideUnitSphere * 3f;
            ball01.OnRemove -= RemoveBall01;
            ball01.OnRemove += RemoveBall01;
        }
    }

    void DrawBall02()
    {
        var ball02 = objectPool02.Get<Ball>();
        if (ball02 != null)
        {
            ball02.transform.localPosition = Random.insideUnitSphere * 3f;
            ball02.OnRemove -= RemoveBall02;
            ball02.OnRemove += RemoveBall02;
        }
    }

    private void RemoveBall01(Ball ball)
    {
        objectPool01.Release(ball.gameObject);
    }

    private void RemoveBall02(Ball ball)
    {
        objectPool02.Release(ball.gameObject);
    }
}
