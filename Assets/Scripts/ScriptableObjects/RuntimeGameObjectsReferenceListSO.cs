using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RuntimePlotsReferenceList", menuName = "ScriptableObjects/RuntimePlotsReferenceList")]
    public class RuntimeGameObjectsReferenceListSO : RuntimeReferenceListSO<GameObject>
    {
        public override void Add(GameObject value)
        {
            Values.Add(value);

            base.Add(value);
        }

        public override void Subtract(GameObject value)
        {
            Values.Remove(value);

            base.Subtract(value);
        }
    }
}
