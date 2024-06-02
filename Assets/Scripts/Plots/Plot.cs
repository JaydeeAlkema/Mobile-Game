using JustGames.FarmingGame.Crops;
using JustGames.FarmingGame.Interfaces;
using NaughtyAttributes;
using System.Text;
using TMPro;
using UnityEngine;

namespace JustGames.FarmingGame.Plots
{
    public class Plot : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlotState state;

        [Space]
        [SerializeField] private CropSO crop;

        [Space]
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