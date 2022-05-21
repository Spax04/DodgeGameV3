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



namespace DodgeGameV3
{
    
    public sealed partial class MainPage : Page
    {
        GameBoard gameboard;
        
        Rectangle playerRect;
        Rectangle[] enemyRect;
        DispatcherTimer timer;
        
        
        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,0,200);
            timer.Tick += timer_Tick;
            timer.Start();




            Rect windowRectangle = ApplicationView.GetForCurrentView().VisibleBounds;
            gameboard = new GameBoard(windowRectangle.Width,windowRectangle.Height);
            playerRect = createNewRectangle(gameboard.player);

            enemyRect = new Rectangle[10];
            for(int i = 0; i < enemyRect.Length; i++)
            {
                enemyRect[i] = createNewRectangle(gameboard.enemy[i]);
                /*System.Threading.Thread.Sleep(5000);*/              //spawn
            }

            // Player Controller
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            //Window.Current.CoreWindow.KeyUp += KeyIsUp; 
            //


        }


        //Timer

        private void timer_Tick(object sender,object e)
        {
            for(int i = 0; i < enemyRect.Length; i++)
            {
                enemyMove(enemyRect[i], gameboard.enemy[i], gameboard.player);
            }
            
            

            
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

        // Creating Rectangles
        public Rectangle createNewRectangle(UnitTool ut)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = ut._width;
            rectangle.Height = ut._height;

            if(ut is PlayerUnit)
            {
                rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
            }
            else
            {
                rectangle.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            
            Canvas.SetLeft(rectangle,ut._x);
            Canvas.SetTop(rectangle, ut._y);

            myCanvas.Children.Add(rectangle);

        
            return rectangle;
        }
       //===============

        //Enemy moovment

         void enemyMove(Rectangle enemyRect,UnitTool e1,UnitTool p1)
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
                e1._y = (int)Canvas.GetTop(enemyRect)+- e1._speed;
            }

         }
    }
}
