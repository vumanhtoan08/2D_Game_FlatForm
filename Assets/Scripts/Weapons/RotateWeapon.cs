using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    GameObject _playerPos;
    Vector3 _mouPos; 

    public Vector3 MousePos
    {
        get { return _mouPos; }
        private set { _mouPos = value; }
    }

    private void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Lấy tọa độ của chuột chuyển đổi về vị trí trong unity
        Vector2 dir = _mouPos - transform.position; // lấy hướng từ vị trí của gameobj tới chuột dir = end.pos - start.pos

        if (_playerPos.transform.position.x < _mouPos.x)
        {
            _playerPos.transform.localScale = Vector2.one; 
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // chuyển đổi từ radian thành độ 
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // sử dụng Quaternion để xoay với góc xoay -10
            transform.rotation = rotation;
        }
        else if (_playerPos.transform.position.x > _mouPos.x)
        {
            _playerPos.transform.localScale = new Vector2(-1, 1);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward); // Sử dụng Quaternion để xoay với góc xoay +170
            transform.rotation = rotation;
        }
    }
}
