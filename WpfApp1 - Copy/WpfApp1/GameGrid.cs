using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace WpfApp1
{
    public class GameGrid
    {
<<<<<<< HEAD
        private Grid grid;
        
        private int colNum;
        private int rowNum;
=======
        private Grid grid;        
>>>>>>> 536aa65894f80a1fa2c2b2a90a9ab762ad1dd3f8

        public GameGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
<<<<<<< HEAD
            this.colNum = cols;
            this.rowNum = rows;
=======
            this.cols = cols;
            this.rows = rows;
>>>>>>> 536aa65894f80a1fa2c2b2a90a9ab762ad1dd3f8
            InitializeMemoryGrid(cols, rows);

        }

<<<<<<< HEAD
        
=======
>>>>>>> 536aa65894f80a1fa2c2b2a90a9ab762ad1dd3f8
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
        }
    }
    

   
}
