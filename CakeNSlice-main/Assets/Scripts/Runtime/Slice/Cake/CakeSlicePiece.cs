using UnityEngine;

public class CakeSlicePiece : MonoBehaviour, ICoverable
{
    [SerializeField] MeshRenderer _cover;

    public void Cover(Material coverMaterial)
    {
        if (_cover == null)
            return;

        _cover.gameObject.SetActive(true);
        _cover.material = coverMaterial;
    }
}
