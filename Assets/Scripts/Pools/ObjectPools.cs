using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Fields 
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 30;
    [SerializeField] private Transform container;
    #endregion

    #region Private Fields 
    private readonly Queue<GameObject> pool = new Queue<GameObject>();
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Prewarm();
    }
    #endregion

    #region Private Methods
    private void Prewarm()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, container);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    #endregion
    #region Public Methods
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefab, container);
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(container);
        pool.Enqueue(obj);
    }
    #endregion
}