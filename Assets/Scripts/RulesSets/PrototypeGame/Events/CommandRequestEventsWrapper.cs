using PrototypeGame.Events.CommandRequestEvents;

namespace PrototypeGame.Events
{
	internal class CommandRequestEventsWrapper
	{
		public readonly MapCommandRequestEvents MapCommandRequestEvents = new MapCommandRequestEvents();
		public readonly CardCommandRequestEvents CardCommandRequestEvents = new CardCommandRequestEvents();
		public readonly StateCommandRequestEvents StateCommandRequestEvents = new StateCommandRequestEvents();
	}
}
