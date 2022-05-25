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

        
       

        public MainPage()
        {
            this.InitializeComponent();

            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            gameboard = new GameBoard(windowRectangle.Width, windowRectangle.Height);
            startMenuRect = startMenu(gameboard);



            gameboard.scoreCheck(currentScore);
        }

        //btn Start game. When btn is pressed - the game is begine.
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
            {
                //Removing "start menu" when btn was clicked
                myCanvas.Children.Remove(btnStartGame);
                myCanvas.Children.Remove(startMenuRect);
                myCanvas.Children.Remove(gameRules);

                myCanvas.Children.Add(btnPause);
                

                // timer has begone
            timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer.Tick += timer_Tick;
                timer.Start();

                // creating player and enemys
                playerRect = gameboard.player.createNewRectangle(playerRect, gameboard.player, myCanvas);
                enemyRect = new Rectangle[10];
                for (int i = 0; i < enemyRect.Length; i++)
                {
                    enemyRect[i] = gameboard.enemy[i].createNewRectangle(enemyRect[i], gameboard.enemy[i], myCanvas);

                }

                // Player Controller
                Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            
        }
        //==========
       
        // Timer
        private void timer_Tick(object sender, object e)
        {
            
            for (int i = 0; i < enemyRect.Length; i++)
            {
                gameboard.enemy[i].Move(enemyRect[i], gameboard.enemy[i], gameboard.player);
            }

            // enemy - enemy hit detector
            for (int i = 0; i < enemyRect.Length; i++)
            {
                for (int j = i + 1; j < enemyRect.Length; j++)
                {

                    gameboard.enemy[i].collisionCheck(enemyRect[i], gameboard.enemy[i], gameboard.enemy[j], myCanvas, timer); 
                }
            }

            // player - enemy hit detector

            for (int i = 0; i < enemyRect.Length; i++)
            {
                gameboard.player.collisionCheck(playerRect, gameboard.player, gameboard.enemy[i],myCanvas,timer);
                
            }


            gameboard.winCheck(timer,gameboard.player);

            gameboard.lvlGrowing();
           


            gameboard.lostCheck(timer);
            if(gameboard.isLost == true)
            {
                gameOver();
            }

            gameboard.heartCheck(myCanvas, heart1, heart2, heart3);

            currentLvl.Text = gameboard.lvlCounter.ToString();
            
        }
        //==================

        // Player Movment
        void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.Left:
                    
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

        // Start Menu Settings
        public Rectangle startMenu(GameBoard gb)
        {
            // pause/play buttons removing
            myCanvas.Children.Remove(btnPause);
            myCanvas.Children.Remove(btnPlay);

            //gameover menu 
            myCanvas.Children.Remove(gameOverMenu);
            myCanvas.Children.Remove(gameOverImg);
            myCanvas.Children.Remove(scoreName);
            myCanvas.Children.Remove(btnRestart);


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

        void resetAll()
        {
            gameboard = null;
            myCanvas.Children.Clear();

            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            gameboard = new GameBoard(windowRectangle.Width, windowRectangle.Height);

            myCanvas.Children.Add(btnPause);
            myCanvas.Children.Add(lvlSpace);
            myCanvas.Children.Add(lvlTxt);
            myCanvas.Children.Add(scoreTxt);
            myCanvas.Children.Add(currentLvl);
            myCanvas.Children.Add(currentScore);

            


            playerRect = gameboard.player.createNewRectangle(playerRect, gameboard.player, myCanvas);
            enemyRect = new Rectangle[10];
            for (int i = 0; i < enemyRect.Length; i++)
            {
                enemyRect[i] = gameboard.enemy[i].createNewRectangle(enemyRect[i], gameboard.enemy[i], myCanvas);

            }
            timer.Start();
        }

        void gameOver()
        {
            myCanvas.Children.Add(gameOverMenu);
            myCanvas.Children.Add(gameOverImg);
            myCanvas.Children.Add(scoreName);
            myCanvas.Children.Add(btnRestart);
        }

        //----------------BUTTONS---------------

        // RESTART BUTTON
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
           resetAll();
        }

        // PAUSE BUTTON
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            myCanvas.Children.Remove(btnPause);
            myCanvas.Children.Add(btnPlay);

        }

        // PLAY BUTTON
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            myCanvas.Children.Remove(btnPlay);
            myCanvas.Children.Add(btnPause);
        }
    }
}

