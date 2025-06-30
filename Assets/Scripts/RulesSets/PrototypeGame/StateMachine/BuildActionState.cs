using CommonEngine.Core;
using GameEngine.StateMachine;
using PrototypeGame.Scene;
using PrototypeGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PrototypeGame.StateMachine
{
	internal class BuildActionState : IStateMachine
	{
		private int _availableMoney;
		private CommonServices _commonServices;
		private UserInterface _userInterface;

		public BuildActionState(int availableMoney, CommonServices commonServices, UserInterface userInterface)
		{
			_availableMoney = availableMoney;
			_commonServices = commonServices;
			_userInterface = userInterface;
			
		}

		public void EnterState()
		{
			_userInterface.CurrentMessage.text = $"Build Action, {_availableMoney}$ remaining\nSelect tile to build on";
			_commonServices.RaycastConfig.SetRaycastLayer(typeof(HexTileObject));
			_commonServices.CommonEngineEvents.ColliderSelectedEvent += OnColliderSelected;
		}

		public void ExitState()
		{
			_commonServices.CommonEngineEvents.ColliderSelectedEvent -= OnColliderSelected;
			_userInterface.CurrentMessage.text = "";
		}

		private void OnColliderSelected(RaycastHit hit)
		{
			if (hit.collider.GetComponent<HexTileObject>() is HexTileObject tile)
			{
				Debug.Log(tile.HexCoord);
			}
		}
	}
}
