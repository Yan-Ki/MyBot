using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MyBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public TelUsers MyBot;
        
        public MainWindow()
        {
            InitializeComponent();
            MyBot = new TelUsers(this);
                  }
     
        private void MesInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window2 Chat = new Window2(this);
            Chat.Owner = this;
            Chat.Show();
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|JSON Files(*.json)|*.json|All(*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(MyBot);
                System.IO.File.WriteAllText(saveFileDialog.FileName, json);
            }
        }
    }
}
