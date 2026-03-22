using UnityEngine;
using Spine.Unity;

public class MushroomController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeleton;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _mySpeed;
    [SerializeField] private LayerMask _layerCheck;

    private bool _isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead)
            this.transform.Translate(_skeleton.transform.right * _mySpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Collider2D colWall = Physics2D.OverlapCircle(_wallCheck.position, 0.1f, _layerCheck);
        Collider2D colGround = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _layerCheck);

        if (colWall || !colGround)
        {
            _skeleton.transform.right *= -1;
        }
    }

    public void OnDead()
    {
        _isDead = true;
        _skeleton.AnimationName = "die";
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.linearVelocity = Vector2.zero;
        rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        Collider2D[] cols = this.GetComponents<Collider2D>();
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].enabled = false;
        }
    }
}
