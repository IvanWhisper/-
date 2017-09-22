using System;
using System.Collections.Generic;
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

namespace 文件加密
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string a = "Server=127.0.0.1;Database=yundaclient;Uid=root;Pwd=abc@123456;Charset=utf8";

            string key = "om2h&lyusy#K6atfa3(#SPimIAoVCcbu18ufj$oUyxjv0&!LGmhL&EV6dq64&AQqwHs*No&folC4FVUMIAoz%yxVKVb#6SXVE#(RL$*1qb6Tb1Zq4SU4v1u(t5v!ncH4";


            string b = EncryptHelper.AESEncrypt(a,key);


            Console.WriteLine(b);

            string c = EncryptHelper.AESDecrypt(b, key);

            Console.WriteLine(c);
            //Console.WriteLine(a.Equals(c));

        }
    }
}
