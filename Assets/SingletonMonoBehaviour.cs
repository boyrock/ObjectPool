using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T instance;
    private bool _inited = false;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogWarning(typeof(T) + " is nothing");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        CheckInstance();
        SingletonInitialize();
    }

    bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this);
        return false;
    }
    void SingletonInitialize()
    {
        if (_inited == false)
        {
            //Debug.Log(this.GetType().Name + " Initialize.");
            _inited = true;
        }
    }
}