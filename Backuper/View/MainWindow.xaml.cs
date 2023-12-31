﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Backuper.Model;
using Backuper.Model.Interfaces;
using Backuper.ViewModel;

namespace Backuper;

public partial class MainWindow : Window
{
    private SaveModel saver;
    private ILogger logger;
    public MainWindow()
    {
        saver = new SaveModel();
        logger = saver.Logger;
        var dataKeeper = new DataKeeperViewModel(saver.DataKeeper, logger);

        logger.Log("Запуск приложения", LoggerLevel.Info);

        BackupModel.Backup(dataKeeper.SourceDirectory, 
            dataKeeper.TargetDirectory, logger);

        InitializeComponent();

        // Создаём привязки к TextBoxs и ComboBox
        var binding = new Binding();
        binding.Source = dataKeeper;
        binding.Mode = BindingMode.TwoWay;
        binding.Path = new PropertyPath("SourceDirectory");
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        SourceTextBox.SetBinding(TextBox.TextProperty, binding);

        binding = new Binding();
        binding.Source = dataKeeper;
        binding.Mode = BindingMode.TwoWay;
        binding.Path = new PropertyPath("TargetDirectory");
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        TargetTextBox.SetBinding(TextBox.TextProperty, binding);

        binding = new Binding();
        binding.Source = dataKeeper;
        binding.Mode = BindingMode.TwoWay;
        binding.Path = new PropertyPath("LoggerLevel");
        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
        ComboBoxLogLevel.SetBinding(ComboBox.SelectedIndexProperty, binding);
    }

    // Собитие при запуске окна, обеспечивает работу приложения в фоне
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Hide();

        var notifyIcon = new System.Windows.Forms.NotifyIcon();
        notifyIcon.Icon = new System.Drawing.Icon("Resources\\icon.ico");
        notifyIcon.Visible = true;

        notifyIcon.DoubleClick += (s, args) =>
        {
            Show();
            WindowState = WindowState.Normal;
        };

        notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
        notifyIcon.ContextMenuStrip.Items.Add("Сделать резервное копирование", null, (s, args) =>
        {
            BackupModel.Backup(SourceTextBox.Text, TargetTextBox.Text, logger);
        });
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

    // Обработкич события по клику кнопки для выбора пути
    private void SelectPath_Click(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        string textBoxName = button.Tag.ToString();

        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
        {
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (textBoxName == "SourceTextBox")
                    SourceTextBox.Text = dialog.SelectedPath;
                else if (textBoxName == "TargetTextBox")
                    TargetTextBox.Text = dialog.SelectedPath;
            }
        }
    }

    private void Backup_Click(object sender, RoutedEventArgs e)
    {
        BackupModel.Backup(SourceTextBox.Text, TargetTextBox.Text, logger);
    }
}