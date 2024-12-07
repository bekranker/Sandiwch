using DG.Tweening;
using UnityEngine;

public class LazerTick : ActionExecute
{
	[SerializeField] private GameObject _sparks;
	[SerializeField] private GameObject _RubbicSparks;
	[SerializeField] private LineRenderer _LineRenderer;
	[SerializeField] private Transform _From;
	[SerializeField, Range(0, 10)] private float _Delay;
	private bool _closed;
	private bool _kill;
	void Start()
	{
		Tick();
	}

	public override Tween ComeBack()
	{
		return null;
	}
	private void Tick()
	{
		if (_kill) return;
		DOVirtual.DelayedCall(_Delay, () =>
		{
			_closed = !_closed;
			_LineRenderer.enabled = !_closed;
			_RubbicSparks.SetActive(!_closed);
			_sparks.SetActive(!_closed);
			Tick();
		});

	}
	public override void Execute()
	{
		DOTween.Kill(transform);
	}
}