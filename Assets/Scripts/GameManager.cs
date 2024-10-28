using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton 
    public static GameManager instance;

    // Khai báo biến
    private bool _isGameOver = false; // kiểm tra game kết thúc hay chưa
    private int _score = 0; // số zombie giết được 
    private int _currentAmmo; // sẽ làm UI để hiển thị số đạng hiện có  

    public GameObject GameOverScreen; // UI hiển thị màn hình game Over;
    public GameObject GameActiveScreen;
    public GameObject GameCompleteScreen;
    public GameObject Boss;
    public GameObject SpawnerZombie;
    private bool bossSpawn = false;

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
    // Update is called once per frame
    void Update()
    {
        if (_isGameOver)
        {
            GameOver();
        }
        if (_score == 2 && !bossSpawn)
        {
            bossSpawn = true;
            Boss.SetActive(true);
            SpawnerZombie.SetActive(false);
        }
    }

    public void AddScore(int scoreVal) // giết được zombie sẽ dùng hàm này để them điểm
    {
        _score += scoreVal;
    }
    public int GetScore() // dùng để lấy điểm số 
    {
        return _score;
    }
    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        GameActiveScreen.SetActive(false);
        GameCompleteScreen.SetActive(false);
    }
    public void GameComplete()
    {
        GameCompleteScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        GameActiveScreen.SetActive(false);
        Audio.instance.PlayCompeleteClip();
    }
    public void PlayAgain()
    {
        GameActiveScreen.SetActive(true);
        GameCompleteScreen.SetActive(false);
        GameOverScreen.SetActive(false);

        _score = 0;
        _isGameOver = false;
        bossSpawn = false;
        SpawnerZombie.SetActive(true);

        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.PlayerRespawn();

        if (Boss != null)
        {
            Boss.SetActive(false);
        }

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(false);
        }
    }
    public bool PlayerDie()
    {
        return _isGameOver = true;
    }

    public void ShowScore()
    {
        Debug.Log(_score);
    }
    public int SetCurrentAmmo(int AmmoVal)
    {
        return _currentAmmo = AmmoVal;
    }
    public int GetCurrentAmmo()
    {
        return _currentAmmo;
    }
}
