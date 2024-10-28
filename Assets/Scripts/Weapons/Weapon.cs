using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Khai báo biến 
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRate; // tốc độ bắn của vũ khí

    public abstract void Attack(); // phương thức Attack mà vũ khí nào cũng phải có
}
