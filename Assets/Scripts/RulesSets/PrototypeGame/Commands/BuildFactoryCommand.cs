using GameEngine.Commands;
using HexSystem;
using PrototypeGame.Events;

namespace PrototypeGame.Commands
{
	internal class BuildFactoryCommand : ICommand
	{
		private readonly CommandRequestEvents _commandRequestEvents;
		private readonly HexCoord _hexCoord;
		private readonly GoodsColor _goodsColor;

		public BuildFactoryCommand(CommandRequestEvents commandRequestEvents, HexCoord hexCoord, GoodsColor productionColor)
		{
			_commandRequestEvents = commandRequestEvents;
			_hexCoord = hexCoord;
			_goodsColor = productionColor;
		}
		public void Execute()
		{
			_commandRequestEvents.RaiseBuildFactoryRequestEvent(_hexCoord, _goodsColor);
		}

		public void Undo()
		{
			_commandRequestEvents.RaiseRemoveFactoryRequestEvent(_hexCoord);
		}
	}
}
