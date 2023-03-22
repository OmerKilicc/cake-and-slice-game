using UnityEngine;

[CreateAssetMenu(fileName = "New Cake Piece", menuName = "Cake/Piece")]
public class CakeLayerSO : ScriptableObject
{
    public float Id;
    public string Name;
    public float Height;
    public Sprite Icon;
    public int Value;

    public override bool Equals(object other)
    {
        CakeLayerSO layer = (CakeLayerSO)other;

        if (layer == null)
            return false;

        if (base.Equals(other))
            return true;

        return layer.Id == Id && layer.Name == Name && layer.Height == Height && layer.Icon == Icon && layer.Value == Value;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
