
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;

namespace Emulator {
    public class Thinker
    {
        /// <summary>
        /// Thinking head :-) (It thinks!)
        /// </summary>
        private Thought _head;
        
        /// <summary>
        /// Thread Timer
        /// </summary>
        private System.Timers.Timer threadTimer = new System.Timers.Timer(100);
        
        /// <summary>
        /// Thread time
        /// </summary>
        private int _time = 0;
        
        /// <summary>
        /// Worker Thread
        /// </summary>
        private Thread workerThread;

        /// <summary>
        /// Initialize a new Thinker to think thoughts
        /// </summary>
        public Thinker()
        {
            threadTimer.Elapsed += new ElapsedEventHandler(TimerCallback);
            threadTimer.AutoReset = true;
        }
        
        /// <summary>
        /// Starts the Worker Thread
        /// </summary>
        public void Start()
        {
            threadTimer.Start();
            workerThread = new Thread(new ThreadStart(this.Work));
            workerThread.Start();
        }

        /// <summary>
        /// Stops the Worker Thread
        /// </summary>
        public void Stop()
        {
            threadTimer.Stop();
            workerThread.Abort();
        }

        /// <summary>
        /// Adds a thought to the head
        /// </summary>
        /// <param name="time"></param>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Thought Add(double time, DelegateThought.ThoughtCallback callback, params object[] data)
        {
            _time = (int)(time * 10 + _time);
            Thought thought = new DelegateThought((int)time, callback, data);
                    thought.AutoLink(ref _head);
            return thought;
        }

        /// <summary>
        /// Requeues the thought 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="thought"></param>
        public void Requeue(double time, Thought thought)
        {
            thought.TriggerTime = (int)(_time + time *10);
            thought.AutoLink(ref _head);
        }

        /// <summary>
        /// Requeues the thought with a delta timer
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="thought"></param>
        public void RequeueDelta(double delta, Thought thought)
        {
            thought.TriggerTime += (int)(delta*10);
            thought.AutoLink(ref _head);
        }

        /// <summary>
        /// Remove a thought from the head
        /// </summary>
        /// <param name="thought"></param>
        public void Remove(Thought thought)
        {
            if(_head == thought) {
                _head = _head.Next;
            } else {
                Thought pos = _head;
                while(pos != null && pos.Next != thought) {
                    pos = pos.Next;
                }

                if(pos != null && pos.Next == thought) {
                    thought.Unlink(pos);
                }
            }
        }

        /// <summary>
        /// Clear the head from thoughts
        /// </summary>
        public void Clear()
        {
            Thought pos = _head;
            _head = null;
            while(pos != null && pos.Next != null) {
                pos.Next.Unlink(pos);
                pos = pos.Next;
            }
        }

        /// <summary>
        /// Increases the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void TimerCallback(object sender, ElapsedEventArgs args)
        {
            ++_time;
        }

        /// <summary>
        /// Executes the thoughts work
        /// </summary>
        public void Work()
        {
            while(true)
            {
                int time = _time;

                Thought head;
                Thought pos;
                pos  =  head = _head;
                head = _head = Thought.UnlinkJob(_head, time);
                if(pos == head) {
                    Thread.Sleep(10);
                    continue;
                }

                while(pos != null)
                {
                    Thought next = pos.Next;
                    pos.Unlink(null);

                    try {
                        pos.Trigger();
                    } catch(Exception e) {
                        ServerConsole.WriteLine(System.Drawing.Color.Red,"Exception in timed callback: {0}", e.Message);
                        if(e.StackTrace != null) {
                            ServerConsole.WriteLine(System.Drawing.Color.Red,e.StackTrace);
                        }
                        ServerConsole.WriteLine("");
                    }
                    pos = next;
                }
                _time = Math.Min(_time, time+5);
                Thread.Sleep(10);
            }
        }
    }
}
