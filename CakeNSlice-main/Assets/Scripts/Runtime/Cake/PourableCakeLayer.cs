using UnityEngine;

public class PourableCakeLayer : CakeLayer
{
    [SerializeField] Material _coverMaterial;

    public override void Picked(Cake cake, int index)
    {
        for (int i = 0; i < index; i++)
        {
            if (!cake.TryGetPieceAtIndex(i, out var piece) || !piece.TryGetComponent<ICoverable>(out var coverable)) 
                return;

            coverable.Cover(_coverMaterial);
        }
    }
}
