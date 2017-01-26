using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class Observable
    {
        List<IWatcher> watchers;
        public void AddWatcher(IWatcher watcher)
        {
            watchers.Add(watcher);
        }
        public void AddWatchers(List<IWatcher> watchers)
        {
            this.watchers.AddRange(watchers);
        }

        public Observable()
        {
            this.watchers = new List<IWatcher>();
        }
        public void CallWatchers(object data)
        {
            foreach (IWatcher watcher in watchers)
            {
                watcher.Notify(this, data);
            }
        }
    }
}
