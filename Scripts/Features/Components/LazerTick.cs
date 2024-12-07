using DG.Tweening;
using UnityEngine;

public class LazerTick : MonoBehaviour
{
	[SerializeField] private GameObject _sparks;
	[SerializeField] private GameObject _RubbicSparks;
	[SerializeField] private LineRenderer _LineRenderer;
	[SerializeField] private Lazer _Lazer;

	[SerializeField, Range(0, 10)] private float _Delay;
	private bool _closed;
	void Start()
	{
		Tick();
	}
	void OnEnable()
	{
		_Lazer.OnOpen += Tick;
	}
	void OnDisable()
	{
		_Lazer.OnOpen -= Tick;
	}
	private void Tick()
	{
		DOVirtual.DelayedCall(_Delay, () =>
		{
			_closed = !_closed;
			_LineRenderer.enabled = !_closed;
			_RubbicSparks.SetActive(!_closed);
			_sparks.SetActive(!_closed);
			Tick();
		});

	}
}