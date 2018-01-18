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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading;
using System.Windows.Threading;

namespace projetPOO
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Labyrinthe
        private Maze maze;
        //Images du labyrinthe
        private Image[][] images;
        //Largeur
        private int height;
        //Hauteur
        private int width;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        //Genere le labyrinthe et l'affiche
        private void generateMazeFunction(object sender, RoutedEventArgs e)
        {
            generateMaze.IsEnabled = false;
            MainGrid.Children.Clear();

            maze = new Maze(int.Parse(MazeHeight.Text), int.Parse(MazeWidth.Text));

            DateTime startGen, endGen, startDisplay, endDisplay;

            startGen = DateTime.Now;
            if (GenerateMod1.IsChecked == true)
            {
                maze.GenerateMaze();
            }
            else if (GenerateMod2.IsChecked == true)
            {
                maze.GenerateMaze(2);
            }
            else
            {
                maze.GenerateMaze();
            }
            endGen = DateTime.Now;


            startDisplay = DateTime.Now;
            Case[][] mazeArray = maze.GetMazeArray();

            for (int i = 0; i < mazeArray.Length; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(10, GridUnitType.Pixel);
                MainGrid.RowDefinitions.Add(row);
                for (int j = 0; j < mazeArray[i].Length; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(10, GridUnitType.Pixel);
                    MainGrid.ColumnDefinitions.Add(col);

                    Image img = new Image();
                    var uri = new Uri(@"pack://application:,,,/img/15.png");
                    if (mazeArray[i][j] != null)
                    {
                        if (maze.GetStartX() == i && maze.GetStartY() == j)
                        {
                            uri = new Uri(@"pack://application:,,,/img/start-" + mazeArray[i][j].GetValue() + ".png");
                        }
                        else if (maze.GetEndX() == i && maze.GetEndY() == j)
                        {
                            uri = new Uri(@"pack://application:,,,/img/end-" + mazeArray[i][j].GetValue() + ".png");
                        }
                        else
                        {
                            uri = new Uri(@"pack://application:,,,/img/" + mazeArray[i][j].GetValue() + ".png");
                        }
                    }

                    img.Width = 10;
                    img.Height = 10;
                    img.Source = new BitmapImage(uri);
                    MainGrid.Children.Add(img);

                    Grid.SetRow(img, i);
                    Grid.SetColumn(img, j);
                }

                endDisplay = DateTime.Now;
                TimeSpan timeSpendGen = endGen.Subtract(startGen);
                TimeSpan timeSpendDisplay = endDisplay.Subtract(startDisplay);
                isAllConnectedLabel.Content = "Is All Connected : " + maze.IsAllCaseLinked() + " | Generation Time : " +
                                              timeSpendGen.Milliseconds + " ms | Display Time : " +
                                              timeSpendDisplay.Milliseconds + " ms";
                generatePath.IsEnabled = true;
                generateMaze.IsEnabled = true;
            }
        }
        //Genere le labyrinthe et l'affiche
        private void generatePath_Click(object sender, RoutedEventArgs e)
        {
            generatePath.IsEnabled = false;
            PathFinding pf = new PathFinding(maze);
            Case[][] mazeArray = maze.GetMazeArray();

            for (int i = 0; i < mazeArray.Length; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(10, GridUnitType.Pixel);
                MainGrid.RowDefinitions.Add(row);
                for (int j = 0; j < mazeArray[i].Length; j++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(10, GridUnitType.Pixel);
                    MainGrid.ColumnDefinitions.Add(col);

                    Image img = new Image();
                    var uri = new Uri(@"pack://application:,,,/img/15.png");
                    bool isAPath = false;
                    Image imgDirection = new Image();
                    var uriDirection = new Uri(@"pack://application:,,,/img/arrow-top.png");
                    string direction = pf.GetDirectionOfCase(i, j);
                    if (mazeArray[i][j] != null)
                    {
                        if (maze.GetStartX() == i && maze.GetStartY() == j)
                        {
                            uri = new Uri(@"pack://application:,,,/img/start-" + mazeArray[i][j].GetValue() + ".png");
                        }
                        else if (maze.GetEndX() == i && maze.GetEndY() == j)
                        {
                            uri = new Uri(@"pack://application:,,,/img/end-" + mazeArray[i][j].GetValue() + ".png");
                        }
                        else if (pf.IsCaseInPath(i, j))
                        {
                            uri = new Uri(@"pack://application:,,,/img/path-" + mazeArray[i][j].GetValue() + ".png");
                            if (direction == "right")
                            {
                                uriDirection = new Uri(@"pack://application:,,,/img/arrow-right.png");
                            }
                            else if (direction == "left")
                            {
                                uriDirection = new Uri(@"pack://application:,,,/img/arrow-left.png");
                            }
                            else if (direction == "bottom")
                            {
                                uriDirection = new Uri(@"pack://application:,,,/img/arrow-bottom.png");
                            }
                            isAPath = true;
                        }
                        else
                        {
                            uri = new Uri(@"pack://application:,,,/img/" + mazeArray[i][j].GetValue() + ".png");
                        }
                    }
                    img.Width = 10;
                    img.Height = 10;
                    img.Source = new BitmapImage(uri);
                    MainGrid.Children.Add(img);

                    Grid.SetRow(img, i);
                    Grid.SetColumn(img, j);
                    if (isAPath)
                    {
                        imgDirection.Width = 10;
                        imgDirection.Height = 10;
                        imgDirection.Source = new BitmapImage(uriDirection);

                        MainGrid.Children.Add(imgDirection);
                        Grid.SetRow(imgDirection, i);
                        Grid.SetColumn(imgDirection, j);
                    }
                }

                generatePath.IsEnabled = true;
            }
        }
        
        //Genere le labyrinthe et l'affiche avec une animation
        private void generateMazeAnimate(object sender, RoutedEventArgs e)
        {
            generatePath.IsEnabled = false;
            generateMaze.IsEnabled = false;
            generateAnimateMaze.IsEnabled = false;
            MainGrid.Children.Clear();

            maze = new Maze(int.Parse(MazeHeight.Text), int.Parse(MazeWidth.Text));

            DateTime startGen, endGen, startDisplay, endDisplay;


            startDisplay = DateTime.Now;
            this.Dispatcher.Invoke(() =>
            {
                images = new Image[int.Parse(MazeHeight.Text)][];
                for (int i = 0; i < int.Parse(MazeHeight.Text); i++)
                {
                    images[i] = new Image[int.Parse(MazeWidth.Text)];
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(10, GridUnitType.Pixel);
                    MainGrid.RowDefinitions.Add(row);
                    for (int j = 0; j < int.Parse(MazeWidth.Text); j++)
                    {
                        ColumnDefinition col = new ColumnDefinition();
                        col.Width = new GridLength(10, GridUnitType.Pixel);
                        MainGrid.ColumnDefinitions.Add(col);

                        Image img = new Image();
                        var uri = new Uri(@"pack://application:,,,/img/15.png");

                        img.Width = 10;
                        img.Height = 10;
                        img.Source = new BitmapImage(uri);
                        images[i][j] = img;
                        MainGrid.Children.Add(img);

                        Grid.SetRow(img, i);
                        Grid.SetColumn(img, j);
                    }
                }
            });
            endDisplay = DateTime.Now;

            startGen = DateTime.Now;
            if (GenerateMod1.IsChecked == true)
            {
                maze.GenerateMaze();
            }
            else if (GenerateMod2.IsChecked == true)
            {
                maze.GenerateMaze(2);
            }
            else
            {
                maze.GenerateMaze();
            }
            endGen = DateTime.Now;

//            Task.Run(() => DisplayWithAnimation());

            Thread myThread = new Thread(new ThreadStart(DisplayWithAnimation));

            TimeSpan timeSpendGen = endGen.Subtract(startGen);
            TimeSpan timeSpendDisplay = endDisplay.Subtract(startDisplay);
            isAllConnectedLabel.Content = "Is All Connected : " + maze.IsAllCaseLinked() + " | Generation Time : " +
                                          timeSpendGen.Milliseconds + " ms | Display Time : " +
                                          timeSpendDisplay.Milliseconds + " ms";
            myThread.Start();
        }
        //Fonctioj utilise pour l'affichage du labyrinthe avec une animation
        private void DisplayWithAnimation()
        {
            Case[][] mazeArray = maze.GetMazeArray();
            Thread.Sleep(500);
            for (int y = 0; y < mazeArray.Length; y++)
            {
                for (int z = 0; z < mazeArray[y].Length; z++)
                {
                    var uri = new Uri(@"pack://application:,,,/img/15.png");
                    if (mazeArray[y][z] != null)
                    {
                        if (maze.GetStartX() == y && maze.GetStartY() == z)
                        {
                            uri = new Uri(
                                @"pack://application:,,,/img/start-" + mazeArray[y][z].GetValue() + ".png");
                        }
                        else if (maze.GetEndX() == y && maze.GetEndY() == z)
                        {
                            uri = new Uri(@"pack://application:,,,/img/end-" + mazeArray[y][z].GetValue() + ".png");
                        }
                        else
                        {
                            uri = new Uri(@"pack://application:,,,/img/" + mazeArray[y][z].GetValue() + ".png");
                        }
                    }
                    height = y;
                    width = z;
                    Dispatcher.Invoke(() =>
                    {
                        images[height][width].Source = new BitmapImage(uri);
                        Thread.Sleep(int.Parse(MazeDisplayTime.Text));
                    });
                }
            }
            Dispatcher.Invoke(() =>
            {
                generatePath.IsEnabled = true;
                generateMaze.IsEnabled = true;
                generateAnimateMaze.IsEnabled = true;
            });
        }
    }
}