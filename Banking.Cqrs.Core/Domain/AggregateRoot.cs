

using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Domain
{
    public abstract class AggregateRoot
    {
        public string Id { get; set; } = string.Empty;

        private int version = -1;
        
        public List<BaseEvent> changes = new List<BaseEvent>();

        public int GetVersion()
        {
            return version;
        }

        public void SetVersion(int newVersion)
        {
            this.version = newVersion;
        }

        public List<BaseEvent> GetUnCommitedChanges()
        {
            return changes;
        }

        public void MarkChangesAsCommited()
        {
            changes.Clear();
        }

        public void ApplyChanges(BaseEvent @event, bool isNewEvent)
        {
            try
            {
                var classEvent = @event.GetType();
                var method = GetType().GetMethod("Apply", new[] { classEvent });
                method.Invoke(this, new object[] { @event });
            }
            catch (Exception)
            {
            }
            finally
            {
                if (isNewEvent)
                {
                    changes.Add(@event);
                }
            }
        }

        public void RaiseEvent(BaseEvent @event)
        {
            ApplyChanges(@event, true);
        }

        public void ReplayEvents(IEnumerable<BaseEvent> events)
        {
            foreach (var ev in events)
            {
                ApplyChanges(ev, false);
            }
        }

    }
}
