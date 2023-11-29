using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Chungkang.GameNetwork.Manager
{
    public enum EventType
    {
        OnLogin,
        OnRegister,
        OnChatSend,
        OnChatReceive
    }

    public class EventManager
    {
        private static EventManager _instance;
        public static EventManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new EventManager();
                return _instance;
            }
        }

        private Dictionary<EventType, List<OnEvent>> _listeners;

        public delegate void OnEvent(EventType type, object sender, params object[] args);

        private EventManager()
        {
            _listeners = new Dictionary<EventType, List<OnEvent>>();
        }

        public void AddListener(EventType type, OnEvent onEvent)
        {
            if(!_listeners.ContainsKey(type))
                _listeners.Add(type, new List<OnEvent>());

            _listeners[type].Add(onEvent);
        }

        public void RemoveListener(EventType type, OnEvent onEvent) 
        {
            if (!_listeners.ContainsKey(type)) return;
            if (!_listeners[type].Contains(onEvent)) return;

            _listeners[type].Remove(onEvent);
        }

        public void PostNotification(EventType type, object sender, params object[] args)
        {
            foreach(var listener in _listeners[type])
            {
                listener.Invoke(type, sender, args);
            }
        }
    }
}
