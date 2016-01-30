using System;
using System.Collections;
using System.Collections.Generic;

namespace PubSub {
	public enum Channel {
		Null = 0,
		EnemyCollide
	}
	
	public class Signal {
		public Channel type;
		public object sender;
		public Hashtable data;
		
		public Signal() {
			type = Channel.Null;
			sender = null;
			data = null;
		}
		
		public Signal(Channel _type, object _sender) {
			type = _type;
			sender = _sender;
			data = null;
		}
		
		public Signal(Channel _type, object _sender, Hashtable _data) {
			type = _type;
			sender = _sender;
			data = _data;
		}
	}
	
	public class PubSubBroker {
		public delegate void PubSubDelegate(Signal n);
		Dictionary<Channel, PubSubDelegate> _dict = new Dictionary<Channel, PubSubDelegate>();
		
		public void Subscribe(Channel type, PubSubDelegate d) {
			PubSubDelegate del = null;
			if (_dict.TryGetValue(type, out del)) {
				_dict[type] -= d;
				_dict[type] += d;
			}
			else {
				del += d;
				_dict.Add(type, del);
			}
		}
		
		public void Unsubscribe(Channel type, PubSubDelegate d) {
			if (_dict.ContainsKey(type)) {
				_dict[type] -= d;
			}
		}
		
		public void Publish(Channel type, object sender) {
			Publish(type, sender, null);
		}
		
		public void Publish(Channel type, object sender, Hashtable data) {
			Signal s = new Signal(type, sender, data);
			
			PubSubDelegate del = null;
			if (_dict.TryGetValue(type, out del))
                if (del != null)
                    del(s);
        }
    }
}


