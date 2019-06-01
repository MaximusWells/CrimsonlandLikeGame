using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody player;
    [SerializeField]
    protected float movSpeed;
    protected int groundMask;
    protected float rayLength = 100f;
    public static Animator animator;
    public int health;

    private const float sphereRadius = 1.5f;

    private void Awake()
    {
        groundMask = LayerMask.GetMask("Ground");
        health = 100;
    }

    void Start ()
    {
        player = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    void FixedUpdate () {
        Movements();
        LookAtPointer();
        AttackPlayer();
    }

    void Movements()
    {
        //Player moves
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movementInput = new Vector3(moveHorizontal, 0f, moveVertical);
        Vector3 moves = movementInput.normalized * movSpeed;
        player.velocity = moves;
        player.MovePosition(transform.position + moves);

        //Animation of players moves
        if (moveHorizontal != 0.0f )
        {
            animator.SetFloat("run_Guard", 1.0f);
        }
        else if (moveVertical != 0.0f)
        {
            animator.SetFloat("run_Guard", 1.0f);
        }
        else
        {
            animator.SetFloat("run_Guard", 0.0f);
        }
    }

    void LookAtPointer()
    {
        //Players rotation
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit groundHit;

        if (Physics.Raycast(camRay, out groundHit, rayLength, groundMask))
        {
            Vector3 playerToMouse = groundHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion playerRotation = Quaternion.LookRotation(playerToMouse);
            player.MoveRotation(playerRotation);
        }

    }

    void AttackPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, sphereRadius);
        foreach(Collider enemy in hitColliders)
        {
            EnemyController enemyScript = enemy.GetComponent<EnemyController>();
            if (enemy.tag == "Enemy")
            {
                enemyScript.Attack();
            }
        }
    }

    public void GetDamaged(int damage)
    {
        health -= damage;
    }

}
