using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;




public class Lazer : ActionExecute
{
    [SerializeField] private GameObject _Sparks;
    [SerializeField] private LayerMask _Everything;
    [SerializeField] private LineRenderer _LineRenderer;
    [SerializeField] private Transform _From;

    public override Tween ComeBack()
    {
        _LineRenderer.gameObject.SetActive(true);
        _Sparks.gameObject.SetActive(true);
        return null;
    }

    public override void Execute()
    {
        _LineRenderer.gameObject.SetActive(false);
        _Sparks.gameObject.SetActive(false);
    }

    void Update()
    {
        _LineRenderer.SetPosition(0, new Vector2(0, 0));

        RaycastHit2D hit = Physics2D.Raycast(_From.position, -_LineRenderer.gameObject.transform.up, 100, _Everything);

        if (hit.collider != null)
        {
            float distance = ((Vector2)hit.point - (Vector2)_From.position).magnitude;
            _LineRenderer.SetPosition(1, new Vector2(0, -distance));
            _Sparks.transform.position = hit.point;
        }
    }
}