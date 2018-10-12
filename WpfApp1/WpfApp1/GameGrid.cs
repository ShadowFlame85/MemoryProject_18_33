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
        public Grid grid;
        public GameGrid(GameGrid grid, int cols, int rows)
        {
            //this.grid = grid;
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
