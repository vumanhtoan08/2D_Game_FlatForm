using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMap : MonoBehaviour
{
    public Transform player;
    public float currentDis = 0f;
    public float limitDis;
    public float respawnDis;

    private Vector3 pos; // luu vi tri cua obj 

    // Update is called once per frame
    void Update()
    {
        this.GetDistance();
        this.Spawning();
        this.PreSpawning();
    }

    protected void Spawning()
    {
        // tính toán nếu mà khoảng cách hiện tại của người chơi nếu vị trí hiện tại của người chơi mà nhỏ hơn giới hạn map thì không làm gì cả 
        if (this.currentDis < this.limitDis) return;
        pos = transform.position;
        pos.x += this.respawnDis;
        transform.position = pos;
    }

    protected void PreSpawning()
    {
        if (this.currentDis > (-this.limitDis - 1f)) return;
        pos = transform.position;
        pos.x -= this.respawnDis;
        transform.position = pos;
    }

    protected void GetDistance()
    {
        this.currentDis = this.player.position.x - this.transform.position.x;
    }
}
