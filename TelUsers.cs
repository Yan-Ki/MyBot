using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;



namespace MyBot
{
     public class  TelUsers
    {
        private static MainWindow w;
        public static  ITelegramBotClient bot;
        public ObservableCollection<User> Users { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="W"></param>
        public  TelUsers(MainWindow W)
        {
            w = W;
            Users = new ObservableCollection<User>();
           w.MesInput.ItemsSource = Users;
            bot = new TelegramBotClient("Введите сюда ваш токен");   //---------------------------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
            
        }
        /// <summary>
        /// Метод получения ошибок
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        /// <summary>
        /// Метод получения сообщения
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

           
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                switch (message.Type)
                {
                    case Telegram.Bot.Types.Enums.MessageType.Text:
                        {
                            string msg = $"{message.Date}:{message.Chat.FirstName} {message.Chat.Id} {message.Text}";
                            Debug.WriteLine(msg);
                            w.Dispatcher.Invoke(() =>
                            {
                                User person = new User(update.Message.Chat.FirstName, update.Message.Chat.Id);
                                                               
                                if(!Users.Contains(person)) Users.Add(person);
                                Debug.WriteLine(Users.IndexOf(person));
                                Users[Users.IndexOf(person)].AddMessage($"{person.Name}:{message.Text}\n{message.Date}");
                              
                            });
                         
                        }
                        break;

                    default:
                        await botClient.SendTextMessageAsync(message.Chat, $"Пришлите текст");
                        break;
                }

            }
        }
       
    }
}
 