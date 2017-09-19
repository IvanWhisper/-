using ComUtil.FileOperation;
using ComUtil.Interface;
using ComUtil.Notify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 大文件读写
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NotifyContainer.Instance.UseNotifyCenter = new NotifyCenter();
            FileIO fio = new FileIO(1024*100, NotifyContainer.Instance);
            fio.ReadPath= @".\in.sql";
            fio.WritePath = @".\out.sql";
            new Task(()=> {
                fio.ReadStreamFromFile();
            }).Start();
            NotifyContainer.Instance.UseNotifyCenter.SubProgressInfo += UseNotifyCenter_SubProgressInfo;
        }

        private void UseNotifyCenter_SubProgressInfo(object sender, ComUtil.NotifyEventArgs.ProgressEventArgs e)
        {
            this.Dispatcher.Invoke(() => {
                prog.Maximum = e.ProgressMax;
                prog.Value = e.ProgressCurrect;
                label.Content = string.Format("{0}/{1}", e.ProgressCurrect, e.ProgressMax);
            });
        }

        private void ReadStreamFromFile()
        {
            string filePath = @".\in.sql";
            int bufferSize = 1024000; //每次读取的字节数
            byte[] buffer = new byte[bufferSize];
            System.IO.FileStream stream = null;

                stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);

            FileStream fsWrite = File.OpenWrite(@".\out.sql");

            long fileLength = stream.Length;//文件流的长度
            double a = (double)(fileLength) / (double)bufferSize;
                int readCount = (int)Math.Ceiling((double)(fileLength)/ (double)bufferSize); //需要对文件读取的次数
                int tempCount = 0;//当前已经读取的次数

               //MessageBox.Show(readCount.ToString());

            try
            {
                do
                {
                    stream.Read(buffer,0, bufferSize);
                    //分readCount次读取这个文件流，每次从上次读取的结束位置开始读取bufferSize个字节
                    //这里加入接收和处理数据的逻辑
                    fsWrite.Write(buffer,0, bufferSize);
                    tempCount++;
                    //
                }
                while (tempCount < readCount);
            }
            catch
            {

            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
                fsWrite.Close();
                MessageBox.Show(readCount.ToString());
            }
        }
    }
}
