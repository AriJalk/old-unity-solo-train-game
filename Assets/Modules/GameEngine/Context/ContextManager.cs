using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Context
{
	public class ContextManager
	{
		private Stack<IContext> _contextStack = new Stack<IContext>();

		public void NextContext(IContext context)
		{
			context.StartContext();
			_contextStack.Push(context);
		}

		public void PreviousContext()
		{
			IContext headContext = _contextStack.Pop();
			headContext.StopContext();
			_contextStack.Peek().StartContext();
		}
	}
}
