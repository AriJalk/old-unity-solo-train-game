using GameEngine.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeGame.Commands
{
	internal class ProduceGoodsCubeInSlotCommand : ICommand
	{
		private readonly CommandRequestEvents _commandRequestEvents;
		private readonly Guid _goodsCubeSlotGuid;
		private readonly GoodsColor _goodsColor;

		public ProduceGoodsCubeInSlotCommand(CommandRequestEvents commandRequestEvents, Guid goodsCubeSlotGuid, GoodsColor goodsColor)
		{
			_commandRequestEvents = commandRequestEvents;
			_goodsCubeSlotGuid = goodsCubeSlotGuid;
			_goodsColor = goodsColor;
		}
		public void Execute()
		{
			_commandRequestEvents.RaiseProduceGoodsCubeInSlotRequestEvents(_goodsCubeSlotGuid, _goodsColor);
		}

		public void Undo()
		{
			_commandRequestEvents.RaiseRemoveGoodsCubeFromSlotRequestEvents(_goodsCubeSlotGuid);
		}
	}
}
