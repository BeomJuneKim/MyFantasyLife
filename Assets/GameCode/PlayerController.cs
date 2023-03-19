using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Funtion
    /// </summary>
    float HorizentalAxis;
    float VerticalAxis;

    bool playerWalk;                            // �÷��̾� �ȱ� bool ��
    bool playerJump;                            // �÷��̾� ���� bool ��
    bool playerDash;                            // �÷��̾� ȸ�� bool ��

    bool AttackDown;                            // �÷��̾� ���� bool ��
    bool isJump;                                // �÷��̾� ���� ���� bool ��
    bool isDash;                                // �÷��̾� ȸ�� ���� bool ��
    bool isAttackReady;                         // �÷��̾� ���� �غ� bool ��

    /// <summary>
    /// Component
    /// </summary>
    Vector3 moveVec;
    Vector3 dashVec;                            // ȸ�ǽ� ������ ��ȯ���� �ʵ��� ����
    Rigidbody playerRigidbody;
    Animator animator;
    GameObject nearObj;
    Weapon weapon;

    [Header("PlayerState")]
    public int PlayerHP;
    public int Damage;
    public float Speed = 10f;
    public float AttackDelay;                          // �÷��̾� ���� ������


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        PlayerInput();

        PlayerMove();
        PlayerTurn();
        PlayerJump();
        PlayerDash();
    }

    // Ű �Է� �Լ�
    void PlayerInput()
    {
        HorizentalAxis = Input.GetAxisRaw("Horizontal");
        VerticalAxis = Input.GetAxisRaw("Vertical");

        playerWalk = Input.GetButton("Walk");                       // Left Ctrl
        playerJump = Input.GetButton("Jump");                       // Space Bar
        playerDash = Input.GetButton("Dash");                       // Left Shift
    }

    // �÷��̾� �̵� �Լ�
    void PlayerMove()
    {
        moveVec = new Vector3(HorizentalAxis, 0, VerticalAxis).normalized;

        if (isDash)
        {
            // ȸ�� �����̶� ���� �����̶� ����
            moveVec = dashVec;
        }
        if (playerWalk)
        {
            transform.position += moveVec * Speed * 0.3f * Time.deltaTime;
        }
        else
        {
            transform.position += moveVec * Speed * Time.deltaTime;
        }
        //transform.position += moveVec * Speed * (WalkDown ? 0.3f : 1f) * Time.deltaTime;
        animator.SetBool("isMove", moveVec != Vector3.zero);
        animator.SetBool("isWalk", playerWalk);

    }

    // �÷��̾� ���� �Լ�
    void PlayerTurn()
    {
        // �̵� ���� ī�޶� ����
        transform.LookAt(transform.position + moveVec);
    }

    // �÷��̾� ���� �Լ�
    void PlayerJump()
    {
        if (playerJump && isJump == false && moveVec == Vector3.zero && isDash == false)
        {
            playerRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.SetTrigger("doJump");
            isJump = true;
        }
    }
    // �÷��̾� ȸ�� �Լ�
    void PlayerDash()
    {
        //if (playerJump && isJump == false && moveVec != Vector3.zero && isDash == false)
        if(playerDash && isJump == false && moveVec != Vector3.zero && isDash == false)
        {
            dashVec = moveVec;
            Speed *= 2;
            animator.SetTrigger("doDash");
            isDash = true;

            // ȸ�� ���������� �ӵ�
            Invoke("PlayerDashEnd", 0.2f);

            // �����̸� �־����
        }
    }

    void PlayerDashEnd()
    {
        // ���� �ӵ��� ���ƿ�����
        Speed *= 0.5f;
        isDash = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
}
