using System;
using System.Collections;
using System.Timers;
using System.Threading;

namespace Emulator
{
    /// <summary>
    /// Triggers actions after a set period of time
    /// </summary>
    /// 
    /// <todo>
    /// Add more documentation
    /// </todo>
    public class Thought
    {
        private int triggerTime;
        private Thought next;

        public int TriggerTime { 
            get { return triggerTime; } 
            set { triggerTime = value; } 
        }
        
        public Thought Next { 
            get { return next; } 
        }

        public Thought(int time)
        {
            triggerTime = time;
        }

        public virtual void Trigger()
        {
        }

        public void Link(Thought to)
        {
            next = to.next;
            to.next = this;
        }

        public void Unlink(Thought from)
        {
            if(from != null) {
                from.next = next;
            }
            next = null;
        }

        public void AutoLink(ref Thought head)
        {
            if(head == null) {
                head = this;
                return;
            }

            if(TriggerTime <= head.TriggerTime) {
                this.next = head;
                head = this;
                return;
            }

            Thought pos = head;
            while(pos.next != null && TriggerTime > pos.next.TriggerTime) {
                pos = pos.next;
            }

            Link(pos);
        }

        public static Thought UnlinkJob(Thought head, int time)
        {
            if(head == null || head.triggerTime > time) {
                return head;
            }

            Thought pos = head;
            while(pos.next != null && pos.next.triggerTime <= time) {
                pos = pos.next;
            }

            Thought next = pos.next;
            pos.next = null;
            return next;
        }
    }


    public class DelegateThought : Thought
    {
        public delegate void ThoughtCallback(Thought thought, object[] data);
        public ThoughtCallback _callback;
        public object[] _data;

        public DelegateThought(int time, ThoughtCallback callback) : base(time)
        {
            _callback = callback;
            _data     = null;
        }
        
        public DelegateThought(int time, ThoughtCallback callback, object[] data) : base(time)
        {
            _callback = callback;
            _data     = data;
        }

        public override void Trigger()
        {
            if(_callback != null) {
                _callback(this,_data);
            }
        }
    }
}
