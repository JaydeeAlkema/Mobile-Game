using JG.FG.Crops;
using JG.FG.Interfaces;
using JG.FG.ScriptableObjects;
using NaughtyAttributes;
using System.Text;
using TMPro;
using UnityEngine;

namespace JG.FG.Plots
{
    public class Plot : MonoBehaviour, IInteractable
    {
        [BoxGroup("Runtime")]
        [SerializeField] private PlotState state;

        [BoxGroup("Reference")]
        [SerializeField] private CropSO crop;
        [BoxGroup("Reference")]
        [SerializeField] private IntSO gold;

        [BoxGroup("--- TEMP ---")]
        [SerializeField] private TextMeshPro tempText;

        private float growthTimer;

        public PlotState State { get => state; set => state = value; }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Grow();
            UpdateTempText();
        }

        [Button]
        public void Interact()
        {
            Harvest();
        }

        private void Initialize()
        {
            state = PlotState.Growing;
            growthTimer = 0;

            UpdateTempText();
        }

        private void Grow()
        {
            if (crop == null)
                return;

            if (state is not PlotState.Growing)
                return;

            growthTimer += Time.deltaTime;
            if (growthTimer >= crop.GrowthTime)
            {
                state = PlotState.ReadyToHarvest;
                growthTimer = 0;
            }
        }

        private void Harvest()
        {
            if (crop == null)
                return;

            if (state is not PlotState.ReadyToHarvest)
                return;

            state = PlotState.Growing;

            gold.Add(crop.GoldWorth);
        }

        private void UpdateTempText()
        {
            string colorFormaterStart = state switch
            {
                PlotState.Growing => "<color=yellow>",
                PlotState.ReadyToHarvest => "<color=green>",
                _ => "<color=red>"
            };
            string colorFormaterEnd = "</color>";

            string stateFormated = state switch
            {
                PlotState.Growing => "Growing",
                PlotState.ReadyToHarvest => "Ready",
                _ => "None"
            };

            StringBuilder sb = new();
            sb.AppendLine($"{colorFormaterStart}{stateFormated}{colorFormaterEnd}");
            sb.AppendLine($"{crop.Name ?? "none"}");
            sb.AppendLine($"{growthTimer:F1}/{crop.GrowthTime:F0}");

            tempText.text = sb.ToString();
        }

    }
}
