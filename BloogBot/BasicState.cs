using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using BloogBot.Game.Objects;
using BloogBot.Game;
using BloogBot.AI;

namespace BloogBot
{
    class BasicState : IBotState
    {
        readonly LocalPlayer player;

        WoWUnit target;

        public BasicState()
        {
            player = ObjectManager.Player;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<string> ConsoleOutput { get; } = new ObservableCollection<string>();

        void Log(string message)
        {
            ConsoleOutput.Add($"({DateTime.Now.ToShortTimeString()}) {message}");
            OnPropertyChanged(nameof(ConsoleOutput));
        }

        public void Update()
        {
            //Console.WriteLine("Player guid {0}",Functions.GetPlayerGuid());
            //player.Jump();
        }
    }
}
