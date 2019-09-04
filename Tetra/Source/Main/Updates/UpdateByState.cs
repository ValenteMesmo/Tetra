using System;
using System.Collections.Generic;

namespace Tetra
{
    public class UpdateByState : IHandleUpdates
    {
        private readonly Dictionary<int, UpdateAggregation> Options = new Dictionary<int, UpdateAggregation>();
        private readonly IHaveState gameOjbect;

        public UpdateByState(IHaveState gameOjbect) =>
            this.gameOjbect = gameOjbect;

        public void Update()
        {
            var updates = Options[gameOjbect.State].updates;
            var initialState = gameOjbect.State;
            Game1.Log += $@"
State {initialState}";
            foreach (var update in updates)
            {
                update.Update();

                if (initialState != gameOjbect.State)
                    break;
            }
        }

        public void Add(int state, UpdateAggregation updateHandler)
        {
            if (Options.ContainsKey(state))
                throw new Exception($"{nameof(UpdateByState)} already have an update handler for state {state}");

            Options[state] = updateHandler;
        }
    }
}
