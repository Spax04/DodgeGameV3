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
using System.Threading;

namespace DodgeGameV3
{

    public sealed partial class MainPage : Page
    {
        GameBoard gameboard;

        Rectangle playerRect;
        Rectangle[] enemyRect;
        Rectangle startMenuRect;
        DispatcherTimer timer;

        public int scorePad = 0;
        public int totalResult;

        public MainPage()
        {
            this.InitializeComponent();

            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            gameboard = new GameBoard(windowRectangle.Width, windowRectangle.Height);
            startMenuRect = startMenu(gameboard);

        }

        //btn Start game. When btn is pressed - the game is begine.
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            //Removing "start menu" when btn was clicked
            myCanvas.Children.Remove(btnStartGame);
            myCanvas.Children.Remove(startMenuRect);
            myCanvas.Children.Remove(gameRules);
            

            myCanvas.Children.Add(btnPause);
            myCanvas.Children.Add(heart1);
            myCanvas.Children.Add(heart2);
            myCanvas.Children.Add(heart3);


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
        private async void timer_Tick(object sender, object e)
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
            for(int i = 0; i < enemyRect.Length; i++)
            {
                gameboard.livesCheck(playerRect,enemyRect[i], gameboard.player, myCanvas, timer);
            }
            
            if (gameboard.isLost == true)
            {
                gameOver();
            }

            //lvl
            currentLvl.Text = gameboard.lvlCounter.ToString();
            gameboard.lvlGrowing();
            //==============
            gameboard.boarderCollisionCheck(playerRect,gameboard.player,myCanvas );

            //hearts
            if(gameboard.player._lives == 2)
            {
                myCanvas.Children.Remove(heart1);

            }else if(gameboard.player._lives == 1)
            {
                myCanvas.Children.Remove(heart2);
            }else if(gameboard.player._lives == 0)
            {
                myCanvas.Children.Remove(heart3);
            }
            //=======

            //score
            scorePad = gameboard.winCheck(timer, gameboard.player, myCanvas, btnNext, scorePad);  // Нужно вернуть значение!!!!!!!
            scoreResult.Text = (gameboard.scorere + totalResult).ToString();//!!!!!
            gameboard.scoreCheck();
            

            
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
            // pause/play/next buttons removing
            myCanvas.Children.Remove(btnPause);
            myCanvas.Children.Remove(btnPlay);
            myCanvas.Children.Remove(btnNext);


            //gameover menu 
            myCanvas.Children.Remove(gameOverMenu);
            myCanvas.Children.Remove(gameOverImg);
            myCanvas.Children.Remove(scoreName);
            myCanvas.Children.Remove(btnRestart);
            myCanvas.Children.Remove(scoreResult);
            myCanvas.Children.Remove(heart1);
            myCanvas.Children.Remove(heart2);
            myCanvas.Children.Remove(heart3);
            myCanvas.Children.Remove(gotcha);


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
            myCanvas.Children.Add(currentLvl);

            myCanvas.Children.Add(heart1);
            myCanvas.Children.Add(heart2);
            myCanvas.Children.Add(heart3);

            playerRect = gameboard.player.createNewRectangle(playerRect, gameboard.player, myCanvas);
            enemyRect = new Rectangle[10];
            for (int i = 0; i < enemyRect.Length; i++)
            {
                enemyRect[i] = gameboard.enemy[i].createNewRectangle(enemyRect[i], gameboard.enemy[i], myCanvas);
            }
            timer.Start();
        }

        // GAME OVER
        void gameOver()
        {
            myCanvas.Children.Add(gameOverMenu);
            myCanvas.Children.Add(gameOverImg);
            myCanvas.Children.Add(scoreName);
            myCanvas.Children.Add(btnRestart);
            myCanvas.Children.Add(scoreResult);
            


        }
        //=======================


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

        // NEXT BUTTON
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            gameboard.lvlCounter++;
            totalResult += scorePad;
            removeUnits();
            gameboard.CreatingUnits();
            addUnits();  
            timer.Start();
            gameboard.player._lives = gameboard.currentLives;
            myCanvas.Children.Remove(btnNext);
        }
        //=================================================================
        public void removeUnits()
        {
            
            myCanvas.Children.Remove(playerRect);
            
            for(int i = 0; i < enemyRect.Length; i++)
            {
                myCanvas.Children.Remove(enemyRect[i]);
            }
        }

        public void addUnits()
        {
            playerRect = gameboard.player.createNewRectangle(playerRect, gameboard.player, myCanvas);
            enemyRect = new Rectangle[10];
            for (int i = 0; i < enemyRect.Length; i++)
            {
                enemyRect[i] = gameboard.enemy[i].createNewRectangle(enemyRect[i], gameboard.enemy[i], myCanvas);
            }
        }

        
    }
}

