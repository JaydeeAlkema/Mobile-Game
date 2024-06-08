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
        [SerializeField] private CropState cropState = default;

        [BoxGroup("Data")]
        [SerializeField] private CropSO crop = default;
        [BoxGroup("Data")]
        [SerializeField] private IntSO gold = default;

        [BoxGroup("References")]
        [SerializeField] private Collider2D collider = default;


        [BoxGroup("--- TEMP ---")]
        [SerializeField] private TextMeshPro tempText = default;

        private float growthTimer;

        public CropState CropState { get => cropState; set => cropState = value; }

        private void OnEnable()
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
            cropState = CropState.Growing;
            growthTimer = 0;

            collider.enabled = true;
            tempText.gameObject.SetActive(true);
            UpdateTempText();
        }

        private void Grow()
        {
            if (crop == null)
                return;

            if (cropState is not CropState.Growing)
                return;

            growthTimer += Time.deltaTime;
            if (growthTimer >= crop.GrowthTime)
            {
                cropState = CropState.ReadyToHarvest;
                growthTimer = 0;
            }
        }

        private void Harvest()
        {
            if (crop == null)
                return;

            if (cropState is not CropState.ReadyToHarvest)
                return;

            cropState = CropState.Growing;

            gold.Add(crop.GoldWorth);
        }

        private void UpdateTempText()
        {
            string colorFormaterStart = cropState switch
            {
                CropState.Growing => "<color=yellow>",
                CropState.ReadyToHarvest => "<color=green>",
                _ => "<color=red>"
            };
            string colorFormaterEnd = "</color>";

            string stateFormated = cropState switch
            {
                CropState.Growing => "Growing",
                CropState.ReadyToHarvest => "Ready",
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
