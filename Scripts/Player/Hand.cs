using System;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class Hand : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _ColliderArea;
    [SerializeField] private LayerMask _PlugAreaLayer;
    [SerializeField] private LayerMask _KeyLayer;
    [SerializeField] private Animator _anim;
    private Environment _holdingObject;
    public static event Action OnLeave, OnPlug, OnUnPlug, OnTake;
    Collider2D[] _plugs;
    Collider2D[] _environments;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteInput();
        }
        _plugs = Physics2D.OverlapBoxAll(transform.position, Vector2.one * _ColliderArea, 0, _PlugAreaLayer);
        _environments = Physics2D.OverlapBoxAll(transform.position, Vector2.one * _ColliderArea, 0, _KeyLayer);
    }

    Environment NearestEnvironment()
    {
        if (_environments.Length == 0) return null;

        Environment ClosestEnvironment = _environments[0].GetComponent<Environment>();
        foreach (Collider2D env in _environments)
        {
            if (Vector2.Distance(transform.position, env.transform.position) < Vector2.Distance(transform.position, ClosestEnvironment.transform.position))
            {
                ClosestEnvironment = env.GetComponent<Environment>();
            }
        }
        return ClosestEnvironment;
    }
    PlugArea NearestPlugArea()
    {
        if (_plugs.Length == 0) return null;
        PlugArea ClosePlugArea = _plugs[0].gameObject.GetComponent<PlugArea>();
        foreach (Collider2D plugArea in _plugs)
        {
            if (Vector2.Distance(transform.position, plugArea.gameObject.transform.position) < Vector2.Distance(transform.position, ClosePlugArea.transform.position))
            {
                ClosePlugArea = plugArea.GetComponent<PlugArea>();
            }
        }
        return ClosePlugArea;
    }
    void TakeObject()
    {
        _holdingObject = NearestEnvironment();
        if (_holdingObject != null)
        {
            _holdingObject.TakeMe(transform);
            OnTake?.Invoke();
        }
    }
    void LeaveObject()
    {
        if (_holdingObject == null) return;
        _holdingObject.transform.SetParent(null);
        _holdingObject.PutMe(NearestPlugArea());
        _holdingObject = null;
    }
    void ExecuteInput()
    {
        if (_holdingObject != null)
            LeaveObject();
        else
            TakeObject();
    }

    public bool IsHolding()
    {
        return _holdingObject;
    }
}
