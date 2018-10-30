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
        /// String that will store the imagePath of the first clicked image DEFAULT=NULL
        /// </summary>
        string firstClickImg = null;
        /// <summary>
        /// String that will store the imageId of the first clicked image DEFAULT=NULL
        /// </summary>
        string firstClickId = null;
        /// <summary>
        /// String that will store the imagePath of the second clicked image DEFAULT=NULL
        /// </summary>
        string secondClickImg = null;
        /// <summary>
        /// String that will store the imageId of the second clicked image DEFAULT=NULL
        /// </summary>
        string secondClickId = null;
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
        private void AddImages()
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
               
        public void CardClick(object sender, MouseButtonEventArgs e)
        {


            

            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
           
            if(clicked == true){
                secondClickId = card.Uid.ToString();
                secondClickImg = card.Tag.ToString();
                if (firstClickImg == secondClickImg && firstClickId != secondClickId)
                {
                    MessageBox.Show("Matched!");
                    matchedPairs++;
                    
                }
                else
                {

                    
                    

                      timer.Interval = TimeSpan.FromSeconds(1);
                     timer.Tick += timer_Tick;
                      timer.Start();
                        MessageBox.Show("Start");
                        

                      
                    
                }
                


            }
            else if (clicked == false)
            {
                firstClickId = card.Uid.ToString();
                firstClickImg = card.Tag.ToString();
                clicked = true;
             
                
            }
            

        }
        /// <summary>
        /// Turns cards back (After specified delay)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Card turns back");
           
            timer.Stop();
            firstClickImg = null;
            firstClickId = null;
            secondClickImg = null;
            secondClickId = null;
            clicked = false;
            //    throw new NotImplementedException();
        }

        


        #endregion

    }
}
