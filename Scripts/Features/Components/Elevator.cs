using UnityEngine;
using DG.Tweening;

using System;
//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;



//Dont fooled from the name. It can use i.e gate or door or something moving from a point to another point (i.e A to B);


public class Elevator : ActionExecute
{
  [SerializeField] private Transform _From, _To;
  [SerializeField, Range(0, 10)] private float _Speed;
  [SerializeField] private bool _loop;
  [SerializeField] private SpriteRenderer _Sp;
  void Start()
  {
    DOTween.Init();
  }
  public override void Execute()
  {
    if (_loop)
    {
      MoveLoop();
    }
    else
      MoveOnce();
  }
  public void MoveOnce()
  {
    transform.DOMove(_To.position, _Speed).SetEase(Ease.Linear);
  }
  public override Tween ComeBack()
  {
    DOTween.Kill(transform);
    return transform.DOMove(_From.position, _Speed).SetEase(Ease.Linear);
  }

  public void MoveLoop()
  {
    DOTween.Kill(transform);
    transform.DOMove(_To.position, _Speed).SetEase(Ease.Linear).OnComplete(() =>
    {
      DOVirtual.DelayedCall(_Speed, () => print("CallbackFromDelay")).OnComplete(() =>
      {
        ComeBack().OnComplete(() =>
        {
          DOVirtual.DelayedCall(_Speed, () => MoveLoop());
        });
      });
    });
  }

}
