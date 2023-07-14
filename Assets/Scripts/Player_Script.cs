using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D _rigid;

    [SerializeField]
    private float _jumpForce = 5.0f;  
    [SerializeField]
    private bool _grounded = false; 

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private float _speed = 1.0f;
    private PlayerAnimation _animation;

    private bool _resetJump = false; 

    // variable for jumpForce
    // variable for grounded = false
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        //assign handle to playeranimation 
        _animation = GetComponent<PlayerAnimation>();
    }
 
    void Update()
    {
        DidMove();  
    }

    void DidMove() 
    {
        Run();
        Jump();
    }

    private void Run() 
    {
        float move = Input.GetAxisRaw("Horizontal");
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _animation.Move(move);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space ) && IsGrounded() == true) 
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpNeededRoutine());
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitInfo =  Physics2D.Raycast(transform.position, Vector2.down , 0.8f, _groundLayer);
        if (hitInfo.collider != null) 
        {
            if (_resetJump == false) 
                return true;
        }
         
        return false;
    }

    IEnumerator ResetJumpNeededRoutine() 
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
} 


