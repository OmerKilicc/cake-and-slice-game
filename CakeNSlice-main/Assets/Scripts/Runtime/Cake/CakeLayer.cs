using UnityEngine;

public class CakeLayer : MonoBehaviour
{

    [SerializeField] protected CakeLayerSO _data;
    public CakeLayerSO Data => _data;

    public virtual void Picked(Cake cake, int index) { }

    public virtual void FixPosition(Cake cake, int index, Transform parent) { }
}
