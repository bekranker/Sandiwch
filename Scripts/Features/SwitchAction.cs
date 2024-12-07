using System;
using System.Collections.Generic;
using UnityEngine;
//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class SwitchAction : MonoBehaviour
{
  [SerializeField] private PlugArea _plugArea;
  [SerializeReference] private List<ActionExecute> _actionToExecute;

  void OnEnable()
  {
    _plugArea.OnPlug += Execute;
    _plugArea.OnUnPlug += Reverse;
  }
  void OnDisable()
  {
    _plugArea.OnPlug -= Execute;
    _plugArea.OnUnPlug -= Reverse;
  }

  public void Execute()
  {
    _actionToExecute.ForEach((item) =>
    {
      if (item != null)
      {
        item.Execute();
      }
    });
  }
  public void Reverse()
  {
    _actionToExecute.ForEach((item) =>
    {
      if (item != null)
      {
        item.ComeBack();
      }
    });
  }
}