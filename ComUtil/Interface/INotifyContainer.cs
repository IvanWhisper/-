using ComUtil.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComUtil.Interface
{
    public interface INotifyContainer
    {
        INotifyCenter UseNotifyCenter { get; set; }
    }
}
