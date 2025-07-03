using HexSystem;
using PrototypeGame.Events.CommandRequestEvents;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Commands
{
	internal class BuildFactoryCommand : ICommand
	{
		protected readonly MapCommandRequestEvents _mapCommandRequestEvents;
		protected readonly HexCoord _hexCoord;
		protected readonly GoodsColor _goodsColor;

		public BuildFactoryCommand(MapCommandRequestEvents mapCommandRequestEvents, HexCoord hexCoord, GoodsColor productionColor)
		{
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_hexCoord = hexCoord;
			_goodsColor = productionColor;
		}
		public virtual void Execute()
		{
			_mapCommandRequestEvents.RaiseBuildFactoryRequestEvent(_hexCoord, _goodsColor);
		}

		public virtual void Undo()
		{
			_mapCommandRequestEvents.RaiseRemoveFactoryRequestEvent(_hexCoord);
		}
	}
}
