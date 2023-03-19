using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    public GameObject Hit_Muzzle;
    public Transform Muzzle_Pos;

    public int Health;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerScript>().Monster = gameObject;
    }


    public void GetDamage(int damage)
    {
        // 파티클 생성
        GameObject muz = Instantiate(Hit_Muzzle, Muzzle_Pos.position, Quaternion.identity);
        // 삭제
        Destroy(muz, 1f);

        Health -= damage;
        if (Health < 1)
        {
            DEAD_Anim();
            player.GetComponent<PlayerScript>().Monster = null;
            Destroy(gameObject, 2f);
        }
    }

    // 몬스터 죽었을 때, 애니메이션
    void DEAD_Anim()
    {
        anim.SetInteger("doDie", 1);
    }
}
