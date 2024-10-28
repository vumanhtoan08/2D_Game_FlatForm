using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieNormal : Zombie
{
    public float attackRate = 1f; // tốc độ đánh của Zombie
    private Coroutine damageCoroutine;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Bắt đầu gây sát thương liên tục
                damageCoroutine = StartCoroutine(DealDamage(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Khi zombie không còn va chạm với người chơi, dừng coroutine
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DealDamage(PlayerHealth playerHealth)
    {
        // Gây sát thương đầu tiên ngay lập tức
        playerHealth.TakeDame(damage);
        playerHealth.ShowCurrentHealth();

        // Lặp lại việc gây sát thương mỗi 0.5 giây
        while (true)
        {
            yield return new WaitForSeconds(attackRate);
            playerHealth.TakeDame(damage);
            playerHealth.ShowCurrentHealth();
        }
    }

    public override void Die()
    {
        GameManager.instance.AddScore(1); // tăng điểm nếu enemy chết 
        //
        base.Die();
    }
}
