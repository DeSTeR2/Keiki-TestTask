using System;

namespace Infrastructure.Events
{
    public class EventHolder
    {
        public Action Event;
        
        public void Invoke() 
            => Event?.Invoke();
    }
}