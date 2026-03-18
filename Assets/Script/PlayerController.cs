using UnityEngine;
using Spine.Unity;
using System.Collections.Generic;

public enum PlayerState { Idle, Move, Jump_Up, Falling, Attack, Dead}
public class PlayerController : MonoBehaviour
{
    [SerializeField] SkeletonAnimation _skeleton;
    [SerializeField] private float _mySpeed;
    [SerializeField] private float _force;

    private Rigidbody2D _myRigid2D;
    private PlayerState _myState;
    private bool _onGround;

    private List<string> _jumpList = new List<string>() { "jump1", "jump2", "jump3" };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _myRigid2D = this.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.OnPlayerMove();
        this.OnPlayerJump();
    }

    private void OnChangeState(PlayerState newState)
    { 
        if (_myState == newState) return;

        _myState = newState;

        switch (_myState)
        {
            case PlayerState.Idle:
                _skeleton.AnimationName = "idle";
                break;
            case PlayerState.Move:
                _skeleton.AnimationName = "run";
                break;
            case PlayerState.Jump_Up:
                _skeleton.AnimationName = _jumpList[Random.Range(0, _jumpList.Count)];
                break;
            case PlayerState.Falling:
                _skeleton.AnimationName = "falldown2";
                break;
            case PlayerState.Dead:
                _skeleton.AnimationName = "die";
                break;
            case PlayerState.Attack:
                
                break;
        }
    }

    private void OnPlayerMove()
    { 
        float moveX = Input.GetAxis("Horizontal");
        _myRigid2D.linearVelocity = new Vector2(moveX * _mySpeed, _myRigid2D.linearVelocityY);

        // Tuc la player dang di chuyen
        if (moveX != 0)
        {
            if (_onGround) this.OnChangeState(PlayerState.Move);
            else
            {
                if (_myRigid2D.linearVelocityY > 0) this.OnChangeState(PlayerState.Jump_Up);
                else this.OnChangeState(PlayerState.Falling);
            }

            // xoay nhan vat trai phai
            float euler = moveX > 0 ? 0f : 180f;
            _skeleton.transform.eulerAngles = Vector3.up * euler;
        }
        else //tuc la player dang dung yen
        {
            if (_onGround) this.OnChangeState(PlayerState.Idle);
            else
            {
                if (_myRigid2D.linearVelocityY > 0) this.OnChangeState(PlayerState.Jump_Up);
                else this.OnChangeState(PlayerState.Falling);
            }
        }
    }

    private void OnPlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (_onGround)
                {
                    _myRigid2D.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
            if (collision.transform.position.y > this.transform.position.y)
            {
                BrickController brick = collision.GetComponent<BrickController>();
                brick?.OnHitBrick();
            }    
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Brick")
        {
            if (collision.transform.position.y < this.transform.position.y)
                _onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Brick")
        {
            _onGround = false;
        }
    }
}
