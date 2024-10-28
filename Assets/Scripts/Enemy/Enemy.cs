using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float speed;
    public int health;

    protected int currentHealth;
    protected Transform target;
    protected Vector3 dirToTarget;

    protected BoxCollider2D enemyColl;

    private void Awake()
    {
        enemyColl = GetComponent<BoxCollider2D>();
    }
    protected virtual void OnEnable()
    {
        currentHealth = health;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = health;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    protected virtual void MoveTowardsTarget()
    {
        if (target != null)
        {
            dirToTarget = (target.position - transform.position).normalized; // lấy vector chỉ phường từ this đến target
            transform.position += dirToTarget * speed * Time.deltaTime;
        }
    }

    public virtual void TakeDamage(int amount) // phương thức lấy dame chung của enemy
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;

            // kiểm tra enemy có hết máu không sau khi trừ máu 
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                Hurt();
            }
        }
    }

    public virtual void Die() // phương thức die chung của enemy 
    {
        // có thể thêm một số thứ như animation...
        gameObject.SetActive(false);
        Audio.instance.PlayZbDieClip();
    }

    public virtual void Hurt()
    {
        // có thể thêm hình ảnh và khả năng bất tử sau khi nhận dame
    }

    public int ShowHealth() // chủ yếu để debug
    {
        return currentHealth;
    }
}
