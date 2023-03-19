using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        CloseUpAttack,
        StandOffAttack
    };
    public WeaponType weaponType;
    public int damage;                                  // 무기 데미지
    public float attackSpeed;                           // 공격 속도
    public BoxCollider AttackArea;                      // 공격 범위
    public TrailRenderer trailRenderer;                 // 공격 효과

    void Start()
    {

    }

    void Update()
    {

    }

    public void UseWeapon()
    {
        if (weaponType == WeaponType.CloseUpAttack)
        {
            StopCoroutine(Swing());
            StartCoroutine(Swing());
        }
    }

    // 근접 공격 코루틴
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        AttackArea.enabled = true;
        trailRenderer.enabled = true;

        yield return new WaitForSeconds(0.3f);
        AttackArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailRenderer.enabled = false;
    }

}
