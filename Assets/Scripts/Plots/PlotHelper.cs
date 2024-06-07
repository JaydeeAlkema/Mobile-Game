using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace JG.FG.Plots
{
    public class PlotHelper : MonoBehaviour
    {
        [SerializeField] private List<Plot> plots;

        [Button]
        private void InteractWithAllPlots()
        {
            foreach (Plot plot in plots)
                plot.Interact();
        }

        [Button]
        private void InteractWithRandomPlot()
        {
            IReadOnlyList<Plot> interactablePlots;
            interactablePlots = plots.FindAll(plot => plot.State is PlotState.ReadyToHarvest);

            if (interactablePlots.Count == 0)
                return;

            int randomIndex = Random.Range(0, interactablePlots.Count);
            interactablePlots[randomIndex].Interact();
        }
    }
}
