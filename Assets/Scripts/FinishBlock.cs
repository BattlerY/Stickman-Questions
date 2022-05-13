using UnityEngine;
using TMPro;

[RequireComponent(typeof(MeshRenderer))]
public class FinishBlock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ParticleSystem[] _particles;

    [HideInInspector] [SerializeField] private Color _color;
    [HideInInspector] [SerializeField] private int _blockNumber;

    public float Length => transform.localScale.z;
    public int BlockNumber => _blockNumber;

    private void Start()=>
        GetComponent<MeshRenderer>().material.color = _color;

    public void Initiate(Color color, float width, float length, int number)
    {
        transform.localScale = new Vector3(width, transform.localScale.y, length);
        _color = color;
        _text.text = $"x{number}";
        _blockNumber = number;
    }

    public void Initiate(Material material, float width, float length)
    {
        transform.localScale = new Vector3(width, transform.localScale.y, length);
        GetComponent<MeshRenderer>().material = material;
        _color = Color.white;
        _text.text = "";
    }

    public void LaunchEffects()
    {
        foreach (var item in _particles)
            item.gameObject.SetActive(true);
    }
}
