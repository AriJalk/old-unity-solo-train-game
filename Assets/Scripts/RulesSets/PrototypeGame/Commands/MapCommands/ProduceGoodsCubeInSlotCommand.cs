using PrototypeGame.Events.CommandRequestEvents;
using System;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Commands
{
	internal class ProduceGoodsCubeInSlotCommand : ICommand
	{
		private readonly MapCommandRequestEvents _mapCommandRequestEvents;
		private readonly Guid _goodsCubeSlotGuid;
		private readonly GoodsColor _goodsColor;

		public ProduceGoodsCubeInSlotCommand(MapCommandRequestEvents mapCommandRequestEvents, Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_goodsCubeSlotGuid = goodsCubeSlotGuid;
			_goodsColor = goodsColor;
		}
		public void Execute()
		{
			_mapCommandRequestEvents.RaiseProduceGoodsCubeInSlotRequestEvent(_goodsCubeSlotGuid, _goodsColor);
		}

		public void Undo()
		{
			_mapCommandRequestEvents.RaiseRemoveGoodsCubeFromSlotRequestEvent(_goodsCubeSlotGuid);
		}
	}
}
