using UnityEngine;


//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;




/*

The Coyote Time is in here;
*/
public class Grounded : MonoBehaviour
{
  [SerializeField, Range(0, 1)] private float _CoyoteTime;
  [SerializeField] private Transform _raycastPoint;
  [SerializeField] private LayerMask _LayerGround;
  [SerializeField] private float _Distance;
  [SerializeField] private PlayerMovement _Player;


  private float _coyoteCounter;
  private RaycastHit2D _currentHit;

  void Update()
  {
    _currentHit = Physics2D.Raycast(_raycastPoint.position, -_raycastPoint.up, _Distance, _LayerGround);

    if (_currentHit.collider)
    {
      _coyoteCounter = _CoyoteTime;
    }
    else
    {
      _coyoteCounter -= _coyoteCounter > 0 ? Time.deltaTime : 0;
    }
  }

  public GameObject GetGround()
  {
    return _currentHit.collider.gameObject;
  }
  public bool IsGrounded()
  {
    return _coyoteCounter > 0;
  }
  /*
    * You can use this part for adding particle effects or etc. right at the jump phase
  */

  void OnEnable()
  {
    _Player.OnJump += () => { };
  }
  void OnDisable()
  {
    _Player.OnJump -= () => { };
  }
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.transform.CompareTag("Ground"))
      _Player.CanJump = true;
  }
  void OnDrawGizmos()
  {
    Gizmos.color = Color.black;
    if (_currentHit.collider != null)
    {
      Gizmos.DrawWireCube(_raycastPoint.position, _Distance * Vector3.one);
    }

  }
}