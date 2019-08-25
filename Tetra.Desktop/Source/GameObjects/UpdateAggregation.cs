namespace Tetra.Desktop
{
    public class UpdateAggregation : IHandleUpdates
    {
        private readonly IHandleUpdates[] updates;

        public UpdateAggregation(params IHandleUpdates[] updates)
        {
            this.updates = updates;
        }

        public void Update()
        {
            foreach (var handler in updates)
            {
                handler.Update();
            }
        }
    }
}
