using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : Health
{
    [SerializeField] int _playerMaxHealth;
    Movement _playerMovementScript;
    public GameObject weaponHolder;
    [SerializeField] protected Image healthBar;

    private void Awake()
    {
        _playerMovementScript = GetComponent<Movement>();
    }

    private void Start()
    {
        _maxHealth = _playerMaxHealth;
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
    }

    public override void TakeDame(int dame)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= dame;
            //  healthBar.fillAmount = (_currentHealth / _maxHealth) * 100; // cách tự nghĩ 2 giá trị vẫn là kiểu int 
            healthBar.fillAmount = ((float)_currentHealth / (float)_playerMaxHealth); // cách ép kiểu 
        }
        if (_currentHealth <= 0)
        {
            PlayerDead();
        }
        else
        {
            PlayerHurt();
        }
    }

    public override void Heal(int healValue)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += healValue;
            healthBar.fillAmount = ((float)_currentHealth / (float)_playerMaxHealth);
        }
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    void PlayerDead()
    {
        //Set animation cho nhân vật
        Debug.Log("Player is dead");
        _playerMovementScript.enabled = false;

        GameManager.instance.PlayerDie();
        weaponHolder.SetActive(false);
        _playerMovementScript.GetComponent<Collider2D>().enabled = false;
        _playerMovementScript.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void PlayerHurt()
    {
        //set animation hurt cho player khi bị đánh 
        //nếu thích thì set thêm thời gian bất tử sau khi bị đánh cho player để player không bị đánh liên tục bằng IEnumerator
    }

    public void PlayerRespawn()
    {
        //Đăt máu người chơi = với max health 
        _currentHealth = _maxHealth;

        //Đặt vị trí của người chơi = với điểm hồi sinh 
        _playerMovementScript.enabled = true;

        weaponHolder.SetActive(true);

        _playerMovementScript.GetComponent<Collider2D>().enabled = true;
        _playerMovementScript.GetComponent<Rigidbody2D>().gravityScale = 7;
        healthBar.fillAmount = _currentHealth = _maxHealth;
    }
    public void ShowCurrentHealth()
    {
        Debug.Log(_currentHealth);
    }
}