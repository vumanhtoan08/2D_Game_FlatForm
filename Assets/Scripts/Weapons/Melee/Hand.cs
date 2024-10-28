using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : Weapon
{
    // Khai báo biến 
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _attackRange;
    [SerializeField] LayerMask _enemyLayer;

    Animator _anim;

    float _attackRateTimmer;
   
    Collider2D[] _hits; //lưu trữ tất cả những enemy trong overlapCircle

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _attackRateTimmer = attackRate;
    }
    private void OnEnable()
    {
        _attackRateTimmer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _attackRateTimmer >= attackRate)
        {
            _anim.SetTrigger("attack");
        }

        _attackRateTimmer += Time.deltaTime;
    }

    public override void Attack()
    {
        _attackRateTimmer = 0;

        // Detect Enemy
        _hits = Physics2D.OverlapCircleAll(_firePoint.position, _attackRange, _enemyLayer);
        if (_hits != null)
        {
            foreach (Collider2D hit in _hits)
            {
                //Hiện tại là có detect tất cả dối tượng lớp enemy trong overlap
                //Sau khi viết xong Heath system quay lại để thực hiện tiếp phần TakeDame của enemy
                Enemy enemy = hit.GetComponent<Enemy>();

                if (enemy != null)
                {
                    // Gọi phương thức TakeDamage
                    enemy.TakeDamage(damage);
                    Debug.Log(enemy.ShowHealth());
                }

                Debug.Log("Hit an enemy: " + hit.gameObject.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_firePoint == null)
            return;

        Gizmos.DrawWireSphere(_firePoint.position, _attackRange);
    }
}
