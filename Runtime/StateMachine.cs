using System.Collections.Generic;

namespace UnityStateMachine
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

        /// <summary>
        /// Initializes the state machine by setting the given state as active.
        /// </summary>
        /// <param name="initial">The initial state</param>
        public void Init(State initial)
        {
            _current = initial;
            _current.OnInit();
        }

        /// <summary>
        /// Adds a given value to the state machine's list of known states.
        /// Duplicates of states are not allowed and attempts to add a single State instance twice will throw an error.
        /// </summary>
        /// <param name="state"></param>
        /// /// <exception cref="DuplicateStateException">Thrown if <paramref name="state"/> is already part of the state machine</exception>
        public void AddState(State state)
        {
            if (_states.Contains(state))
                throw new DuplicateStateException($"The State {state} is already part of this state machine.");
            _states.Add(state);
            _transitionRules.Add(state, new List<Transition>());
        }

        /// <summary>
        /// Adds a new <see cref="DefaultTransition"/> built from the given parameters to the transitions list.
        /// /// Transition rules are expected to be deterministic. Multiple transitions with the same <paramref name="first"/> / <paramref name="condition"/> pairs will result in undefined behavior.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="next">Value of the Next property of the new Transition</param>
        /// <param name="condition">Value of the Condition property of the new Transition</param>
        /// <exception cref="StateNotContainedException">Thrown if either first or next are not part of the state machine</exception>
        public void AddTransition(State first, State next, Condition condition)
        {
            if (!_states.Contains(next))
            {
                throw new StateNotContainedException($"The State {next} is not part of this State Machine");
            }
            Transition t = new DefaultTransition(next, condition);
            if (_transitionRules.ContainsKey(first)) {
                _transitionRules[first].Add(t);
            }
            else
            {
                throw new StateNotContainedException($"The State {first} is not part of this State Machine");
            }
        }

        /// <summary>
        /// Adds a new Transition to the transitions list. Use this only if you have a custom <see cref="Transition"/> implementation.
        /// Prefer <see cref="AddTransition"/> if you want default behaviour for the transition.
        /// Transition rules are expected to be deterministic. Multiple transitions with the same 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="next">Value of the Next property of the new Transition</param>
        /// <param name="condition">Value of the Condition property of the new Transition</param>
        /// /// <exception cref="StateNotContainedException">Thrown if either <paramref name="state"/> or t.next are not part of the state machine.</exception>
        public void AddCustomTransition(State state, Transition t)
        {
            if (!_states.Contains(t.Next))
            {
                throw new StateNotContainedException($"The State {t.Next} is not part of this State Machine");
            }
            if (_states.Contains(state))
            {
                _transitionRules[state].Add(t);
            }
            else
                throw new StateNotContainedException($"The State {state} is not part of this State Machine");
        }

        /// <summary>
        /// Performs an update step on the state machine that represents the passing of a single time step.
        /// For the machine to 'run' continuously, Tick() must be called repeatedly, e.g. in a game objects's Update() method.
        /// During execution, the current state's OnTick() method will be executed. If the state's condition applies, the machine will change state accoring to its known transition rules.
        /// </summary>
        public void Tick()
        {
            _current.OnTick();
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
            _current.OnReset();
            _current = transition.Next;
            _current.OnInit();
        }
    }
}
