using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    // khai báo biến 
    public TextMeshProUGUI score;
    public TextMeshProUGUI currentBullet;

    // Update is called once per frame
    void Update()
    {
        score.SetText(GameManager.instance.GetScore().ToString());
        currentBullet.SetText("x "+GameManager.instance.GetCurrentAmmo().ToString());
    }
    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonPlayAgain()
    {
        GameManager.instance.PlayAgain();
    }
}
