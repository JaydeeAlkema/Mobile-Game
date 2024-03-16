using MobileGame.Crops;
using MobileGame.Enums;
using MobileGame.Interfaces;
using MobileGame.Managers;
using MobileGame.Plots;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame.NPC
{

	public class NPC : MonoBehaviour
	{
		[BoxGroup("Base"), SerializeField] private new string name = default;
		[BoxGroup("Base"), SerializeField] private float walkSpeed = default;
		[BoxGroup("Base"), SerializeField] private float tickRate = default;

		[BoxGroup("Runtime"), SerializeField] private NPCState state = default;
		[Space]
		[BoxGroup("Runtime"), SerializeField, ProgressBar("tickRatetimer", "tickRate", EColor.Green)] private float tickRatetimer = default;
		[Space]
		[BoxGroup("Runtime"), SerializeField] private IInteractable target = default;
		[Space]
		[BoxGroup("Runtime"), SerializeField] private List<Crop> inventory = new();

		[Foldout("Debugging"), SerializeField] private bool isDebugging = false;

		#region Getters and Setters
		private void SetState(NPCState state)
		{
			if (this.state == state)
				return;

			if (isDebugging) Debug.Log($"State changed from {this.state} to {state}");
			this.state = state;
		}
		#endregion

		private void Update()
		{
			if (tickRatetimer >= tickRate)
			{
				UpdateTick();
				tickRatetimer = 0;
			}
			else
				tickRatetimer += Time.deltaTime;

			MoveToTarget();
		}

		private void UpdateTick()
		{
			target ??= GetNearestTarget();
		}

		private void MoveToTarget()
		{
			if (target == null)
				return;

			Vector2 targetPostition = target.GetPosition();
			if (Vector2.Distance(transform.position, targetPostition) < 0.1f)
			{
				SetState(NPCState.Interacting);
				InteractWithTarget();
			}
			else
			{
				SetState(NPCState.Walking);
				transform.position = Vector2.MoveTowards(transform.position, targetPostition, walkSpeed * Time.deltaTime);
			}
		}

		private IInteractable GetNearestTarget()
		{
			if (isDebugging) Debug.Log($"{name} is looking for a target");
			HashSet<Plot> plots = RuntimeManager.Plots;
			HashSet<Plot> emptyPlots = new();
			HashSet<Plot> plantedPlots = new();

			// First, split up all plots into empty and planted
			foreach (Plot plot in plots)
			{
				switch (plot.State)
				{
					case PlotState.Empty:
						emptyPlots.Add(plot);
						break;

					case PlotState.Planted:
						plantedPlots.Add(plot);
						break;

					default:
						throw new System.NotImplementedException($"{plot.State} is not supported");
				}
			}

			float distance = Mathf.Infinity;
			float closestDistance = distance;
			Plot targetPlot = null;

			if (emptyPlots.Count > 0)
			{
				foreach (Plot plot in emptyPlots)
				{
					distance = Vector2.Distance(transform.position, plot.GetPosition());
					if (distance >= closestDistance)
						continue;

					closestDistance = distance;
					targetPlot = plot;
				}
			}
			else
			{
				foreach (Plot plot in plantedPlots)
				{
					distance = Vector2.Distance(transform.position, plot.GetPosition());
					if (distance >= closestDistance)
						continue;
					if (plot.Crop.IsWatered)
						continue;

					closestDistance = distance;
					targetPlot = plot;
				}
			}

			if (isDebugging) Debug.Log($"{name} found {targetPlot}");
			return targetPlot;
		}

		private void InteractWithTarget()
		{
			if (target == null)
			{
				SetState(NPCState.Idle);
				return;
			}

			target.Interact();
			Plot plot = target as Plot;

			if (plot == null)
				return;

			if (plot.State == PlotState.Empty)
			{
				Crop crop = inventory[0];
				inventory.Remove(crop);
				plot.PlantCrop(crop);
				if (isDebugging) Debug.Log($"{name} planted {crop.name}");
			}
			else
			{
				plot.InteractWithCrop();
				if (isDebugging) Debug.Log($"{name} interacted with {plot.name}");
			}

			target = null;
			SetState(NPCState.Idle);
		}

		private RuntimeManager RuntimeManager => RuntimeManager.Instance;
	}
}
