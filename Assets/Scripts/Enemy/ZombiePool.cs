using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    public static ZombiePool instance;

    List<GameObject> _pooledObjects = new List<GameObject>(); // tạo một list để lưu trữ gameobj ở đây là bullet
    [SerializeField] int _amountToPool = 30; // max của List

    [SerializeField] GameObject _zombiePrefab; // đối tượng bullet đc gắn vào đây 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // nạp bulletPrefab vào trong List<GameObject>
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_zombiePrefab);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() // lấy những game obj trong pool và active chúng
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
