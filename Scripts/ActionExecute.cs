using UnityEngine;
using DG.Tweening;
public abstract class ActionExecute : MonoBehaviour
{
  public abstract Tween ComeBack();
  public abstract void Execute();
}