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

namespace WpfApp1
{
    public class GameGrid

    {
        #region Variables

        private string themepath = "HarryPotter";
        private Grid grid;
        private int colNum;
        private int rowNum;
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
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        #endregion

        #region Image list and placement
        private void AddImages()
        {
            List<ImageSource> imageList = GetImageList();
            for (int row = 0; row < rowNum; row++)
            {
                for (int column = 0; column < colNum; column++)
                {
                    Image backgroundImage = new Image();
                    backgroundImage.Source = new BitmapImage(new Uri("Resources/Theme/"+ themepath + "/cardBack.jpg", UriKind.Relative));
                    backgroundImage.Tag = imageList.First();
                    imageList.RemoveAt(0);
                    backgroundImage.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    grid.Children.Add(backgroundImage);
                }
            }
        }

            private List<ImageSource> GetImageList()
            {
            List<string> Stage1 = new List<string>();
            for (int i = 1; i < (colNum * rowNum / 2 + 1); i++)
            {
                string imagePath = string.Format(@"Resources/Theme/" + themepath + "/Front" + i + ".jpg");

                Stage1.Add(imagePath);
                Stage1.Add(imagePath);

            }

            Random random = new Random();
            List<ImageSource> imageList = new List<ImageSource>();
                
                for (int i = 1; i < 37; i++)
                {
                
                int rnd = random.Next(Stage1.Count);
                string source = Stage1[rnd];
                    ImageSource CardPath = new BitmapImage(new Uri(source, UriKind.Relative));
                    imageList.Add(CardPath);
                //imageList.Add(CardPath);

                Stage1.RemoveAt(rnd);

            }
           
                return imageList;
            }
        

        #endregion

        #region Mouse interaction
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;

        }

        #endregion


    }
}
