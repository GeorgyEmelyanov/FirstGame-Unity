using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            Zombies,
            Roads,
        }

        public ObjectType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [SerializeField]
    private List<ObjectInfo> _objectsInfo;

    private Dictionary<ObjectInfo.ObjectType, Pool> pools;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitPool();
    }

    private void InitPool()
    {
        pools = new Dictionary<ObjectInfo.ObjectType, Pool>();

        var emptyGo = new GameObject();

        foreach (var obj in _objectsInfo)
        {
            var container = Instantiate(emptyGo, transform, false);
            container.name = obj.Type.ToString();

            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++) 
            {
                var go = InstantiateObject(obj.Type, container.transform);
                pools[obj.Type].Objects.Enqueue(go);
            }
        }

        Destroy(emptyGo);
    }

    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var go = Instantiate(_objectsInfo.Find(x => x.Type == type).Prefab, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        var obj = pools[type].Objects.Count > 0 ?
            pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);

        obj.SetActive(true);

        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        pools[obj.GetComponent<IPooledObject>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }
}
