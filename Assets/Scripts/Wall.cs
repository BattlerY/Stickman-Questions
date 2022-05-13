using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Wall : MonoBehaviour
{
    [HideInInspector] [SerializeField] private float _cathetus;

    public float Ñathetus => _cathetus;

    public void SetSize(float hypotenuse)
    {
         _cathetus = Mathf.Sqrt(hypotenuse * hypotenuse / 2);
         BoxCollider collider = GetComponent<BoxCollider>();
         collider.size = new Vector3(_cathetus, collider.size.y, _cathetus);
    }
}
