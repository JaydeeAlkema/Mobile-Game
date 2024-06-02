using UnityEngine;

namespace JustGames.FarmingGame.Crops
{
    [CreateAssetMenu(fileName = "New Crop", menuName = "ScriptableObjects/Crop")]
    public class CropSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float GrowthTime { get; private set; }
        [field: SerializeField] public int GoldWorth { get; private set; }
    }
}
