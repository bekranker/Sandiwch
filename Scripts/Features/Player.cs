using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Color _RedColor, _BlueColor;
    [SerializeField] private PlayerType _PlayerType = PlayerType.Red;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private float _Speed;
    void Start()
    {
        Changecolor();
    }
    public void Changecolor()
    {
        DOTween.Kill(_SpriteRenderer);
        if (_PlayerType == PlayerType.Red)
        {
            _SpriteRenderer.DOColor(_BlueColor, _Speed);
            _PlayerType = PlayerType.Blue;
        }
        else
        {
            _SpriteRenderer.DOColor(_RedColor, _Speed);
            _SpriteRenderer.color = _RedColor;
            _PlayerType = PlayerType.Red;
        }
    }
}


public enum PlayerType
{
    Red, Blue
}
