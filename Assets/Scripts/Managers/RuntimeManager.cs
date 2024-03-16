using MobileGame.Plots;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame.Managers
{
	public class RuntimeManager : MonoBehaviour
	{
		private static RuntimeManager instance;
		public static RuntimeManager Instance
		{
			get
			{
				if (instance == null)
					instance = FindFirstObjectByType<RuntimeManager>();

				return instance;
			}
		}

		[BoxGroup("Game Settings"), SerializeField] private int targetFrameRate = 60;

		[BoxGroup("Runtime"), SerializeField] private HashSet<Plot> plots = new();

		#region Getters and Setters
		public HashSet<Plot> Plots => plots;
		#endregion

		private void Awake()
		{
			SetInstance();

			Application.targetFrameRate = targetFrameRate;
		}

		#region Plots
		public void AddPlot(Plot plot)
		{
			plots.Add(plot);
		}
		public void RemovePlot(Plot plot)
		{
			plots.Remove(plot);
		}
		#endregion

		private void SetInstance()
		{
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
		}
	}
}
