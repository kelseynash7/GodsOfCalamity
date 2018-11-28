using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GodsOfCalamity
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private List<UserControl> enemies = new List<UserControl>();
        private int maxEnemies = 10;
        private DispatcherTimer timer = new DispatcherTimer();
        private Random randomizer = new Random();
        private int Level { get; set; }

        public GamePage()
        {
            this.InitializeComponent();

            Loaded += (sender, args) =>
            {
                timer.Tick += TimerOnTick;
                timer.Interval = new TimeSpan(0, 0, 0, 2);
                timer.Start();
            };

            CompositionTarget.Rendering += GameLoop;
        }

        private void TimerOnTick(object sender, object o)
        {
            if (enemies.Count < maxEnemies)
            {
                var enemyType = randomizer.Next(1, 2);
                if (enemyType == 1)
                {
                    var enemy = new Meteor
                    {
                        AreaWidth = (int)LayoutRoot.ActualWidth,
                        Location =
                        new Point(randomizer.Next(0, (int)LayoutRoot.ActualWidth - 80), 0)
                    };

                    enemy.Velocity = enemy.Velocity * ((Level / (double)10) + 1);
                    enemies.Add(enemy);
                    Canvas.SetZIndex(enemy, 5);
                    LayoutRoot.Children.Add(enemy);
                }
                else
                {
                    var enemy = new Fire()
                    {
                        AreaWidth = (int)LayoutRoot.ActualWidth,
                        Location =
                            new Point(randomizer.Next(0, (int)LayoutRoot.ActualWidth - 80), 0)
                    };

                    enemy.PropagationRate = enemy.PropagationRate * ((Level / (double)10) + 1);
                    enemies.Add(enemy);
                    Canvas.SetZIndex(enemy, 5);
                    LayoutRoot.Children.Add(enemy);
                }
                
            }
        }

        private void GameLoop(object sender, object e)
        {
            //move enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Move();
                enemies[i].Margin = new Thickness(enemies[i].Location.X, enemies[i].Location.Y, 0, 0);

                if (enemies[i].Margin.Top > App.ScreenHeight)
                {
                    LayoutRoot.Children.Remove(enemies[i]);
                    enemies.Remove(enemies[i]);
                }
            }
        }
    }
}
