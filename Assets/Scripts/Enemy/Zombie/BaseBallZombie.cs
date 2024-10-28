using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseBallZombie : Enemy
{
    // khai báo biến
    public float attackRange;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private Animator anim;

    public float attackCoolDown = 3f;
    private float attackCoolDownTimmer = 0;

    Collider2D hit; // lưu collider của player nếu đánh trúng

    public Image healthbar;

    protected override void OnEnable()
    {
        base.OnEnable();
        healthbar.fillAmount = currentHealth / health;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (attackCoolDownTimmer >= attackCoolDown && DetectPlayer())
        {
            if (currentHealth <= (health / 2))
            {
                anim.SetTrigger("attackhard");
                attackCoolDownTimmer = 0;
            }
            else
            {
                // Set animation attack 
                anim.SetTrigger("attack");
                attackCoolDownTimmer = 0;
            }

        }

        if (!DetectPlayer())
        {
            MoveTowardsTarget();
        }
        attackCoolDownTimmer += Time.deltaTime;

        if (currentHealth <= (health / 2))
        {
            speed = 3;
            attackRange = 2;
        }
    }

    public void AttackMelee()
    {
        hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (currentHealth <= (health / 2))
        {
            // Tấn công Hard
            if (hit != null)
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDame(damage * 3 / 2);
                    playerHealth.ShowCurrentHealth();
                }
            }
        }
        else
        {
            // Tấn công normal
            if (hit != null)
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDame(damage);
                    playerHealth.ShowCurrentHealth();
                }
            }
        }
    }

    private bool DetectPlayer()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
    }

    public override void TakeDamage(int amount) // phương thức lấy dame chung của enemy
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
            healthbar.fillAmount = (float)currentHealth / (float)health;

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

    public override void Die()
    {
        base.Die();

        GameManager.instance.GameComplete();
    }

    protected override void MoveTowardsTarget()
    {
        base.MoveTowardsTarget();
        if (transform.position.x < target.position.x)
        {
            transform.localScale = Vector3.one;
            healthbar.fillOrigin = -1;
        }
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            healthbar.fillOrigin = 1;
        }
    }
}
