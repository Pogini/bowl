using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    [SerializeField]
    public Status status;

    public int currentHp;

    [SerializeField]
    private GameObject damegePrefab;

    [SerializeField]
    private GameObject deathPrefab;


    void Start()
    {
        currentHp = status.hp;
    }

    //ダメージ
    public void OnDamege(int value)
    {
        currentHp -= value;
        GameObject particle = Instantiate(damegePrefab,this.transform) as GameObject;
        // 死亡時
        if (currentHp < 1)
        {
            GameObject particle_death = Instantiate(deathPrefab) as GameObject;
            particle_death.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }

    //回復
    public void onHeal(int value)
    {
        currentHp += value;
    }
}
