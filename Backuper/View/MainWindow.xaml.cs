using System;
using System.Windows;
using System.Windows.Forms;
using Backuper.Model;

namespace Backuper
{
    public partial class MainWindow : Window
    {
        private SaveModel saver;
        public MainWindow()
        {
            saver = new SaveModel();
            BackupModel backup = new(saver.DataKeeper.SourceDirectory, 
                saver.DataKeeper.TargetDirectory);

            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Hide();

            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("Resources\\icon.ico");
            notifyIcon.Visible = true;

            notifyIcon.DoubleClick += (s, args) =>
            {
                Show();
                WindowState = WindowState.Normal;
            };

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Открыть настройки", null, (s, args) =>
            {
                Show();
                WindowState = WindowState.Normal;
            });
            notifyIcon.ContextMenuStrip.Items.Add("Закрыть приложение", null, (s, args) =>
            {
                notifyIcon.Dispose();
                saver.Save();
                System.Windows.Application.Current.Shutdown();
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
