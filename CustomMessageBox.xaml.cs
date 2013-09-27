using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace efcClient.Windows
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        CustomMessageBoxResult _result;

        private CustomMessageBox()
        {
            InitializeComponent();
        }

        public static CustomMessageBoxResult ShowDialog(string caption, string message, string button1Text, string button2Text)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            return messageBox.ShowDialogInternal(caption, message, button1Text, button2Text);
        }

        public CustomMessageBoxResult ShowDialogInternal(string caption, string message, string button1Text, string button2Text)
        {
            Title = caption;
            messageBlock.Text = message;
            button1.Content = button1Text;
            button2.Content = button2Text;

            ShowDialog();
            return _result;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _result = CustomMessageBoxResult.Button1;
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _result = CustomMessageBoxResult.Button2;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }

    public enum CustomMessageBoxResult
    {
        Button1 = 1,
        Button2 = 2,
    }
}
