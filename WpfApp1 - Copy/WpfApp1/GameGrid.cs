using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1
{
    public class GameGrid

    {
        #region Variables
        /// <summary>
        /// This stores the selected theme
        /// </summary>
        private string themepath = "HarryPotter";
        /// <summary>
        /// Creates a grid var
        /// </summary>
        private Grid grid;
        /// <summary>
        /// Amount of Columns in the Gamegrid
        /// </summary>
        private int colNum;
        /// <summary>
        /// Amount of Rows in the Gamegrid 
        /// </summary>
        private int rowNum;
        /// <summary>
        /// Total amount of cards (rowNum * colNum)
        /// </summary>
        private int totalNum;
        /// <summary>
        /// Total amount of cards (rowNum * colNum/2)
        /// </summary>
        private int halfNum;
        /// <summary>
        /// Integer that will give each card a unique ID
        /// </summary>
        int cardId = 1;
        /// <summary>
        /// Image that will store the properties of the first clicked image DEFAULT=NULL
        /// </summary>
        Image firstClick = null;
        /// <summary>
        /// Image that will store the properties of the second clicked image DEFAULT=NULL
        /// </summary>
        Image secondClick = null;
        /// <summary>
        /// Bool that will specify if a card is already clicked(True) or not (False) DEFAULT=FALSE
        /// </summary>
        bool clicked = false;
        /// <summary>
        /// Integer that will count how many paired cards there are in the current game (If matchedpairs == halfNum game is over) DEFAULT=0
        /// </summary>
        int matchedPairs = 0;
        /// <summary>
        /// Timer for card turn delay (after 2 not matching cards)
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer();
        /// <summary>
        /// Bool that makes sure the timer is only activated once
        /// </summary>
        bool initiateTimer = false;
        /// <summary>
        /// Bool that makes sure that the user cannot turn around any card in the card turn delay 9and thus breaking the game) DEFAULT=TRUE
        /// </summary>
        bool AllowClick = true;
        /// <summary>
        /// Bool to check if the game is won or not after game ending (true == won, false == lost) DEFAULT=FALSE
        /// </summary>
        bool gameWin = false;
        /// <summary>
        /// Card turn delay interval
        /// </summary>
        double timerInterval = 0.60;

        #endregion

        #region Game Order of Operations

        public GameGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            this.colNum = cols;
            this.rowNum = rows;
            InitializeMemoryGrid(cols, rows);
            AddImages();
        }
        #endregion

        #region Create Grid
        /// <summary>
        /// Creates a grid in the WPF application
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        private void InitializeMemoryGrid(int cols, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());

            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            }
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            totalNum = colNum * rowNum;
            halfNum = colNum * rowNum/2;
        }
        #endregion

        #region Image list and placement
        /// <summary>
        /// Function for inserting cardBack images and Front images to cardBack.Tag
        /// </summary>
        public void AddImages()
        {
            
            
            List<ImageSource> imageList = GetImageList();
            for (int row = 0; row < rowNum; row++)
            {
                for (int column = 0; column < colNum; column++)
                {
                    Image cardBack = new Image();
                    cardBack.Source = new BitmapImage(new Uri("Resources/Theme/"+ themepath + "/cardBack.jpg", UriKind.Relative));
                    cardBack.Tag = imageList.First();
                    cardBack.Uid = cardId.ToString();
                    imageList.RemoveAt(0);
                    cardBack.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(cardBack, column);
                    Grid.SetRow(cardBack, row);
                    grid.Children.Add(cardBack);
                    cardId++;
                }
            }
        }
        /// <summary>
        /// randomizes imagePaths as string and converts them to ImageSource (in a list)
        /// </summary>
        /// <returns></returns>
            private List<ImageSource> GetImageList()
            {
            
            List<string> Stage1 = new List<string>();
            for (int i = 1; i < halfNum + 1; i++)
            {
                string imagePath = string.Format(@"Resources/Theme/" + themepath + "/Front" + i + ".jpg");

                Stage1.Add(imagePath);
                Stage1.Add(imagePath);

            }

            Random random = new Random();
            
            List<ImageSource> imageList = new List<ImageSource>();
                
                for (int i = 1; i < totalNum +1; i++)
                {
                
                int rnd = random.Next(Stage1.Count);
                string source = Stage1[rnd];
                    ImageSource CardPath = new BitmapImage(new Uri(source, UriKind.Relative));
                    imageList.Add(CardPath);

                Stage1.RemoveAt(rnd);

            }
           
                return imageList;
            }


        #endregion

        #region Mouse interaction and card functionality
        /// <summary>
        /// Turns cards around after click and checks selected cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CardClick(object sender, MouseButtonEventArgs e)
        {
            if (AllowClick == true) { 



            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
            if (initiateTimer == false)
            {
                timer.Interval = TimeSpan.FromSeconds(timerInterval);
                timer.Tick += timer_Tick;
                initiateTimer = true;
            }
            if (firstClick == null)
            {
                
                firstClick = card;
                clicked = true;


            }
            else if (secondClick == null)
            {
                
                secondClick = card;
                if (firstClick.Source.ToString() == secondClick.Source.ToString() && firstClick.Uid.ToString() != secondClick.Uid.ToString())
                {
                    MessageBox.Show("Matched!");
                    matchedPairs++;
                    firstClick.IsEnabled = false;
                    secondClick.IsEnabled = false;
                    firstClick = null;
                    
                    secondClick = null;
                    
                    if (matchedPairs == halfNum)
                        {
                            gameWin = true;
                            gameOver();
                        }
                    }
                else
                {
                    timer.Start();
                    AllowClick = false;
                    //MessageBox.Show("Start");
                }

            }
        }
            #endregion

        #region Card turn delay timer


        }
        /// <summary>
        /// Turns cards back (After specified delay)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            // MessageBox.Show("Card turns back");
            
            timer.Stop();
            firstClick.Source = new BitmapImage(new Uri("Resources/Theme/" + themepath + "/cardBack.jpg", UriKind.Relative));
            secondClick.Source = new BitmapImage(new Uri("Resources/Theme/" + themepath + "/cardBack.jpg", UriKind.Relative));
            AllowClick = true;
            firstClick = null;
            secondClick = null;
            
            //    throw new NotImplementedException();
        }
        #endregion

        #region Ending
        /// <summary>
        /// will end the game (And check if won or lost)
        /// </summary>
        void gameOver()
        {
            if (gameWin == true)
            {
                MessageBox.Show("Congratulations");
            } 
        }

        #endregion

    }
}
