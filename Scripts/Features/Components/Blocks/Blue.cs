using UnityEngine;
using DG.Tweening;
//--------------------------------------------------------;
//--------------------------------------------------------;
//----------------------SORRY FOR NESTY CODE--------------;
//--------------------------------------------------------;
//--------------------------------------------------------;

public class Blue : MonoBehaviour, IBlock
{

    [Header("Custom Gravity")]
    [SerializeField, Range(0, 100)] private float _InfluenceRange;
    [SerializeField, Range(0, 100)] private float _Intensity;
    private float _distanceToPlayer;
    private Vector2 _pullForce;


    [SerializeField] private SpriteRenderer _Sp;
    [SerializeField] private Vector2 _Size;
    [SerializeField] private LayerMask _PlayerLayer;
    [SerializeField] private BoxCollider2D _BoxCollider2D;

    private Vector3 _startPos;
    private Vector2 _startVelocity;
    private float _rotateTo = 0;


    private Rigidbody2D _playerRB;
    private Collider2D _previousCollider;

    void Start()
    {
        _startPos = transform.localPosition;
    }




    /*
        /------------------------\
      /--------------------\     |
      | *                  |  o  |
      | *                  |  u  |
      | *                  |  t  |
      | *   inside box     |     |
      | *                  |  b  |
      | *                  |  o  |
      | *                  |  x  |
      \--------------------/     |
        \------------------------/
    
        This is a box inside a box. The conditions of Left, Right, Bottom and above working like this.
        if inside box (i.e player) touching out box (i.e this class (i.e opposite color boxes)) it means we can walk and rotate etc. on the out box's surfaces 

    */




    void Update()
    {
        Collider2D playerCol = Physics2D.OverlapBox(transform.position, _Size, 0, _PlayerLayer);


        if (playerCol != null)
        {
            _previousCollider = playerCol;
            _distanceToPlayer = Vector2.Distance(playerCol.transform.position, transform.position);

            _playerRB = playerCol.GetComponent<Rigidbody2D>();


            //Left
            if (playerCol.transform.position.x < transform.position.x &&
                playerCol.transform.position.y <= transform.position.y + _BoxCollider2D.size.y / 2 &&
                playerCol.transform.position.y >= transform.position.y - _BoxCollider2D.size.y / 2)
            {

                _rotateTo = 90;
                playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);

                //apply gravity in X direction (i.e gravity direction is Right)
                if (_distanceToPlayer <= _InfluenceRange)
                {
                    _pullForce = ((transform.position.x - playerCol.transform.position.x) / _distanceToPlayer * _Intensity) * Vector2.right;
                    _playerRB.AddForce(_pullForce, ForceMode2D.Force);
                }

                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------
                //-----------------This part of code taking player from the corner to the flat surface on blue--------
                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------

                #region Player Glitch from Left to Above or Bottom
                if (playerCol.transform.position.y >= (transform.position.y + _BoxCollider2D.size.y / 2) - .03f)
                {
                    _rotateTo = 0;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    _playerRB.linearVelocity = Vector2.zero;
                    playerCol.transform.position = new Vector3(transform.position.x - (_BoxCollider2D.size.x / 2) + .5f, playerCol.transform.position.y + 1, 0);
                    return;

                }
                else if (playerCol.transform.position.y <= (transform.position.y - _BoxCollider2D.size.y / 2) - .03f)
                {
                    _rotateTo = -180;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    _playerRB.linearVelocity = Vector2.zero;
                    playerCol.transform.position = new Vector3(transform.position.x - (_BoxCollider2D.size.x / 2) + .5f, playerCol.transform.position.y - .4f, 0);
                    return;
                }
                #endregion
            }
            //Right
            else if (playerCol.transform.position.x > transform.position.x &&
                    playerCol.transform.position.y <= transform.position.y + _BoxCollider2D.size.y / 2 &&
                    playerCol.transform.position.y >= transform.position.y - _BoxCollider2D.size.y / 2)
            {

                _rotateTo = -90;
                playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);

                //apply gravity in X direction (i.e gravity direction is Left)
                if (_distanceToPlayer <= _InfluenceRange)
                {
                    _pullForce = ((transform.position.x - playerCol.transform.position.x) / _distanceToPlayer * _Intensity) * Vector2.right;
                    _playerRB.AddForce(_pullForce, ForceMode2D.Force);
                }

                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------
                //-----------------This part of code taking player from the corner to the flat surface on blue--------
                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------

                #region Player Glitch from Right to Above or Bottom
                if (playerCol.transform.position.y >= (transform.position.y + _BoxCollider2D.size.y / 2) - .03f)
                {
                    _rotateTo = 0;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    playerCol.transform.position = new Vector3(transform.position.x + (_BoxCollider2D.size.x / 2) - .5f, playerCol.transform.position.y + 1, 0);
                    return;

                }
                else if (playerCol.transform.position.y <= (transform.position.y - _BoxCollider2D.size.y / 2) - .03f)
                {
                    _rotateTo = -180;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    _playerRB.linearVelocity = Vector2.zero;
                    playerCol.transform.position = new Vector3(transform.position.x + (_BoxCollider2D.size.x / 2) - .5f, playerCol.transform.position.y - .4f, 0);
                    return;

                }
                #endregion
            }
            //Top
            else if (
                playerCol.transform.position.y > transform.position.y &&
                playerCol.transform.position.x < transform.position.x + _BoxCollider2D.size.x / 2 &&
                playerCol.transform.position.x > transform.position.x - _BoxCollider2D.size.x / 2)
            {
                _rotateTo = 0;
                playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);

