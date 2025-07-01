using HexSystem;
using PrototypeGame.Logic;
using PrototypeGame.Logic.MetaData;
using PrototypeGame.Logic.State;
using PrototypeGame.Logic.State.Cards;
using System;

namespace PrototypeGame.RulesServices
{
	internal class RulesValidator
	{
		private readonly LogicCardState _logicCardState;
		private readonly LogicMapState _logicMapState;

		public RulesValidator(LogicCardState logicCardState, LogicMapState logicMapState)
		{
			_logicCardState = logicCardState;
			_logicMapState = logicMapState;
		}

		public bool IsValidTransportationSource(Guid slotId)
		{
			if (!_logicMapState.CubeSlotInfo.ContainsKey(slotId))
			{
				return false;
			}
			SlotInfo slotInfo = _logicMapState.CubeSlotInfo[slotId];

			return slotInfo.Slot.GoodsCube != null;
		}

		public bool IsValidTransportationDestination(Guid slotId) 
		{
			if (!_logicMapState.CubeSlotInfo.ContainsKey(slotId))
			{
				return false;
			}
			SlotInfo slotInfo = _logicMapState.CubeSlotInfo[slotId];

			return slotInfo.Slot.GoodsCube == null;
		}

		public bool IsValidTransportationAction(Guid sourceSlot, Guid destinationSlot)
		{
			return true;
		}

		public bool IsValidBuildLocation(HexCoord hexCoord, BuildingType buildingType)
		{
			HexTileData tile = _logicMapState.Tiles[hexCoord];

			switch (buildingType)
			{
				case BuildingType.STATION:
					return tile.Station == null;
				case BuildingType.FACTORY:
					return tile.Factory == null;
				default:
					return false;
			};
		}

		public bool CanCardBeDiscarded(Guid cardId)
		{
			if (_logicCardState.CardsInHand.ContainsKey(cardId))
			{
				return _logicCardState.CardsInHand[cardId].CanBeDiscarded;
			}
			return false;
		}
	}
}
