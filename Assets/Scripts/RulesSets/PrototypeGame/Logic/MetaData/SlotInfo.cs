using CommonEngine.Interfaces;
using System;

namespace PrototypeGame.Logic.MetaData
{
	internal struct SlotInfo
	{
		public GoodsCubeSlot Slot;
		public HexTileData HexTileData;
		public Type Type;
		public bool CanPlace;
		public IIdentifiable ParentEntity;

		public static SlotInfo CreateSlotInfo(GoodsCubeSlot slot, HexTileData hexTileData, Type type, bool canPlace, IIdentifiable parentEntity)
		{
			return new SlotInfo
			{
				CanPlace = canPlace,
				HexTileData = hexTileData,
				Type = type,
				ParentEntity = parentEntity,
				Slot = slot,
			};
		}
	}
}
