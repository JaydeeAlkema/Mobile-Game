using JG.FG.Interfaces;
using JG.FG.ScriptableObjects;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JG.FG.Plots
{
    public class PlotBuyer : MonoBehaviour, IInteractable
    {
        private const string BUY_COST_TEXT = "Buy: {0}{1}{2}G";

        [BoxGroup("References")]
        [SerializeField] private List<Plot> plotsToBuy = new();
        [BoxGroup("References")]
        [SerializeField] private TextMeshPro buyCostText = default;

        [BoxGroup("Settings")]
        [SerializeField] private Color enoughGoldColor = default;
        [BoxGroup("Settings")]
        [SerializeField] private Color notEnoughGoldColor = default;

        [BoxGroup("Data")]
        [SerializeField] private IntSO playerGoldAmount = default;
        [BoxGroup("Data")]
        [SerializeField] private IntSO baseBuyCost = default;
        [BoxGroup("Data")]
        [SerializeField] private IntSO boughtPlotsMultiplier = default;

        private void OnEnable()
        {
            UpdateCostText();

            playerGoldAmount.OnValueChanged += UpdateCostText;
            boughtPlotsMultiplier.OnValueChanged += UpdateCostText;
        }

        private void OnDisable()
        {
            playerGoldAmount.OnValueChanged -= UpdateCostText;
            boughtPlotsMultiplier.OnValueChanged -= UpdateCostText;
        }

        private void OnDestroy()
        {
            playerGoldAmount.OnValueChanged -= UpdateCostText;
            boughtPlotsMultiplier.OnValueChanged -= UpdateCostText;
        }

        public void Interact()
        {
            OnInteract();
        }

        private void OnInteract()
        {
            if (HasEnoughGold() is false)
            {
                // Implement some user feedback to display they need more gold to buy these plots...
                if (Debug.isDebugBuild)
                    Debug.Log($"#Plot Buyer# Not enough gold to buy these plots {string.Join(',', plotsToBuy)}", this);

                return;
            }

            plotsToBuy.ForEach(plot => plot.gameObject.SetActive(true));

            int finalCost = CalculateFinalCost();
            playerGoldAmount.Subtract(finalCost);
            boughtPlotsMultiplier.Add(1);

            Destroy(gameObject);
        }

        private void UpdateCostText()
        {
            Color color = HasEnoughGold() ? enoughGoldColor : notEnoughGoldColor;
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            string colorTag = $"<color=#{colorHex}>";
            string closeColorTag = "</color>";

            int finalCost = CalculateFinalCost();

            buyCostText.text = string.Format(BUY_COST_TEXT, colorTag, finalCost, closeColorTag);
        }

        private bool HasEnoughGold()
        {
            int finalCost = CalculateFinalCost();

            return playerGoldAmount.Value >= finalCost;
        }

        private int CalculateFinalCost()
        {
            int finalValue = baseBuyCost.Value * boughtPlotsMultiplier.Value;

            return finalValue;
        }
    }
}
