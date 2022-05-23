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
using DodgeGameV3.Units;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;
using System.Windows;
using Windows.Media.Devices;
using System.Threading.Tasks;



using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGameV3
{

    public sealed partial class MainPage : Page
    {
        GameBoard gameboard;

        Rectangle playerRect;
        Rectangle[] enemyRect;
        Rectangle startMenuRect;
        DispatcherTimer timer;
        public bool collision = false;
        public int enemyCounter = 0;
        public int z = 0;




        public MainPage()
        {
            this.InitializeComponent();

            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            gameboard = new GameBoard(windowRectangle.Width, windowRectangle.Height);
            startMenuRect = startMenu(gameboard);





        }


        // Timer
        private void timer_Tick(object sender, object e)
        {
            for (int i = 0; i < enemyRect.Length; i++)
            {
                enemyMove(enemyRect[i], gameboard.enemy[i], gameboard.player);
            }

            for(int i = 0; i < enemyRect.Length; i++)
            {
                for(int j = i +1; j< enemyRect.Length; j++)
                {
                    collisionCheck(enemyRect[i], gameboard.enemy[i], gameboard.enemy[j]);
                }
            }

            if(enemyCounter == 9)
            {
                timer.Stop();
            }

        }

        //==================

        // Player Movment
        void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Left:
                    /*if(gameboard.player._x == 0)
                    {
                        Canvas.SetLeft(playerRect, myCanvas.MaxWidth);
                    }else*/
                    Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) - gameboard.player._speed);
                    gameboard.player._x = (int)Canvas.GetLeft(playerRect) - gameboard.player._speed;
                                        
                        
                    
                    break;

                case VirtualKey.Right:
                    Canvas.SetLeft(playerRect, Canvas.GetLeft(playerRect) + gameboard.player._speed);
                    gameboard.player._x = (int)Canvas.GetLeft(playerRect) + gameboard.player._speed;
                    break;

                case VirtualKey.Up:
                    Canvas.SetTop(playerRect, Canvas.GetTop(playerRect) - gameboard.player._speed);
                    gameboard.player._y = (int)Canvas.GetTop(playerRect) - gameboard.player._speed;
                    break;

                case VirtualKey.Down:
                    Canvas.SetTop(playerRect, Canvas.GetTop(playerRect) + gameboard.player._speed);
                    gameboard.player._y = (int)Canvas.GetTop(playerRect) + gameboard.player._speed;
                    break;


            }
        }

        //============

        //Enemy moovment
        void enemyMove(Rectangle enemyRect, UnitTool e1, UnitTool p1)
        {

            if (e1._x > p1._x)
            {
                Canvas.SetLeft(enemyRect, Canvas.GetLeft(enemyRect) - e1._speed);
                e1._x = (int)Canvas.GetLeft(enemyRect) - e1._speed;
            }
            if (e1._x < p1._x)
            {
                Canvas.SetLeft(enemyRect, Canvas.GetLeft(enemyRect) + e1._speed);
                e1._x = (int)Canvas.GetLeft(enemyRect) + e1._speed;
            }
            if (e1._y > p1._y)
            {
                Canvas.SetTop(enemyRect, Canvas.GetTop(enemyRect) - e1._speed);
                e1._y = (int)Canvas.GetTop(enemyRect) - e1._speed;
            }
            if (e1._y < p1._y)
            {
                Canvas.SetTop(enemyRect, Canvas.GetTop(enemyRect) + e1._speed);
                e1._y = (int)Canvas.GetTop(enemyRect) + -e1._speed;
            }
        }
        //============

        // cheking hits method
        async void collisionCheck(Rectangle enemyRectangle, UnitTool enemyRectOne, UnitTool enemyRectTwo)
        {
            if (enemyRectOne._x + enemyRectOne._width >= enemyRectTwo._x &&
                enemyRectOne._x <= enemyRectTwo._x + enemyRectTwo._width &&
                enemyRectOne._y + enemyRectOne._height >= enemyRectTwo._y &&
                enemyRectOne._y <= enemyRectTwo._y + enemyRectTwo._height)

            {
                //style part: 
                enemyRectangle.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/DeadFish2.png"))
                    
                    
                };
                enemyRectangle.Width = 50;
                enemyRectangle.Height = 50;
                enemyRectOne._speed = 0;

                await Task.Delay(4000); //async pause 
                myCanvas.Children.Remove(enemyRectangle);
                
            }
        }
        //============

        // Start Menu Stings
        public Rectangle startMenu(GameBoard gb)
        {
            Rectangle startRect = new Rectangle();
            startRect.Width = gb._boardWidth;
            startRect.Height = gb._boardHeight;
            startRect.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/startScreen.jpg"))
            };

            Canvas.SetLeft(startRect, myCanvas.MinWidth);
            Canvas.SetTop(startRect, myCanvas.MinHeight);
            myCanvas.Children.Add(startRect);

            return startRect;
        }
        //================

        //btn Start game. When btn is pressed - game is begane.
        private void btnStartGame_Click(object sender, RoutedEventArgs e) 
        {
            //Removing start menu while btn was clicked
            myCanvas.Children.Remove(btnStartGame);
            myCanvas.Children.Remove(startMenuRect);
            myCanvas.Children.Remove(gameRules);

            // timer has begone
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();

            // creating player and enemys
             playerRect = gameboard.player.createNewRectangle(playerRect,gameboard.player,myCanvas);
            enemyRect = new Rectangle[10];
            for (int i = 0; i < enemyRect.Length; i++)
            {
                enemyRect[i] = gameboard.enemy[i].createNewRectangle(enemyRect[i], gameboard.enemy[i], myCanvas);

            }

            // Player Controller
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }
        //==========
    }
}

