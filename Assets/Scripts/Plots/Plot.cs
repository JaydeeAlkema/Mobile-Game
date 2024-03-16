using MobileGame.Crops;
using MobileGame.Enums;
using MobileGame.Interfaces;
using MobileGame.Managers;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace MobileGame.Plots
{
	public class Plot : MonoBehaviour, IInteractable
	{
		[BoxGroup("Runtime"), SerializeField] private Crop crop = default;
		[BoxGroup("Runtime"), SerializeField] private PlotState state = default;

		[Foldout("Debugging"), SerializeField] private bool isDebugging = false;

		[Foldout("Testing"), SerializeField] private Crop testCrop;

		#region Getters and Setters
		public PlotState State => state;
		public Crop Crop => crop;
		#endregion

		private void Start()
		{
			RuntimeManager.AddPlot(this);
		}

		public void PlantCrop(Crop crop)
		{
			if (this.crop != null)
				return;

			this.crop = Instantiate(crop, transform); ;
			state = PlotState.Planted;
		}

		public void InteractWithCrop()
		{
			if (crop == null)
				return;

			Interact();

			switch (crop.CropState)
			{
				case CropState.Seed:
					if (isDebugging) Debug.Log("Crop is still a seed");
					state = PlotState.Planted;
					break;

				case CropState.Growing:
					if (isDebugging) Debug.Log("Crop is still growing");
					break;

				case CropState.Grown:
					if (isDebugging) Debug.Log("Crop is grown");
					state = PlotState.Empty;
					crop = null;
					break;

				default:
					throw new NotImplementedException($"{crop.CropState} is not supported");
			}
		}

		public void Interact()
		{
			if (crop == null)
				return;

			crop.Interact();
		}

		public Vector2 GetPosition()
		{
			return transform.position;
		}

		[Button]
		private void Test_PlantCop()
		{
			PlantCrop(testCrop);
		}

		[Button]
		private void Test_HarvestCrop()
		{
			InteractWithCrop();
		}

		private RuntimeManager RuntimeManager => RuntimeManager.Instance;
	}
}
