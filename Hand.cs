using System;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _ColliderArea;
    [SerializeField] private LayerMask _PlugAreaLayer;
    [SerializeField] private LayerMask _KeyLayer;

    private Environment _holdingObject;
    public static event Action OnLeave, OnPlug, OnUnPlug;
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
        print("Taking");
        _holdingObject = NearestEnvironment();
        if (_holdingObject != null)
        {
            _holdingObject.TakeMe(transform);
        }
    }
    void LeaveObject()
    {
        if (_holdingObject == null) return;
        print("Putting");
        _holdingObject.PutMe(NearestPlugArea());
        _holdingObject = null;
    }
    void ExecuteInput()
    {
        print("Executing");
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
