using System.Collections.Generic;

namespace StM
{
    public class StateMachine
    {
        private List<State> _states;
        private State _current;
        public State Current => _current;

        private Dictionary<State, List<Transition>> _transitionRules;

        public StateMachine()
        {
            _states = new List<State>();
            _transitionRules = new Dictionary<State, List<Transition>>();
        }

        public void Init(State initial)
        {
            _current = initial;
        }

        public void AddState(State state)
        {
            _states.Add(state);
            _transitionRules.Add(state, new List<Transition>());
        }

        public void AddTransition(State first, State next, Condition condition)
        {
            Transition t = new DefaultTransition(next, condition);
            if (_transitionRules.ContainsKey(first)) {
                _transitionRules[first].Add(t);
            }
            else
            {
                throw new StateNotContainedException($"The State {first} is not part of this State Machine");
            }
        }

        public void AddCustomTransition(State state, Transition t)
        {
            if (_states.Contains(state))
            {
                _transitionRules[state].Add(t);
            }
            else
                throw new StateNotContainedException($"The State {state} is not part of this State Machine");
        }

        public void Step()
        {
            _current.Execute();
            CheckForTransition();
        }

        private void CheckForTransition()
        {
            List<Transition> transitions = _transitionRules[_current];
            foreach(Transition t in transitions)
            {
                if (t.DoesApply())
                {
                    makeTransition(t);
                    break;
                }
            }
        }

        private void makeTransition(Transition transition)
        {
            _current.Reset();
            _current = transition.Next;
            _current.Init();
        }
    }
}
