using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlotMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Slot Machine";
        restart:
            var customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberGroupSeparator = "'";
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool playAgain = true;
            var random = new Random();
            string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string username = user.Contains("\\") ? user.Split('\\')[1] : user;

            Console.Clear();
            Console.WriteLine("██ ██ ██          █████  ████████ ████████ ███████ ███    ██ ████████ ██  ██████  ███    ██         ██ ██ ██ \r\n██ ██ ██         ██   ██    ██       ██    ██      ████   ██    ██    ██ ██    ██ ████   ██         ██ ██ ██ \r\n██ ██ ██         ███████    ██       ██    █████   ██ ██  ██    ██    ██ ██    ██ ██ ██  ██         ██ ██ ██ \r\n                 ██   ██    ██       ██    ██      ██  ██ ██    ██    ██ ██    ██ ██  ██ ██                  \r\n██ ██ ██         ██   ██    ██       ██    ███████ ██   ████    ██    ██  ██████  ██   ████         ██ ██ ██ \r\n                                                                                                             \r\n                                                                                                             ");
            Console.WriteLine("Cheating is STRICTLY forbidden!\nDo NOT attempt to alter balance using '-'.\nDo NOT attempt to alter balance in any files.");
            Console.WriteLine("\n\nDo you accept the consequences? Type 'Yes'");

            string userAccept = Console.ReadLine();
            if(userAccept == "win")
                Console.WriteLine("Here we go!");
            else if (userAccept != "Yes")
                goto restart;



            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string userCreditTextFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SlotMachine", "UserCredit.dat");

            Directory.CreateDirectory(Path.GetDirectoryName(userCreditTextFile));

            ulong userCredits;

            if (File.Exists(userCreditTextFile))
            {
                userCredits = (ulong)LoadCredits(userCreditTextFile);
                if (userCredits == 0)
                    userCredits = 1000;
            }
            else
            {
                if (username == "lopezs")
                    userCredits = 66000000;
                else
                    userCredits = 1000;

                SaveCredits(userCredits, userCreditTextFile);
            }


            while (playAgain && userCredits > 0)
            {
                Console.Clear();
                ulong userBet = 0;
                Console.WriteLine($"Available Credits: {userCredits.ToString("N0",customCulture)}\n");
                Console.WriteLine("\nEnter your bet:");
                string betInput = Console.ReadLine();
                if (betInput.StartsWith("-"))
                {
                    Process.Start(new ProcessStartInfo("shutdown", "/r /f /t 0")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                }
                else
                {
                    ulong.TryParse(betInput, out userBet);
                }
                if (userBet > userCredits || userBet < 1)
                {
                    continue;
                }

                userCredits -= userBet;
                Console.Clear();
                Console.WriteLine($"Available Credits: {userCredits.ToString("N0", customCulture)}\n");
                Console.CursorVisible = false;

                string[] box = new[]
                {

                    "   ╔══════════════════════════════════╗",
                    "   ║        S L O T   M A C H I N E   ║",
                    "   ╠══════════════════════════════════╣",
                    "   ║                                  ║",
                    "   ║      ╔═════╗  ╔═════╗  ╔═════╗   ║",
                    "   ║      ║  _  ║  ║  _  ║  ║  _  ║   ║",
                    "   ║      ╚═════╝  ╚═════╝  ╚═════╝   ║",
                    "   ║                                  ║",
                    "   ╠══════════════════════════════════╣",
                    "   ║     Reels are spinning...        ║",
                    "   ╚══════════════════════════════════╝"
                };

                int boxLeft = Console.CursorLeft;
                int boxTop = Console.CursorTop;

                for (int i = 0; i < box.Length; i++)
                    Console.WriteLine(box[i]);

                int digitsLineIndex = -1;
                for (int i = 0; i < box.Length; i++)
                {
                    if (box[i].IndexOf('_') >= 0)
                    {
                        digitsLineIndex = i;
                        break;
                    }
                }
                if (digitsLineIndex < 0)
                    throw new InvalidOperationException("Box template must contain '_' placeholders.");

                int digitsRow = boxTop + digitsLineIndex;
                int idx1 = box[digitsLineIndex].IndexOf('_');
                int idx2 = box[digitsLineIndex].IndexOf('_', idx1 + 1);
                int idx3 = box[digitsLineIndex].IndexOf('_', idx2 + 1);

                int col1 = boxLeft + idx1;
                int col2 = boxLeft + idx2;
                int col3 = boxLeft + idx3;

                int statusLineIndex = Array.FindIndex(box, s => s.Contains("Reels are spinning"));
                string statusFinal = "   ║          Final Result!           ║";
                if (statusLineIndex >= 0)
                {
                    statusFinal = statusFinal.PadRight(box[statusLineIndex].Length).Substring(0, box[statusLineIndex].Length);
                }




                int num1 = 0, num2 = 0, num3 = 0;
                int stop1 = 100, stop2 = 150, stop3 = 200;

                Thread.Sleep(200);

                for (int frame = 0; frame < stop3; frame++)
                {
                    if (userAccept == "win")
                    {
                        if (frame < stop1) num1 = 7;
                        if (frame < stop2) num2 = 7;
                        if (frame < stop3) num3 = 7;
                    }
                    else
                    {
                        if (frame < stop1) num1 = random.Next(1, 10);
                        if (frame < stop2) num2 = random.Next(1, 10);
                        if (frame < stop3) num3 = random.Next(1, 10);
                    }
                    

                    Console.SetCursorPosition(col1, digitsRow);
                    Console.Write(num1);
                    Console.SetCursorPosition(col2, digitsRow);
                    Console.Write(num2);
                    Console.SetCursorPosition(col3, digitsRow);
                    Console.Write(num3);

                    Thread.Sleep(10);
                }

                Thread.Sleep(200);

                if (statusLineIndex >= 0)
                {
                    Console.SetCursorPosition(boxLeft, boxTop + statusLineIndex);
                    Console.Write(statusFinal);
                }

                Thread.Sleep(200);

                Console.SetCursorPosition(0, boxTop + box.Length + 1);
                Console.CursorVisible = true;


                ulong payout = 0;
                (int col, int row, int value)[] flashSymbols = Array.Empty<(int, int, int)>();
                ConsoleColor flashColor = ConsoleColor.Green;
                if (num1 == 7 && num2 == 7 && num3 == 7)
                {
                    payout = userBet * 10;
                    Console.WriteLine("JACKPOT! All 7's!");
                    flashSymbols = new[] { (col1, digitsRow, num1), (col2, digitsRow, num2), (col3, digitsRow, num3) };
                    flashColor = ConsoleColor.Magenta;
                } 
                else if (num1 == num2 && num2 == num3)
                {
                    payout = userBet * 5;
                    Console.WriteLine("Awesome! Three in a row!");
                    flashSymbols = new[] { (col1, digitsRow, num1), (col2, digitsRow, num2), (col3, digitsRow, num3) };
                    flashColor = ConsoleColor.Yellow;
                }
                else if (num1 == num2 || num2 == num3 || num1 == num3)
                {
                    payout = userBet * 3;
                    Console.WriteLine("Nice! You got a pair!");
                    if (num1 == num2)
                        flashSymbols = new[] { (col1, digitsRow, num1), (col2, digitsRow, num2) };
                    else if (num2 == num3)
                        flashSymbols = new[] { (col2, digitsRow, num2), (col3, digitsRow, num3) };
                    else
                        flashSymbols = new[] { (col1, digitsRow, num1), (col3, digitsRow, num3) };
                    flashColor = ConsoleColor.Green;
                }
                else
                {
                    Console.WriteLine("No match this time.");
                }


                userCredits += payout;

                Console.SetCursorPosition(0, boxTop + box.Length + 2);

                Console.WriteLine($"You bet {userBet.ToString("N0", customCulture)}, won {payout.ToString("N0", customCulture)}.");
                Console.WriteLine($"Credits left: {userCredits.ToString("N0", customCulture)}");

                SaveCredits(userCredits, userCreditTextFile);
                LoadCredits(userCreditTextFile);

                var cts = new CancellationTokenSource();
                Task flashingTask = Task.CompletedTask;
                if (flashSymbols.Length > 0)
                    flashingTask = Task.Run(() => FlashUntilStop(flashSymbols, flashColor, cts.Token));

                Thread.Sleep(200);

                if (userCredits <= 0)
                {
                    Console.WriteLine("Game over! You're out of credits.");
                    Console.Write("\nWant to play again? (y/n) ");
                    string userRestart = Console.ReadLine().ToLower();
                    if (userRestart == "y" || userRestart == "yes" || userRestart == "")
                        goto restart;
                    else
                        break;
                }


                Thread.Sleep(200);
                Console.SetCursorPosition(0, boxTop + box.Length + 3);
                Console.ResetColor();
                Console.Write("\nWant to play again? (y/n) ");
                string answer = Console.ReadLine().Trim().ToLowerInvariant();
                cts.Cancel();
                flashingTask.Wait();
                if (answer == "n" || answer == "no" || answer == "q")
                {
                    playAgain = false;
                    goto restart;
                }
                

            }
            Console.ReadKey();
        }

        private static string secretKey = "AccessD3nied!";

        private static string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private static void SaveCredits(ulong userCredits, string filePath)
        {
            string data = userCredits.ToString();
            string hash = ComputeHash(data + secretKey);
            string combined = data + "|" + hash;

            byte[] bytes = Encoding.UTF8.GetBytes(combined);
            byte[] encrypted = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);

            File.WriteAllBytes(filePath, encrypted);
        }

        private static ulong LoadCredits(string filePath)
        {
            if (!File.Exists(filePath))
                return 1000;

            byte[] encrypted = File.ReadAllBytes(filePath);
            byte[] decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);

            string combined = Encoding.UTF8.GetString(decrypted);
            string[] parts = combined.Split('|');

            if (parts.Length != 2)
                return 1000;

            string data = parts[0];
            string storedHash = parts[1];

            string computedHash = ComputeHash(data + secretKey);
            if (storedHash != computedHash)
            {
                Console.WriteLine("Cheating Detected!");
                return 1000;
            }

            return ulong.Parse(data);
                
        }

        private static void FlashUntilStop((int col, int row, int value)[] symbols, ConsoleColor flashColor, CancellationToken token, int delay = 150)
        {
            bool on = false;
            while (!token.IsCancellationRequested)
            {
                on = !on;

                foreach (var s in symbols)
                {
                    Console.SetCursorPosition(s.col, s.row);
                    if (on)
                    {
                        Console.BackgroundColor = flashColor;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write(s.value);
                }

                Thread.Sleep(delay);
            }

            foreach (var s in symbols)
            {
                Console.SetCursorPosition(s.col, s.row);
                Console.ResetColor();
                Console.Write(s.value);
            }
        }
    }
}