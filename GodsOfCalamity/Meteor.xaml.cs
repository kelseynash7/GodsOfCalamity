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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GodsOfCalamity
{
    public sealed partial class Meteor : UserControl
    {
        public int AreaWidth { get; set; }
        public Windows.Foundation.Point Location { get; set; }
        public double Velocity;
        private readonly Random randomizer = new Random();
        private int direction;
        private int directionCount = 0;

        public Meteor()
        {
            this.InitializeComponent();
            Velocity = randomizer.Next(1, 3);
        }

        public void Move()
        {
            int move;

            //randomize move direction
            if (directionCount == 0)
            {
                direction = randomizer.Next(1, 3);
            }

            if (direction == 1){
                move = -1;
            }
            else
            {
                move = 1;
            }
            directionCount++;

            //Change direction every 30 count
            if (directionCount > 30)
            {
                directionCount = 0;
            }

            //Check that we don't go through walls
            if (Location.X + direction < 0)
            {
                move = 0;
            }
            if (Location.X + direction > AreaWidth)
            {
                move = AreaWidth;
            }

            //Set new location
            Location = new Windows.Foundation.Point(Location.X + move, Location.Y + Velocity);
        }
    }
}
