using JG.FG.ScriptableObjects;
using NaughtyAttributes;
using UnityEngine;

namespace JG.FG.Npc
{
    [CreateAssetMenu(fileName = "NpcConfig", menuName = "ScriptableObjects/NpcConfig")]
    public class NpcConfig : ScriptableObject
    {
        [BoxGroup("Stats")]
        [SerializeField] private FloatSO moveSpeed = default;
        [BoxGroup("Stats")]
        [SerializeField] private FloatSO interactSpeed = default;
        [BoxGroup("Stats")]
        [SerializeField] private FloatSO interactCooldown = default;

        public float MoveSpeed => moveSpeed.Value;
        public float InteractSpeed => interactSpeed.Value;
        public float InteractCooldown => interactCooldown.Value;
    }
}
