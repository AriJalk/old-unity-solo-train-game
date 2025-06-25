using GameEngine.Commands;
using HexSystem;

namespace PrototypeGame.Commands
{
	internal class BuildFactoryCommand : ICommand
	{
		private readonly LogicStateEvents _logicStateEvents;
		private readonly HexCoord _hexCoord;
		private readonly GoodsColor _goodsColor;

		public BuildFactoryCommand(LogicStateEvents logicStateEvents, HexCoord hexCoord, GoodsColor productionColor)
		{
			_logicStateEvents = logicStateEvents;
			_hexCoord = hexCoord;
			_goodsColor = productionColor;
		}
		public void Execute()
		{
			_logicStateEvents.RaiseBuildFactoryRequestEvent(_hexCoord, _goodsColor);
		}

		public void Undo()
		{
			_logicStateEvents.RaiseRemoveFactoryRequestEvent(_hexCoord);
		}
	}
}
