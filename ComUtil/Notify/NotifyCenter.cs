using ComUtil.Interface;
using ComUtil.NotifyEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComUtil.Notify
{
    /// <summary>
    /// 进度委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SubProgressInfoHandle(object sender, ProgressEventArgs e);
    public class NotifyCenter:INotifyCenter
    {
        /// <summary>
        /// 进度委托事件
        /// </summary>
        public event SubProgressInfoHandle SubProgressInfo;
        /// <summary>
        ///  进度委托事件保护函数
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSubProgressInfo(ProgressEventArgs e)
        {
            if (SubProgressInfo != null)
            {
                SubProgressInfo(this, e);
            }
        }
        public void PublishProgressInfo(object param)
        {
            long progressMax = ((long[])param)[0];
            long progressCurrect = ((long[])param)[1];
            OnSubProgressInfo(new ProgressEventArgs() { ProgressMax= progressMax, ProgressCurrect= progressCurrect });
        }
    }
}
