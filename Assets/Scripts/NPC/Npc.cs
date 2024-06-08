using JG.FG.StateMachines;
using NaughtyAttributes;
using UnityEngine;

namespace JG.FG.Npc
{
    public class Npc : MonoBehaviour
    {
        [BoxGroup("References")]
        [SerializeField, Expandable] private NpcConfig config = default;

        private StateMachine stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine(new IdleState());
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}
