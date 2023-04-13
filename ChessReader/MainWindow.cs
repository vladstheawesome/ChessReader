using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ChessReader
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawBoard();
        }
        
        private void DrawBoard()
        {
            // Clear the boardPictureBox
            pictureBox1.Controls.Clear();

            // Calculate the size of each square based on the size of the boardPictureBox
            int squareSize = pictureBox1.Width / 8;

            // Create a square for each row and column
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    // Create a new square
                    Vector2Int position = new Vector2Int(col, row);
                    Square square = new Square(position);

                    // Set the location and size of the square based on the row, column, and squareSize
                    square.Location = new Point(col * squareSize, row * squareSize);
                    square.Size = new Size(squareSize, squareSize);

                    // Set the background color of the square based on the row and column
                    Color lightSquares = ColorTranslator.FromHtml("#f0d9b5");
                    Color darkSquares = ColorTranslator.FromHtml("#b58863");
                    square.BackColor = (row + col) % 2 == 0 ? lightSquares : darkSquares;

                    // Add the square to the boardPictureBox
                    pictureBox1.Controls.Add(square);
                }
            }
        }
    }

    public class Square : PictureBox
    {
        public Vector2Int Position { get; set; }
        public Piece Piece { get; set; }

        public Square(Vector2Int position)
        {
            Position = position;
            Size = new Size(60, 60);
            Margin = new Padding(0);
            BackColor = (position.x + position.y) % 2 == 0 ? System.Drawing.Color.White : System.Drawing.Color.SaddleBrown;
            Anchor = AnchorStyles.None;
        }
    }

    public class Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum PieceColor
    {
        White,
        Black
    }

    public class Piece
    {
        public PieceType Type { get; set; }
        public PieceColor Color { get; set; }

        public Piece(PieceType type, PieceColor color)
        {
            Type = type;
            Color = color;
        }
    }

}