                //apply gravity in Y direction (i.e gravity direction is Top)
                if (_distanceToPlayer <= _InfluenceRange)
                {
                    _pullForce = ((transform.position.y - playerCol.transform.position.y) / _distanceToPlayer * _Intensity) * Vector2.up;
                    _playerRB.AddForce(_pullForce, ForceMode2D.Force);
                }

                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------
                //-----------------This part of code taking player from the corner to the flat surface on blue--------
                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------

                #region Player Glitch from Top to Left or Right

                //to Right
                if (playerCol.transform.position.x >= (transform.position.x + _BoxCollider2D.size.x / 2) - .03f)
                {
                    _playerRB.linearVelocity = Vector2.zero;
                    _rotateTo = -90;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    playerCol.transform.position = new Vector3(transform.position.x + (_BoxCollider2D.size.x / 2) + .3f, playerCol.transform.position.y - .5f, 0);
                    return;

                }
                //To Left
                else if (playerCol.transform.position.x <= (transform.position.x - _BoxCollider2D.size.x / 2) + .03f)
                {
                    _rotateTo = 90;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    _playerRB.linearVelocity = Vector2.zero;
                    playerCol.transform.position = new Vector3(transform.position.x - (_BoxCollider2D.size.x / 2) - .3f, playerCol.transform.position.y - .5f, 0);
                    return;

                }
                #endregion

                //gravity must be normal when we are above on opposite block
            }
            //Bottom
            else if (
                playerCol.transform.position.y < transform.position.y &&
                playerCol.transform.position.x < transform.position.x + _BoxCollider2D.size.x / 2 &&
                playerCol.transform.position.x > transform.position.x - _BoxCollider2D.size.x / 2)
            {
                _rotateTo = -180;
                playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                //apply gravity in Y direction (i.e gravity direction is Down)
                if (_distanceToPlayer <= _InfluenceRange)
                {
                    _pullForce = ((transform.position.y - playerCol.transform.position.y) / _distanceToPlayer * _Intensity) * Vector2.up;
                    _playerRB.AddForce(_pullForce, ForceMode2D.Force);
                }

                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------
                //-----------------This part of code taking player from the corner to the flat surface on blue--------
                //----------------------------------------------------------------------------------------------------
                //----------------------------------------------------------------------------------------------------

                #region Player Glitch from Bottom to Right or Left
                if (playerCol.transform.position.x >= (transform.position.x + _BoxCollider2D.size.x / 2) - .03f)
                {
                    playerCol.transform.position = new Vector3(transform.position.x + _BoxCollider2D.size.x / 2 + 1f, transform.position.y + _BoxCollider2D.size.y / 2 + .05f, 0);
                    _playerRB.linearVelocity = Vector2.zero;
                    _rotateTo = -90;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    return;
                }
                else if (playerCol.transform.position.x <= (transform.position.x - _BoxCollider2D.size.x / 2) - .03f)
                {
                    playerCol.transform.position = new Vector3(transform.position.x - _BoxCollider2D.size.x / 2 - 1f, transform.position.y + _BoxCollider2D.size.y / 2 + .05f, 0);
                    _playerRB.linearVelocity = Vector2.zero;
                    _rotateTo = 90;
                    playerCol.transform.rotation = Quaternion.Euler(0, 0, _rotateTo);
                    return;
                }
                #endregion
            }
            return;
        }

        //if player out of area, adjust the player's rotation to normal (i.e normal is zero)
        if (!_previousCollider) return;
        _previousCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    void OnCollisionEnter2D(Collision2D cols)
    {
        if (!cols.transform.CompareTag("Player")) return;
        _playerRB.gravityScale = 0;
        _playerRB.linearVelocity = Vector2.zero;
    }

    void OnCollisionExit2D(Collision2D cols)
    {
        if (!cols.transform.CompareTag("Player")) return;
        cols.transform.rotation = Quaternion.Euler(0, 0, 0);
        _playerRB.gravityScale = 1;
        _playerRB.linearVelocityY = 0;
    }

    const float _plusOffsetBlue = 2;
    const float _plusOffsetPlayer = 2.5f;
}