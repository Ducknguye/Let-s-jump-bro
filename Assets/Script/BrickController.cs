using UnityEngine;

public enum BrickType { Normal_Brick, Secret_Box, Lock_Box}

public class BrickController : MonoBehaviour
{
    [SerializeField] private BrickType _myType;
    [SerializeField] private Sprite _sprNormal;
    [SerializeField] private Sprite _sprSecret;
    [SerializeField] private Sprite _sprLock;
    [SerializeField] private SpriteRenderer _mySpriteRender;

    private Animator _myAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch(_myType)
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

        _myAnim = this.GetComponent<Animator>();
    }

    public void OnHitBrick()
    {
        switch (_myType)
        {
            case BrickType.Normal_Brick:
                _myAnim.SetTrigger("Hit");
                break;
            case BrickType.Secret_Box:

                break;
            case BrickType.Lock_Box:

                break;
        }
    }
}
