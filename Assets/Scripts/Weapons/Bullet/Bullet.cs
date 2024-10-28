using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 30f; // Tốc độ bay của đạn 
    Rigidbody2D _body; // Lấy Rigidbody2D của bullet 

    float _lifeTime = 2f; // Thời gian sống của viên đạn
    float _lifeTimer = 0f; // Bộ đếm thời gian sống
    Vector2 _dir; // Hướng bay của viên đạn
    private int _damage; // Thuộc tính damage của viên đạn

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        // Di chuyển viên đạn theo hướng đã được thiết lập
        Shooting(_dir);

        // Cập nhật bộ đếm thời gian sống của viên đạn
        _lifeTimer += Time.fixedDeltaTime;
        if (_lifeTimer >= _lifeTime)
        {
            gameObject.SetActive(false); // Tắt viên đạn sau thời gian sống
            _lifeTimer = 0f; // Đặt lại bộ đếm thời gian
        }
    }

    #region Lấy thuộc tính từ súng vào đạn

    public void SetDirection(Vector2 direction)// Phương thức để thiết lập hướng của viên đạn
    {
        _dir = direction; // Lưu hướng đã được truyền vào
    }
    public void SetDamage(int damage)// Phương thức để lấy dame từ khẩu súng
    {
        _damage = damage;
    }
    // Phương thức để lấy tầm bắn của khẩu súng 
    #endregion

    // Di chuyển viên đạn
    void Shooting(Vector2 dir)
    {
        _body.velocity = dir.normalized * _speed; // Thiết lập tốc độ cho viên đạn
    }

    // Kiểm tra va chạm với tag enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
                Debug.Log(enemy.ShowHealth());
            }
        }
        gameObject.SetActive(false);
    }
}
