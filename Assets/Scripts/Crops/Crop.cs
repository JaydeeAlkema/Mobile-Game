using MobileGame.Enums;
using MobileGame.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace MobileGame.Crops
{
	public class Crop : MonoBehaviour, IInteractable
	{
		[BoxGroup("Base"), SerializeField] private float growthTimeTillNextState = default;
		[BoxGroup("Base"), SerializeField] private float growthRate = default;
		[Space]
		[BoxGroup("Base"), SerializeField, ProgressBar("growthTimer", "growthTimeTillNextState", EColor.Green)] private float growthTimer = 0;
		[Space]
		[BoxGroup("Base"), SerializeField] private CropState cropState = default;
		[BoxGroup("Base"), SerializeField] private bool isWatered = default;
		[Space]
		[BoxGroup("Base"), SerializeField, Required] private Sprite seedSprite = default;
		[BoxGroup("Base"), SerializeField, Required] private Sprite growingSprite = default;
		[BoxGroup("Base"), SerializeField, Required] private Sprite grownSprite = default;
		[Space]
		[BoxGroup("Base"), SerializeField, Required] private SpriteRenderer spriteRenderer = default;
		[BoxGroup("Base"), SerializeField, Required] private Animator animator = default;

		#region Getters and Setters
		private void SetState(CropState newState) => cropState = newState;
		private void SetNextState()
		{
			switch (cropState)
			{
				case CropState.Seed:
					SetState(CropState.Growing);
					SetSprite(growingSprite);
					break;

				case CropState.Growing:
					SetState(CropState.Grown);
					SetSprite(grownSprite);
					break;

				case CropState.Grown:
					break;

				default:
					throw new System.NotImplementedException($"{cropState} is not supported");
			}
		}
		private void SetSprite(Sprite newSprite) => spriteRenderer.sprite = newSprite;
		#endregion

		private void Start()
		{
			SetSprite(seedSprite);
		}

		private void Update()
		{
			if (cropState == CropState.Grown)
				return;

			Grow();
		}

		private void Grow()
		{
			if (!isWatered)
				return;

			growthTimer += Time.deltaTime * growthRate;
			if (growthTimer >= growthTimeTillNextState)
			{
				SetNextState();
				animator.SetTrigger("Idle");
				growthTimer = 0;
				isWatered = false;
			}
		}

		[Button]
		public void Water()
		{
			if (isWatered)
				return;

			if (cropState == CropState.Grown)
				return;

			animator.SetTrigger("Interact");
			isWatered = true;
		}

		public void Harvest()
		{
			Destroy(gameObject);
		}

		[Button]
		public void Interact()
		{
			switch (cropState)
			{
				case CropState.Seed:
				case CropState.Growing:
					Water();
					break;

				case CropState.Grown:
					Harvest();
					break;

				default:
					throw new System.NotImplementedException($"{cropState} is not supported");
			}
		}
	}
}
