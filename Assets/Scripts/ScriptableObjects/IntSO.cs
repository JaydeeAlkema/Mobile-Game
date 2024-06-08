using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IntSO", menuName = "ScriptableObjects/IntSO")]
    public class IntSO : NumericSO<int>
    {
        public override void Add(int value)
        {
            Value += value;

            base.Add(value);
        }

        public override void Subtract(int value)
        {
            Value -= value;

            base.Subtract(value);
        }
    }
}
