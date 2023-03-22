using System;

namespace Euphrates
{
    public struct Transition
	{
        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

		public IState To;
		public Func<bool> Condition;
	}
}
