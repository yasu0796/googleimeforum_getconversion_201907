// 参考サイト
// http://robotastics.wpblog.jp/技術情報/c-net/imestatus-get/

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            // Call this procedure when the application starts.  
            // Set to 1 second.  
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Enable timer.  
            timer1.Enabled = true;
        }

        // WinAPIで必要なものを読み込み
        [DllImport("User32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);
        [DllImport("imm32.dll")]
        static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern uint GetGUIThreadInfo(uint dwthreadid, ref GUITHREADINFO lpguithreadinfo);

        // GuiThredInfoの変数の型を定義
        public struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public System.Drawing.Rectangle rcCaret;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // IME状態の取得
            GUITHREADINFO gti = new GUITHREADINFO();
            gti.cbSize = Marshal.SizeOf(gti);
            uint GUIinfo = GetGUIThreadInfo(0, ref gti);
            IntPtr imwd = ImmGetDefaultIMEWnd(gti.hwndFocus);

            int imes1 = SendMessage(imwd, 0x0283, 0x0001, 0); // IME状態を取得（有効，無効）

            label1.Text = imes1.ToString();
        }

    }
}
