using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector3 _minForce;
    [SerializeField] private Vector3 _maxForce;

    private Rigidbody _rigidbody;
    private Material _material;

    public float EdgeLength => transform.localScale.x;
    public float HalfEdgeLength => transform.localScale.x / 2;

    private void OnValidate()
    {
        Vector3 temp = _maxForce;

        if (_maxForce.x < _minForce.x)
            temp.x = _minForce.x;
        if (_maxForce.y < _minForce.y)
            temp.y = _minForce.y;
        if (_maxForce.z < _minForce.z)
            temp.z = _minForce.z;

        _maxForce = temp;
    }

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Fall()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(GetRandomForce(), ForceMode.Impulse);
    }

    public void SetColor(Color color) =>
        _material.color = color;

    private Vector3 GetRandomForce() =>
        new Vector3(Random.Range(_minForce.x, _maxForce.x), 
                    Random.Range(_minForce.y, _maxForce.y), 
                    Random.Range(_minForce.z, _maxForce.z));
}
