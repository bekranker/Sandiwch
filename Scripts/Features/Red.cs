using UnityEngine;
using DG.Tweening;
//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;

public class Red : MonoBehaviour
{
    [SerializeField] private AreaEffector2D _Wind;
    [SerializeField] private LayerMask _Player;
    [SerializeField] private SpriteRenderer _Sp;
    [SerializeField] private float _Distance;
    private Vector3 _startPos;
    private bool _canMove;

    private Vector2 _startVelocity;
    void Start()
    {
        _startPos = transform.localPosition;
    }

    void OnTriggerStay2D(Collider2D cols)
    {
        if (cols.CompareTag("Player"))
        {
            _startVelocity = cols.GetComponent<Rigidbody2D>().linearVelocity;
            cols.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            cols.GetComponent<Rigidbody2D>().AddForceX(_startVelocity.x * 50, ForceMode2D.Impulse);
            cols.GetComponent<Rigidbody2D>().AddForceY(10, ForceMode2D.Impulse);

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

