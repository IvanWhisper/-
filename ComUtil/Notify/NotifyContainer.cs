using ComUtil.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComUtil.Notify
{
    public class NotifyContainer: INotifyContainer
    {
        private INotifyCenter notifyCenter;

        private static NotifyContainer _instance = null;
        private static readonly object SynObject = new object();

        NotifyContainer()
        {
        }
        public static NotifyContainer Instance
        {
            get
            {
                // Double-Checked Locking
                if (null == _instance)
                {
                    lock (SynObject)
                    {
                        if (null == _instance)
                        {
                            _instance = new NotifyContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        public INotifyCenter UseNotifyCenter
        {
            get
            {
               return this.notifyCenter;
            }

            set
            {
                this.notifyCenter=value;
            }
        }
    }
}
