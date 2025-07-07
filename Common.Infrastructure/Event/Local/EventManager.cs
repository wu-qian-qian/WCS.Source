using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Event
{
    internal class EventManager
    {
        private static readonly ConcurrentDictionary<string, Type[]> _handlers = new();
 
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public IEnumerable<Type> GetHandlerForEvent(string eventName) => _handlers[eventName];

        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public void AddSubscription(string eventName, Type[] handlers)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.TryAdd(eventName, handlers);
            }
        }
    }
}
