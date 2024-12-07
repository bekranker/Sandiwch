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
    print("Executing the action");
    foreach (ActionExecute item in _actionToExecute)
    {
      item.Execute();
      print("execture");
    }
  }
  public void Reverse()
  {
    print("Executing the ComeBack action");
    foreach (ActionExecute item in _actionToExecute)
    {
      item.ComeBack();
    }

  }
}