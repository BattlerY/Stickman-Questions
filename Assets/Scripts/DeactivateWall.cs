using UnityEngine;

public class DeactivateWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Block>(out Block block))
            block.gameObject.SetActive(false);
        else if (other.TryGetComponent<Gate>(out Gate gate))
            gate.gameObject.SetActive(false);
    }
}
