using JG.FG.ScriptableObjects;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace JG.FG.UI
{
    public class GoldDisplay : MonoBehaviour
    {
        private const string GoldTextFormat = "Gold: {0}";

        [BoxGroup("Data")]
        [SerializeField] private TextMeshProUGUI goldText;

        [BoxGroup("Data")]
        [SerializeField] private IntSO gold;

        private void Awake()
        {
            UpdateGoldText();

            gold.OnValueChanged += UpdateGoldText;
        }

        private void OnDestroy()
        {
            gold.OnValueChanged -= UpdateGoldText;
        }

        private void UpdateGoldText()
        {
            goldText.text = string.Format(GoldTextFormat, gold.Value);
        }
    }
}
