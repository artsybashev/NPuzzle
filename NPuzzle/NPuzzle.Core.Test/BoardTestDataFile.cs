using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace Amv.NPuzzle.Core.Test
{
    public class BoardTestDataFile
    {
        private readonly FileInfo _file;
        private readonly Lazy<BoardTestData> _data;
        private static readonly Regex _filenameRegex = new Regex(@"^puzzle(\dx\d-){0,1}(?<moves>\d{2}|unsolvable\d{0,1}).txt$");

        public BoardTestDataFile(FileInfo file)
        {
            _file = file;
            _data = new Lazy<BoardTestData>(Parse);
        }

        public BoardTestData Data => _data.Value;

        public override string ToString()
        {
            return _file.Name;
        }

        private BoardTestData Parse()
        {
            var content = File.ReadAllText(_file.FullName).Split("\n");

            var moves = GetMoves(_file.Name);

            short n = short.Parse(content[0]);

            var target = Board.GetClassicalGoal(n);

            var board = new short[n,n];
            for (int i=0; i<n;i++)
            {
                var cells = content[i+1].Replace("  ", " ").Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                for (int j=0;j<n;j++)
                {
                    board[i, j] = short.Parse(cells[j]);
                }
            }

            var source = new Board(board);

            return new BoardTestData(moves.Moves, source, target, moves.IsSolvable);
        }

        private (int Moves, bool IsSolvable) GetMoves(string fileName)
        {
            var result = _filenameRegex.Match(fileName);
            if (result.Success)
            {
                if(result.Groups["moves"].Value.StartsWith("unsolvable"))
                {
                    return (Moves:0,IsSolvable:false);
                }

                return (Moves: int.Parse(result.Groups["moves"].Value), IsSolvable: true);
            }
            
            throw new ArgumentException("Cannot parse moves from "+ fileName);
        }
    }
}