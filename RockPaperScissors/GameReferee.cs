using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    public class GameReferee
    {
        public string[] Moves { get; set; }

        private byte[] HmacKey { get; set; } = new byte[64];

        public GameReferee()
        {
            RandomNumberGenerator.Create().GetBytes(HmacKey);
        }

        public void PlayGame()
        {
            do
            {
                var computerMoveIndex = GenerateRandomActionIndex();
                var computerMove = Moves[computerMoveIndex];
                var hmac = GetHexHmac(computerMove);
                Console.WriteLine($"HEX HMAC: {hmac}");

                var playerMoveIndex = GetPlayerMove();
                if (playerMoveIndex == 0)
                {
                    Console.WriteLine("Exit");
                    break;
                }

                playerMoveIndex--;
                var playerMove = Moves[playerMoveIndex];
                Console.WriteLine("Your move " + playerMove);
                Console.WriteLine("Computer move " + computerMove);

                if (playerMoveIndex == computerMoveIndex)
                {
                    Console.WriteLine("Draw\n");
                    continue;
                }

                Console.WriteLine("You " + (IsWin(playerMoveIndex, computerMoveIndex) ? "Win!" : "Lose!"));
                Console.WriteLine($"HEX Key: {HashEncode(HmacKey)}");
                break;
            } while (true);
        }

        private int GetPlayerMove()
        {
            bool isCorrect;
            int result;

            do
            {
                LogAvailableMoves();
                Console.Write("Enter your move: ");
                var answer = Console.ReadLine();
                isCorrect = int.TryParse(answer, out result) && result >= 0 && result <= Moves.Length;
            } while (!isCorrect);

            return result;
        }

        private void LogAvailableMoves()
        {
            Console.WriteLine("\nAvailable moves:");
            for (int i = 0; i < Moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {Moves[i]}");
            }

            Console.WriteLine("0 - exit");
        }

        private bool IsWin(int playerMoveIndex, int computerMoveIndex)
        {
            var length = Moves.Length;
            var halfOfLength = (length - 1) / 2;

            var startIndex = computerMoveIndex + 1;
            var endIndex = computerMoveIndex + halfOfLength;
            var endIndexIfOutOfLength = endIndex - length;

            return playerMoveIndex >= startIndex && playerMoveIndex <= endIndex
                   || playerMoveIndex <= endIndexIfOutOfLength;
        }

        private string GetHexHmac(string data)
        {
            using var hmac256 = new HMACSHA256(HmacKey);
            var hash = hmac256.ComputeHash(StringDecode(data));
            return HashEncode(hash);
        }

        private string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }

        private byte[] StringDecode(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        private int GenerateRandomActionIndex()
        {
            var random = new Random();
            return random.Next(Moves.Length);
        }
    }
}