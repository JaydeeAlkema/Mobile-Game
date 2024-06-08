using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FloatSO", menuName = "ScriptableObjects/FloatSO")]
    public class FloatSO : NumericSO<float>
    {
        public override void Add(float value)
        {
            Value += value;

            base.Add(value);
        }

        public override void Subtract(float value)
        {
            Value -= value;

            base.Subtract(value);
        }
    }
}
