using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Color _RedColor, _BlueColor;
    [SerializeField] private Color _PanelColorRed, _PanelColorBlue;
    [SerializeField] Color _MaterialRed, _MaterialBlue;

    [SerializeField] private Image _Panel;
    [SerializeField] private ColorType _PlayerType = ColorType.Red;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private float _Speed;
    [SerializeField] private Material _shader;
    private List<BlockHandler> _blockHandlers;


    void Start()
    {
        _blockHandlers = FindObjectsByType<BlockHandler>(FindObjectsSortMode.None).ToList();
        if (_PlayerType == ColorType.Red)
        {
            _shader.SetColor("_Color", _MaterialRed);
            _Panel.DOColor(_PanelColorRed, _Speed);
            _SpriteRenderer.DOColor(_RedColor, _Speed);
        }
        else
        {
            _shader.SetColor("_Color", _MaterialBlue);
            _Panel.DOColor(_PanelColorBlue, _Speed);
            _SpriteRenderer.DOColor(_BlueColor, _Speed);
        }
    }

    //Calling this function when we take keys
    public void Changecolor()
    {


        if (_PlayerType == ColorType.Red)
        {
            _blockHandlers?.ForEach((item) =>
            {
                item.SwitchTo(ColorType.Red);
            });
            _SpriteRenderer.DOColor(_BlueColor, _Speed);
            _Panel.DOColor(_PanelColorBlue, _Speed);
            _shader.DOColor(_MaterialBlue, "_Color", _Speed);
            _PlayerType = ColorType.Blue;
        }
        else
        {
            _blockHandlers?.ForEach((item) =>
            {
                item.SwitchTo(ColorType.Blue);
            });
            _SpriteRenderer.DOColor(_RedColor, _Speed);
            _Panel.DOColor(_PanelColorRed, _Speed);
            _SpriteRenderer.color = _RedColor;
            _shader.DOColor(_MaterialRed, "_Color", _Speed);
            _PlayerType = ColorType.Red;
        }
    }

    void OnEnable() => Hand.OnTake += Changecolor;
    void OnDisable() => Hand.OnTake -= Changecolor;

}


public enum ColorType
{
    Red, Blue
}
