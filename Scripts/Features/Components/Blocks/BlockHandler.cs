using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _bxCol;
    [SerializeField] private ColorType _colorType = ColorType.Red;
    [SerializeField] private Blue _blue;
    [SerializeField] private Red _red;
    private Vector2 _startBoxSize;

    void Start()
    {
        _startBoxSize = _bxCol.size;
        SelectColorType(_colorType);
    }
    private void SelectColorType(ColorType select)
    {
        if (select == ColorType.Red)
        {
            _bxCol.isTrigger = true;
            _red.enabled = true;
            _blue.enabled = false;
            _bxCol.size = new Vector2(_bxCol.size.x, _bxCol.size.y + 2);
            _bxCol.offset = new Vector2(0, _bxCol.offset.y + 1);
        }
        else
        {
            _bxCol.isTrigger = false;
            _blue.enabled = true;
            _red.enabled = false;
            _bxCol.size = _startBoxSize;
        }
        _colorType = select;
    }
    public void SwitchTo(ColorType switchTo)
    {
        if (switchTo == ColorType.Red)
        {
            _bxCol.isTrigger = true;
            _blue.enabled = false;
            _red.enabled = true;
            _bxCol.size = new Vector2(_bxCol.size.x, _bxCol.size.y + 2);
            _bxCol.offset = new Vector2(0, _bxCol.offset.y + 1);
        }
        else
        {
            _bxCol.isTrigger = false;
            _red.enabled = false;
            _blue.enabled = true;
            _bxCol.size = _startBoxSize;
            _bxCol.offset = new Vector2(0, 0);
        }
        _colorType = switchTo;

    }
}
