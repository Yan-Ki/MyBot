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
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MyBot
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        
        private static MainWindow w;
        User thisUser;
        public Window2(MainWindow win)
        {
            InitializeComponent();
            w = win;
           thisUser = w.MyBot.Users[w.MyBot.Users.IndexOf(w.MesInput.SelectedItem as User)];
           chatList.ItemsSource = thisUser.Messages;
        }
        private void OutputMes_Click(object sender, RoutedEventArgs e)
        {
           

            if (!String.IsNullOrEmpty(mesSend.Text))
            {
                string messa = $"Admin: {mesSend.Text} \n {DateTime.Now}";
                thisUser.Messages.Add(messa);
                TelUsers.bot.SendTextMessageAsync(thisUser.Id, mesSend.Text);
                mesSend.Text = String.Empty;
            }
            else MessageBox.Show("Напишиnt сообщение");


        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|JSON Files(*.json)|*.json|All(*.*)|*.*"
            };
            
            if (saveFileDialog.ShowDialog() == true)
            {
                string json = JsonConvert.SerializeObject(thisUser);
                System.IO.File.WriteAllText(saveFileDialog.FileName, json);
            }
                
        }
    }
}
