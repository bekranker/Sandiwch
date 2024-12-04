using System;
using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class PlugArea : MonoBehaviour
{
  public event Action OnPlug, OnUnPlug;

  public void ConnectToMe(Environment env)
  {
    OnPlug?.Invoke();
    print(env + " is connected to me successfully");
  }
  //Guess what does this comment do o_o; 
  public void BreakConnect(Environment env)
  {
    OnUnPlug?.Invoke();
    print(env + " is break connection from me successfully");
  }
}