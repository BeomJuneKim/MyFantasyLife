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
    public int damage;                                  // ���� ������
    public float attackSpeed;                           // ���� �ӵ�
    public BoxCollider AttackArea;                      // ���� ����
    public TrailRenderer trailRenderer;                 // ���� ȿ��

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

    // ���� ���� �ڷ�ƾ
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
