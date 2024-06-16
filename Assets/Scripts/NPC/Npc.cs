using JG.FG.StateMachines;
using NaughtyAttributes;
using UnityEngine;

namespace JG.FG.Npc
{
    public class Npc : MonoBehaviour
    {
        private const string NAME = "NPC";

        [BoxGroup("References")]
        [SerializeField, Expandable] private NpcConfig config = default;

        private StateMachine stateMachine;

        private void Awake()
        {
            stateMachine = new StateMachine(new IdleState());

            stateMachine.OnStateStart += StateMachine_OnStateStart;
            stateMachine.OnStateChange += StateMachine_OnStateChange;

            stateMachine.Initialize();
        }

        private void OnDestroy()
        {
            stateMachine.OnStateStart -= StateMachine_OnStateStart;
            stateMachine.OnStateChange -= StateMachine_OnStateChange;
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void StateMachine_OnStateChange(BaseState oldState, BaseState newState)
        {
            Debug.Log($"[{NAME}] - State changed from {oldState.GetType().Name} to {newState.GetType().Name}");
        }

        private void StateMachine_OnStateStart(BaseState state)
        {
            Debug.Log($"{NAME} - State started: {state.GetType().Name}");
        }
    }
}
