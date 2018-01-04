using System;
using System.IO;
using System.Linq;

namespace Amv.NPuzzle.Core.Test
{
    public class BoardTestDataFile
    {
        private readonly FileInfo _file;
        private readonly Lazy<BoardTestData> _data;

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

            return new BoardTestData(0, source, target, true);
        }
    }
}