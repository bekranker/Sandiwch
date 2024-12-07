using UnityEngine;
using DG.Tweening;
//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;

public class Red : MonoBehaviour, IBlock
{
    [SerializeField] private SpriteRenderer _Sp;
    private Vector3 _startPos;

    private Vector2 _startVelocity;
    void Start()
    {
        _startPos = transform.localPosition;
    }

    void OnTriggerStay2D(Collider2D cols)
    {
        if (cols.CompareTag("Player"))
        {
            Rigidbody2D rb = cols.GetComponent<Rigidbody2D>();
            _startVelocity = rb.linearVelocity;
            rb.linearVelocity = Vector2.zero;
            rb.AddForceX(_startVelocity.x * 50, ForceMode2D.Impulse);
            rb.AddForceY(10, ForceMode2D.Impulse);

            _Sp.transform.DOLocalMoveY(-2, 0.3f);
        }
    }
    void OnTriggerExit2D(Collider2D cols)
    {
        DOTween.Kill(_Sp);
        if (cols.CompareTag("Player"))
        {
            _Sp.transform.DOLocalMoveY(0, .3f);
        }
    }
}

