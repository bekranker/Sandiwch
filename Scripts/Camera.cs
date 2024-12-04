using UnityEngine;

//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;
public class Camera : MonoBehaviour
{
  [SerializeField, Range(0, 10)] private float _Speed;
  [SerializeField] private Transform _Target;
  private Vector3 refVector = Vector3.one;
  [SerializeField] private Vector3 _Offset;

  private void LateUpdate()
  {
    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_Target.position.x, _Target.position.y, transform.position.z) + _Offset, ref refVector, _Speed * Time.deltaTime);
  }


}