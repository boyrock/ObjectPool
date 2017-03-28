using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public event Action<Ball> OnRemove;

    float t;
    float limitT = 3f;

    // Use this for initialization
    void Start () {
    }

    private void OnEnable()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update () {

        if (this.gameObject.activeSelf == false)
            return;

        t += Time.deltaTime;
        if (t >= limitT)
        {
            Remove();
        }
	}

    void Remove()
    {
        t = 0;
        if(OnRemove != null)
            OnRemove(this);
    }
}
