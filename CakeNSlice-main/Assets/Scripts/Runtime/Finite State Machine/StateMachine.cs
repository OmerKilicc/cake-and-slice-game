using System;
using System.Collections.Generic;

namespace Euphrates
{
    public class StateMachine
    {
        IState _currentSate;

        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        List<Transition> _currentTransitions = new List<Transition>(0);

        List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> _emptyTransitions = new List<Transition>(0);

        public void Tick(float deltaTime)
        {
            if (TryGetTransition(out var transition))
                SetCurrentState(transition.To);

            _currentSate?.Tick(deltaTime);
        }

        public void SetCurrentState(IState state)
        {
            if (_currentSate == state)
                return;

            _currentSate?.OnExit();
            _currentSate = state;

            if (!_transitions.TryGetValue(_currentSate.GetType(), out _currentTransitions))
                _currentTransitions = _emptyTransitions;

            _currentSate.OnEnter();
        }

        bool TryGetTransition(out Transition transition)
        {
            transition = new Transition();

            foreach (var at in _anyTransitions)
                if (at.Condition())
                {
                    transition = at;
                    return true;
                }

            foreach (var t in _currentTransitions)
                if (t.Condition())
                {
                    transition = t;
                    return true;
                }

            return false;
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            Transition newTransition = new Transition(to, condition);

            if (!_transitions.TryGetValue(from.GetType(), out var transitions))
            {
                var tList = new List<Transition>();
                
                tList.Add(newTransition);

                _transitions.Add(from.GetType(), tList);
                return;
            }

            transitions.Add(newTransition);
        }

        public void AddAnyTransition(IState to, Func<bool> condition) => _anyTransitions.Add(new Transition(to, condition));
    }
}
