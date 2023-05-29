
namespace Banking.Cqrs.Core.Events
{
    public class AccountCloseEvent : BaseEvent
    {
        public AccountCloseEvent(string id) : base(id)
        {
        }
    }
}
