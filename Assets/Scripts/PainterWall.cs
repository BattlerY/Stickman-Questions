using UnityEngine;

public class PainterWall : Wall
{
    [SerializeField] private float _changeTime;
    [SerializeField] private Color[] _palette;

    private Color _color;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Block>(out Block block))
            block.SetColor(_color);
    }

    private void Start() =>
        InvokeRepeating("SetNewColor", 0, _changeTime);
   
    private void SetNewColor()
    {
        Color newColor;

        do
        {
            newColor = _palette[Random.Range(0, _palette.Length)];
        }
        while (newColor == _color && _palette.Length > 1);

        _color = newColor;
    }
}
