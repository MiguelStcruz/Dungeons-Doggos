using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Doggo : MonoBehaviour
{
    public GameObject hammerPrefab;
    // basic stats
    private float speed = 6.0f;

    // attack variables
    private bool channeling = false;
    private float channelingTime;
    private bool canAttack = true;
    private float attackRate = 2f;
    private float nextAttack = 0;


    // movement variables
    private bool canInput = true;
    private bool canMove = true;
    private float MovHor;
    private float MovVer;
    private Vector3 Dir;
    private int facingDirection = 1;
    private bool isFacingRight = true;
    private bool isMoving;

    // body components
    private Rigidbody2D rb;
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            CheckInput();
        }
        CheckMovDirection();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        ChannelAttack();
        ApplyMovement();
    }

    private void CheckMovDirection()
    {
        if (isFacingRight && MovHor < 0)
        {
            Flip();
        }
        else if (!isFacingRight && MovHor > 0)
        {
            Flip();
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isMoving", isMoving);
    }

    private void CheckInput()
    {
        if (canMove)
        {
            MovVer = Input.GetAxisRaw("Vertical");
            MovHor = Input.GetAxisRaw("Horizontal");
            if (MovHor != 0 || MovVer != 0)
            {
                isMoving = true;
            }
            else { isMoving = false; }
        }
        if (canAttack && Time.time >= nextAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
            }
        }
    }

    private void ApplyMovement()
    {
        if (canMove)
        {
            Dir = new Vector3(MovHor, MovVer);
            rb.velocity = Dir * speed;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        facingDirection = -facingDirection;
    }

    private void Attack()
    {
        GameObject a = Instantiate(hammerPrefab) as GameObject;
        if (!isFacingRight)
        {
            a.transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y + 1.0f);
            a.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            a.transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y + 1.0f);
        }
        channelingTime = 0.3f;
        channeling = true;
    }

    private void ChannelAttack()
    {
        if (channeling)
        {
            canMove = false;
            canInput = false;
            channelingTime -= Time.deltaTime;
        }
        if (channelingTime <= 0)
        {
            canInput = true;
            channeling = false;
            canMove = true;
        }
    }

    private void OnDrawGizmos()
    {
    }
}
