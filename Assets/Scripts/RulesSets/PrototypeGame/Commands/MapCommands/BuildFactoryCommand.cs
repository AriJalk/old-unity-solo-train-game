using GameEngine.Commands;
using HexSystem;
using PrototypeGame.Events;
using PrototypeGame.Events.CommandRequestEvents;

namespace PrototypeGame.Commands
{
	internal class BuildFactoryCommand : ICommand
	{
		private readonly MapCommandRequestEvents _mapCommandRequestEvents;
		private readonly HexCoord _hexCoord;
		private readonly GoodsColor _goodsColor;

		public BuildFactoryCommand(MapCommandRequestEvents mapCommandRequestEvents, HexCoord hexCoord, GoodsColor productionColor)
		{
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_hexCoord = hexCoord;
			_goodsColor = productionColor;
		}
		public void Execute()
		{
			_mapCommandRequestEvents.RaiseBuildFactoryRequestEvent(_hexCoord, _goodsColor);
		}

		public void Undo()
		{
			_mapCommandRequestEvents.RaiseRemoveFactoryRequestEvent(_hexCoord);
		}
	}
}
