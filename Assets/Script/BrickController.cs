using UnityEngine;
using System.Collections;

public enum BrickType { Normal_Brick, Secret_Box, Lock_Box}

public class BrickController : MonoBehaviour
{
    [SerializeField] private BrickType _myType;
    [SerializeField] private Sprite _sprNormal;
    [SerializeField] private Sprite _sprSecret;
    [SerializeField] private Sprite _sprLock;
    [SerializeField] private SpriteRenderer _mySpriteRender;
    [SerializeField] private GameObject _objEarn;
    [SerializeField] private ParticleSystem _fxBreak;

    private Animator _myAnim;
    private GameObject _enemyAbove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.OnUpdateSpriteBox();

        _myAnim = this.GetComponent<Animator>();
    }

    private void OnValidate()
    {
        this.OnUpdateSpriteBox();
    }
    private void OnUpdateSpriteBox()
    {
        switch (_myType)
        {
            case BrickType.Normal_Brick:
                _mySpriteRender.sprite = _sprNormal;
                break;
            case BrickType.Secret_Box:
                _mySpriteRender.sprite = _sprSecret;
                break;
            case BrickType.Lock_Box:
                _mySpriteRender.sprite = _sprLock;
                break;
        }
    }

    public void OnHitBrick(bool canBreak)
    {
        switch (_myType)
        {
            case BrickType.Normal_Brick:
                if (!canBreak)
                {
                    _myAnim.SetTrigger("Hit");
                }
                else
                {
                    ParticleSystem fxBreak = Instantiate(_fxBreak, this.transform);
                    fxBreak.transform.localPosition = Vector3.zero;
                    _mySpriteRender.gameObject.SetActive(false);
                    StartCoroutine(IEDeactiveBox());

                    IEnumerator IEDeactiveBox()
                    {
                        yield return new WaitForSeconds(0.15f);
                        this.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                break;
            case BrickType.Secret_Box:
                _mySpriteRender.sprite = _sprLock;
                _myType = BrickType.Lock_Box;

                GameObject earnObj = Instantiate(_objEarn, this.transform);
                earnObj.transform.localPosition = Vector3.zero;
                break;
            case BrickType.Lock_Box:

                break;
        }

        if (_enemyAbove != null)
        {
            MushroomController enemy = _enemyAbove.GetComponent<MushroomController>();
            enemy?.OnDead();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyAbove = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _enemyAbove = null;
            }
    }
}
