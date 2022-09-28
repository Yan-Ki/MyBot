using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MyBot
{
    public class User : INotifyPropertyChanged, IEquatable<User>
    {
        public string Name { get; set; }
        private long id;
        public long Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Messages { get; set; }

        public User(string Name, long Id) 
        {
            this.Name = Name;
            this.Id = Id;
            Messages = new ObservableCollection<string>();
           

        }
        
        public bool Equals(User other) => other.Id == this.id;
        public void AddMessage(string Text)
        {
            this.Messages.Add(Text);
        }
    }
}
