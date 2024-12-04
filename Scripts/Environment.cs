using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class Environment : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _Rb;
	[SerializeField] private BoxCollider2D _BoxCollider;
	//@param _playerParent variable stands for SetParent() function. It will set it-self's parent an objet that a child in player parent. That child will change size X value on every turn (i.e Input.GetAxisRaw("Horizontal") is giving the dirextion of that object);
	private Transform _playerParent;
	public bool Plugged;
	private PlugArea _connectedArea;
	private PlayerMovement _playerComponent;
	void Start()
	{
		_playerComponent = FindAnyObjectByType<PlayerMovement>();
		_playerParent = _playerComponent._child;
	}
	public void TakeMe(Transform playerT)
	{
		Take(playerT);
		if (Plugged)
			UnPlug(_connectedArea);
		Plugged = false;
	}
	public void PutMe(PlugArea plugArea)
	{
		if (plugArea != null)
		{
			Plug(plugArea);
			return;
		}
		//execute bellow if there is no plug side;
		Drop();
	}
	private void Take(Transform playerT)
	{
		if (playerT.position.x < transform.position.x)
			transform.position += Vector3.right;
		else
			transform.position += Vector3.left;

		transform.SetParent(_playerParent);
		_Rb.bodyType = RigidbodyType2D.Kinematic;
		_Rb.linearVelocity = Vector2.zero;
		_BoxCollider.isTrigger = true;

		//Renk değişimi
	}
	private void Drop()
	{
		transform.SetParent(null);
		_Rb.bodyType = RigidbodyType2D.Dynamic;
		_BoxCollider.isTrigger = false;
	}
	private void UnPlug(PlugArea plugArea)
	{
		plugArea.BreakConnect(this);
	}
	private void Plug(PlugArea plugArea)
	{
		_Rb.bodyType = RigidbodyType2D.Kinematic;
		_BoxCollider.isTrigger = true;
		Plugged = true;
		_connectedArea = plugArea;
		_Rb.linearVelocity = Vector2.zero;
		DOTween.Kill(transform);
		transform.DOMove(plugArea.transform.position, .3f);
		plugArea.ConnectToMe(this);
	}
}