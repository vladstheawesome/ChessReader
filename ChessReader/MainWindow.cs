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
        private readonly Board _board;
        private readonly int _squareSize = 150;

        public MainWindow()
        {
            InitializeComponent();
            _board = new Board();
            LoadChessPieces();
            DrawBoard();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the position of the mouse pointer relative to the PictureBox control
            int x = e.X / _squareSize;
            int y = e.Y / _squareSize;

            // TODO: Perform some action based on the mouse position
        }

        private void LoadChessPieces()
        {
            foreach (var piece in _board.Pieces)
            {                
                string colorName = piece.Color.ToString().ToLower();
                string typeName = piece.Type.ToString().ToLower();
                string pieceImagePath = $"images/pieces/{colorName}_{typeName}.png";
                try
                {
                    using (var fs = new FileStream(pieceImagePath, FileMode.Open, FileAccess.Read))
                    {
                        piece.Image = Image.FromStream(fs);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image '{pieceImagePath}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Debug code to check the color of each piece
                Console.WriteLine($"{piece.Color} {piece.Type} {piece.Image != null}");
            }
        }

        private void DrawBoard()
        {
            // Load the board background image
            var boardImage = Image.FromFile("images/board/150.png");

            // Create a new bitmap with the same size as the board image
            var boardBitmap = new Bitmap(boardImage.Width, boardImage.Height);

            // Draw the board image onto the bitmap
            var g = Graphics.FromImage(boardBitmap);
            g.DrawImage(boardImage, 0, 0);

            // Draw the black pieces onto the bitmap
            foreach (var piece in _board.Pieces.Where(p => p.Color == Color.Black).Reverse())
            {
                //var pieceImage = Image.FromFile($"images/pieces/{piece.Color}{piece.GetType().Name}.png");
                string colorName = piece.Color.ToString().ToLower();
                string typeName = piece.Type.ToString().ToLower();
                string pieceImagePath = $"images/pieces/{colorName}_{typeName}.png";
                var pieceImage = Image.FromFile(pieceImagePath);
                g.DrawImage(pieceImage, piece.Column * _squareSize, piece.Row * _squareSize);
            }

            // Draw the white pieces onto the bitmap
            foreach (var piece in _board.Pieces.Where(p => p.Color == Color.White).Reverse())
            {
                //var pieceImage = Image.FromFile($"images/pieces/{piece.Color}{piece.GetType().Name}.png");
                string colorName = piece.Color.ToString().ToLower();
                string typeName = piece.Type.ToString().ToLower();
                string pieceImagePath = $"images/pieces/{colorName}_{typeName}.png";
                var pieceImage = Image.FromFile(pieceImagePath);
                g.DrawImage(pieceImage, piece.Column * _squareSize, piece.Row * _squareSize);
            }

            // Set the bitmap as the image for the PictureBox control
            pictureBox1.Image = boardBitmap;
        }
    }

    public class Board
    {
        public List<Piece> Pieces { get; set; }

        public Board()
        {
            Pieces = new List<Piece>();

            // Add white pieces
            Pieces.Add(new Piece(Color.White, PieceType.Rook, 7, 0));
            Pieces.Add(new Piece(Color.White, PieceType.Knight, 7, 1));
            Pieces.Add(new Piece(Color.White, PieceType.Bishop, 7, 2));
            Pieces.Add(new Piece(Color.White, PieceType.Queen, 7, 3));
            Pieces.Add(new Piece(Color.White, PieceType.King, 7, 4));
            Pieces.Add(new Piece(Color.White, PieceType.Bishop, 7, 5));
            Pieces.Add(new Piece(Color.White, PieceType.Knight, 7, 6));
            Pieces.Add(new Piece(Color.White, PieceType.Rook, 7, 7));
            for (int i = 0; i < 8; i++)
            {
                Pieces.Add(new Piece(Color.White, PieceType.Pawn, 6, i));
            }

            // Add black pieces
            Pieces.Add(new Piece(Color.Black, PieceType.Rook, 0, 0));
            Pieces.Add(new Piece(Color.Black, PieceType.Knight, 0, 1));
            Pieces.Add(new Piece(Color.Black, PieceType.Bishop, 0, 2));
            Pieces.Add(new Piece(Color.Black, PieceType.Queen, 0, 3));
            Pieces.Add(new Piece(Color.Black, PieceType.King, 0, 4));
            Pieces.Add(new Piece(Color.Black, PieceType.Bishop, 0, 5));
            Pieces.Add(new Piece(Color.Black, PieceType.Knight, 0, 6));
            Pieces.Add(new Piece(Color.Black, PieceType.Rook, 0, 7));

            for (int i = 0; i < 8; i++)
            {
                Pieces.Add(new Piece(Color.Black, PieceType.Pawn, 1, i));
            }
        }
    }

    public class Piece
    {
        public Piece(Color color, PieceType type, int row, int column)
        {
            if (!Enum.IsDefined(typeof(Color), color))
                throw new ArgumentException($"Invalid color value: {color}");

            if (!Enum.IsDefined(typeof(PieceType), type))
                throw new ArgumentException($"Invalid piece type value: {type}");

            Color = color;
            Type = type;
            Row = row;
            Column = column;
        }

        public Color Color { get; set; }
        public PieceType Type { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Image Image { get; set; }
    }

    public enum Color
    {
        White,
        Black
    }

    public enum PieceType
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn
    }

}
