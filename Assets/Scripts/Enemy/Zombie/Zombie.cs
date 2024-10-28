using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Zombie : Enemy
{
    public float distanceDetect;
    public LayerMask playerMask;

    // bien dung de patroll 
    public Vector3 patrollLeft;
    public Vector3 patrollRight;

    public override void Start()
    {
        base.Start();
        patrollLeft = new Vector3(transform.position.x - 3, transform.position.y);
        patrollRight = new Vector3(transform.position.x + 3, transform.position.y);
    }

    public override void Update()
    {
        base.Update();
        if (ZombieSeePlayer())
        {
            MoveTowardsTarget();
        }
        else
        {
            Patrolling(patrollLeft, patrollRight);
        }

        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }

    // nghiên cứu để làm ra một hệ thống detect nếu detect phát hiện ra target thì mới chomove
    public bool ZombieSeePlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast(enemyColl.bounds.center, enemyColl.bounds.size, 0,
                                            new Vector2(transform.localScale.x, 0), distanceDetect, playerMask);

        return hit.collider != null;
    }

    public void Patrolling(Vector3 left, Vector3 right)
    {
        // Di chuyển Zombie về phía trước theo hướng hiện tại
        Vector3 dirPatroll = new Vector3(transform.localScale.x, 0, 0);
        transform.position += dirPatroll * speed / 2 * Time.deltaTime;

        // Nếu Zombie đã vượt qua giới hạn trái (patrollLeft)
        if (transform.position.x < left.x && dirPatroll.x < 0)
        {
            // Đảo hướng sang phải
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        // Nếu Zombie đã vượt qua giới hạn phải (patrollRight)
        if (transform.position.x > right.x && dirPatroll.x > 0)
        {
            // Đảo hướng sang trái
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            MoveTowardsTarget();
        }
    }
}
