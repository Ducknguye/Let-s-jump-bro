using Spine.Unity;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState { Idle, Move, Jump_Up, Falling, Attack, Dead}
public class PlayerController : MonoBehaviour
{
    [SerializeField] SkeletonAnimation _skeleton;
    [SerializeField] private float _mySpeed;
    [SerializeField] private float _force;

    private Rigidbody2D _myRigid2D;
    private PlayerState _myState;
    private bool _onGround;
    private bool _canBreakBrick;
    private bool _wasOnGround;

    private List<string> _jumpList = new List<string>() { "jump1", "jump2", "jump3" };
    private float _moveInput;

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
        if (_isDead) return;

        float keyboardInput = Input.GetAxis("Horizontal");

        // ưu tiên UI nếu đang bấm
        float finalInput = Mathf.Abs(_moveInput) > 0 ? _moveInput : keyboardInput;

        OnPlayerMove(finalInput);

        // nhảy bằng bàn phím
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (!_wasOnGround && _onGround)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.land);
            }
        }
        _wasOnGround = _onGround;
    }

    private void OnEnable()
    {
        InputManager.EvtMove += OnMove;
        InputManager.EvtJump += OnJump;
    }

    private void OnDisable()
    {
        InputManager.EvtMove -= OnMove;
        InputManager.EvtJump -= OnJump;
    }

    private void OnMove(float value)
    {
        _moveInput = value;
    }

    private void OnJump()
    {
        if (_onGround && !_isDead)
        {
            _myRigid2D.linearVelocity = new Vector2(_myRigid2D.linearVelocity.x, 0); // FIX: reset lực Y
            _myRigid2D.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            SoundManager.instance.PlaySFX(SoundManager.instance.jump);
        }
    }

    private void OnChangeState(PlayerState newState)
    {
        if (_isDead && newState != PlayerState.Dead) return;

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

    private void OnPlayerMove(float moveX)
    {
        _myRigid2D.linearVelocity = new Vector2(moveX * _mySpeed, _myRigid2D.linearVelocityY);

        if (moveX != 0)
        {
            if (_onGround) this.OnChangeState(PlayerState.Move);
            else
            {
                if (_myRigid2D.linearVelocityY > 0) this.OnChangeState(PlayerState.Jump_Up);
                else this.OnChangeState(PlayerState.Falling);
            }

            float euler = moveX > 0 ? 0f : 180f;
            _skeleton.transform.eulerAngles = Vector3.up * euler;
        }
        else
        {
            if (_onGround) this.OnChangeState(PlayerState.Idle);
            else
            {
                if (_myRigid2D.linearVelocityY > 0) this.OnChangeState(PlayerState.Jump_Up);
                else this.OnChangeState(PlayerState.Falling);
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
                brick?.OnHitBrick(_canBreakBrick);
            }    
        }
        else if (collision.CompareTag("Item"))
        {
            _canBreakBrick = true;
            collision.transform.parent.gameObject.SetActive(false);
            SoundManager.instance.PlaySFX(SoundManager.instance.itemCollect);
        }
        else if (collision.CompareTag("Enemy"))
        {
            // chỉ khi đang rơi xuống
            if (_myRigid2D.linearVelocityY < 0 &&
                this.transform.position.y > collision.transform.position.y)
            {
                MushroomController enemy = collision.GetComponent<MushroomController>();
                enemy?.OnDead();
                SoundManager.instance.PlaySFX(SoundManager.instance.enemyDie);

                _myRigid2D.linearVelocity = Vector2.zero;
                _myRigid2D.AddForce(Vector2.up * _force * 0.75f, ForceMode2D.Impulse);
            }
            else
            {
                this.OnDead();
            }
        }
        else if (collision.CompareTag("Coin"))
        {
            collision.GetComponent<Animator>().SetBool("earn", true);
            GameManager.instance.AddCoin(10);
            SoundManager.instance.PlaySFX(SoundManager.instance.coinCollect);
        }
        else if (collision.CompareTag("Plant"))
        {
            this.OnDead();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Brick" || collision.tag == "Cong")
        {
            if (collision.transform.position.y < this.transform.position.y - 0.1f)
                _onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Brick"  || collision.tag == "Cong")
        {
            _onGround = false;
        }
    }

    private bool _isDead;

    public void OnDead()
    {
        if (_isDead) return;
        _isDead = true;
        SoundManager.instance.PlaySFX(SoundManager.instance.die);

        this.OnChangeState(PlayerState.Dead);

        _myRigid2D.linearVelocity = Vector2.zero;
        _myRigid2D.bodyType = RigidbodyType2D.Dynamic;
        _myRigid2D.gravityScale = 4f;
        _myRigid2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
    }
}
