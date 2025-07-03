using PrototypeGame.Events.CommandRequestEvents;
using System.Collections.Generic;
using TurnBasedHexEngine.Commands;

namespace PrototypeGame.Commands
{
	internal class ProduceGoodsInAllFactorySlotsCommand : ICommand
	{
		private MapCommandRequestEvents _mapCommandRequestEvents;
		private IEnumerable<Factory> _emptyFactories;

		public ProduceGoodsInAllFactorySlotsCommand(MapCommandRequestEvents mapCommandRequestEvents, IEnumerable<Factory> emptyFactories)
		{
			_mapCommandRequestEvents = mapCommandRequestEvents;
			_emptyFactories = emptyFactories;
		}
		public void Execute()
		{
			foreach (Factory factory in _emptyFactories)
			{
				_mapCommandRequestEvents.RaiseProduceGoodsCubeInSlotRequestEvent(factory.GoodsCubeSlot.guid, factory.ProductionColor);
			}
		}

		public void Undo()
		{
			foreach (Factory factory in _emptyFactories)
			{
				_mapCommandRequestEvents.RaiseRemoveGoodsCubeFromSlotRequestEvent(factory.GoodsCubeSlot.guid);
			}
		}
	}
}
