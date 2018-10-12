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
        private Grid grid;
        private int colNum;
        private int rowNum;
        public GameGrid(Grid grid, int cols, int rows)
        {
            this.grid = grid;
            this.colNum = cols;
            this.rowNum = rows;
            InitializeMemoryGrid(cols, rows);

        }
        
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
