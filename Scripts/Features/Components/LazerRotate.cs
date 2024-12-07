using UnityEngine;
using DG.Tweening;
public class LazerRotate : MonoBehaviour
{
    [SerializeField] private float _TargetRotate;
    [SerializeField] private Transform _LaserT;
    [SerializeField] private float _Speed;
    [SerializeField] private Lazer _Lazer;

    private Vector3 _startRot;
    void Start()
    {
        _startRot = _LaserT.rotation.eulerAngles;
        RotateExecute();
    }
    void OnEnable()
    {
        _Lazer.OnOpen += RotateExecute;
    }
    void OnDisable()
    {
        _Lazer.OnOpen -= RotateExecute;
    }
    void RotateExecute()
    {
        _LaserT.DORotate(_TargetRotate * Vector3.forward, _Speed, RotateMode.Fast).OnComplete(() =>
        {
            DOVirtual.DelayedCall(_Speed, () =>
            {
                _LaserT.DORotate(_startRot, _Speed).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(_Speed, () =>
                    {
                        RotateExecute();
                    });
                }).SetEase(Ease.Linear);
            });
        }).SetEase(Ease.Linear);
    }

}