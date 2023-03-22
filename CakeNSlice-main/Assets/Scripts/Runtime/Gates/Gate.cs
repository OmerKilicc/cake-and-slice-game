using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] CakeLayer _piecePrefab;
    [SerializeField] SpriteRenderer _image;

    private void Start()
    {
        if (_image == null && _piecePrefab == null)
            return;

        _image.sprite = _piecePrefab.Data.Icon;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.layer != 6 || !otherGO.TryGetComponent<Cake>(out var cake))
            return;

        CakeLayer spawned = Instantiate(_piecePrefab);
        spawned.transform.position = transform.position + Vector3.up * 2;
        cake.AddPiece(spawned);
    }
}
