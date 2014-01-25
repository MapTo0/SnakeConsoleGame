using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

class Snake
{
           class Field
    {
        private int width;

        private int height;

        public int w_space;

        public int h_space;

        // The game will be made in space matrix ( that is the inner matrix - without borders)
        public char[,] space;

        public Field(int width, int height)
        {
            this.width = width;
            this.height = height;
            w_space = width - 2;
            h_space = height - 2;
            space = new char[h_space, w_space];
            for (int i = 0; i < h_space; i++)
            {
                for (int j = 0; j < w_space; j++)
                {
                    space[i, j] = ' ';
                }
            }
        }

        public void Print(int padding_left, int padding_top)
        {
            for (int i = 0; i < height; i++)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(padding_left, padding_top + i);
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        if (j == 0 || j == width - 1)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(space[i - 1, j - 1]);
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }

    private static int x = 20;

    private static int y = 20;

    const string pathScores = @".../.../scores/scores.txt";
    const string pathNames = @".../.../scores/Names.txt";
    const string pathFiles = @".../.../scores";

    private static void PrintConsole()
    {
        // height and width of the console 
        Console.WindowWidth = 100;
        Console.WindowHeight = 40;
        Console.BufferHeight = 40;
        Console.BufferWidth = 100;
    }

    // Clear part of Console Method
    private static void Clear(int x, int y, int width, int height)
    {
        int curTop = Console.CursorTop;
        int curLeft = Console.CursorLeft;
        for (; height > 0; )
        {
            Console.SetCursorPosition(x, y + --height);
            Console.Write(new string(' ', width));
        }

        Console.SetCursorPosition(curLeft, curTop);
    }

    //Menu Print Method
    private static void MenuPrint()
    {
        string[] menu = 
        {
        "NEW GAME",
        "HIGHSCORES",
        "INSTRUCTIONS",
        "EXIT"
        };
        int left = 43;
        Console.ForegroundColor = ConsoleColor.Blue;
        for (int i = 0, top = 20; i < menu.Length; i++, top += 3)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(menu[i]);
        }

        Console.BackgroundColor = ConsoleColor.Black;
    }

    //Print logo Method
    private static void ConsoleMenu()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(@"
               SSSSSSSSSSSSS                              kkkkkk 
             S:::SSSSSS::::::S                            k::::k                        
             S:::S     SSSSSSS                            k::::k                        
            S:::S         nnnnnnnnnnnn    aaaaaaaaaaaaa   k:::k    kkkkkkkeeeeeeeee     
             S::SSSS      n:::::::::::nn  aaaaaaaaa:::::a k:::k  k:::::e:::eeeee:::::ee
              S:::::SSSSS nn::::::::::::n          a::::a k:::k k:::::e:::e     e:::::e
               SS::::::::SS n:::nnnn::::n   aaaaaaa:::::a k::::k:::::ke::::eeeee::::::e
                      S:::::n::n    n:::na::::aaaa::::::a k:::::::::k e:::eeeeeeeeeee  
                      S:::::n::n    n:::a::::a    a:::::a k::::k:::::ke::::e           
            SSSSS     S:::::n::n    n:::a::::a    a:::::ak::::k k:::::e:::::e          
            S::::SSSSSS:::::n::n    n:::a:::::aaaa::::::ak::::k  k:::::e:::::eeeeeeee   
             SSSSSSSSSSSSS  nnnn    nnnnn aaaaaaaaaa  aaakkkkkk    kkkkkkkeeeeeeeeeee  ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        string str = new string('-', 90);
        Console.WriteLine("     " + str);
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 38);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Yellow;
        string dash = new string('-', 35);
        Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);
    }

    //Print Cursor Symbol
    private static void PrintCursor(int x, int y)
    {
        Clear(15, 15, 25, 20);
        MenuPrint();
        Console.SetCursorPosition(x + 9, y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(@"\_/\_/\_O.O");
    }

    private static void GameOver()
    {

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                         ____    _    __  __ _____    _____     _______ ____  
                        / ___|  / \  |  \/  | ____|  / _ \ \   / / ____|  _ \ 
                       | |  _  / _ \ | |\/| |  _|   | | | \ \ / /|  _| | |_) |
                       | |_| |/ ___ \| |  | | |___  | |_| |\ V / | |___|  _ < 
                        \____/_/   \_\_|  |_|_____|  \___/  \_/  |_____|_| \_\
");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 12);
        Console.WriteLine("YOUR POINTS");
        Console.WriteLine();
        Console.SetCursorPosition(Console.WindowWidth / 2, 14);
        Console.WriteLine(points);
        HighScores();
        Console.SetCursorPosition(0, 38);
        Console.ForegroundColor = ConsoleColor.Yellow;
        string dash = new string('-', 35);
        Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);

        Console.Beep(659, 125);
        Console.Beep(659, 125);
        Thread.Sleep(125);
        Console.Beep(659, 125);
        Thread.Sleep(167);
        Console.Beep(523, 125);
        Console.Beep(659, 125);
        Thread.Sleep(125);
        Console.Beep(784, 125);
        Thread.Sleep(375);
        Console.Beep(392, 125);
        Thread.Sleep(375);
        Console.Beep(523, 125);
        Thread.Sleep(250);
        Console.Beep(392, 125);
        Thread.Sleep(250);
        Console.Beep(330, 125);
        Thread.Sleep(250);
        Console.Beep(440, 125);
        Thread.Sleep(125);
        Console.Beep(494, 125);
        Thread.Sleep(125);
        Console.Beep(466, 125);
        Thread.Sleep(42);
        Console.Beep(440, 125);
        Thread.Sleep(125);
        Console.Beep(392, 125);
        Thread.Sleep(125);
        System.Environment.Exit(0);
    }

    struct Point
    {
        public int X;

        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    private static int points = 0;

    static List<Point> snake = new List<Point>();

    // The game 
    private static void PlayGame()
    {
        Field newField = new Field(70, 21);
        byte right = 0;
        byte left = 1;
        byte down = 2;
        byte up = 3;
        byte direction = up;
        int speed = 40000000;
        bool lastFood = false;
        Point food = new Point();
        Random randomNumbers = new Random();

        Point[] directions = new Point[]
            {
                new Point(1, 0), // right
                new Point(-1, 0), // left
                new Point(0, 1), // up
                new Point(0, -1), // down
            };

        for (int i = newField.h_space - 1; i > newField.h_space - 6; i--)
        {
            snake.Add(new Point(35, i));
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        ConsoleMenu();
        newField.Print(15, 16);


        while (true)
        {

            for (int i = 0; i < newField.h_space; i++)
            {
                for (int j = 0; j < newField.w_space; j++)
                {
                    newField.space[i, j] = ' ';
                }
            }

            Point head = snake[snake.Count - 1];
            Point nextMove = directions[direction];

            Point newHead = new Point(
                                head.X + nextMove.X,
                                head.Y + nextMove.Y);
            if (newHead.X < 0 || newHead.X > newField.w_space - 1)
            {
                GameOver();
            }

            if (newHead.Y < 0 || newHead.Y > newField.h_space - 1)
            {
                GameOver();
            }

            if (snake.Contains(newHead))
            {
                GameOver();
            }

            //feeding
            if (lastFood == false)
            {
                do
                {
                    food = new Point(randomNumbers.Next(0, newField.w_space - 1), randomNumbers.Next(0, newField.h_space - 1));
                }
                while (snake.Contains(food));
                lastFood = true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            newField.space[food.Y, food.X] = '%';
            Console.SetCursorPosition(16 + food.X, 17 + food.Y);
            Console.Write("%");
            snake.Add(newHead);
            if (newHead.Y == food.Y && newHead.X == food.X)
            {
                points++;
                if (speed > 1000000)
                {
                    speed -= 1500000;
                }

                lastFood = false;
            }
            else
            {
                Console.SetCursorPosition(16 + snake[0].X, 17 + snake[0].Y);
                Console.Write(" ");
                snake.RemoveAt(0);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < snake.Count; i++)
            {
                if (i == snake.Count - 1)
                {
                    newField.space[snake[i].Y, snake[i].X] = '@';
                    Console.SetCursorPosition(16 + snake[i].X, 17 + snake[i].Y);
                    Console.Write("@");
                }
                else
                {
                    newField.space[snake[i].Y, snake[i].X] = 'O';
                    Console.SetCursorPosition(16 + snake[i].X, 17 + snake[i].Y);
                    Console.Write("O");
                }
            }



            // Time delay
            for (int j = 0; j < speed; j++) ;

            // Check for key pressed
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();
                if (userInput.Key == ConsoleKey.LeftArrow)
                {
                    if (direction != right)
                    {
                        direction = left;
                    }

                }
                if (userInput.Key == ConsoleKey.RightArrow)
                {
                    if (direction != left)
                    {
                        direction = right;
                    }

                }

                if (userInput.Key == ConsoleKey.UpArrow)
                {
                    if (direction != down)
                    {
                        direction = up;
                    }

                }

                if (userInput.Key == ConsoleKey.DownArrow)
                {
                    if (direction != up) direction = down;
                }

            }
        }
    }

    //keeps high score
    public static void HighScores()
    {
        // Console.Clear();
        string firstName = "Null";
        string secondJName = "Null";
        string thirdName = "Null";
        int firstScore = 0;
        int secondScore = 0;
        int thirdScore = 0;
        printFooterGameOver();
        Console.SetCursorPosition(0, 0);



        //reading from files
        //-> Reading scores
        List<string> hiScores = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(pathScores))
            {
                int lineCount = 1;
                while (!sr.EndOfStream || lineCount == 3)
                {
                    hiScores.Add(sr.ReadLine());
                    lineCount++;
                }
            }

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Could not found file with scores!");
        }
        catch (FileLoadException)
        {
            Console.WriteLine("Not enough permision reading from fle scores!");
        }

        //-> Reading names
        List<string> hiNames = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(pathNames))
            {
                int lineCount = 1;
                while (!sr.EndOfStream || lineCount == 3)
                {
                    hiScores.Add(sr.ReadLine());
                    lineCount++;
                }
            }

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Could not found file with scores!");
        }
        catch (FileLoadException)
        {
            Console.WriteLine("Not enough permision reading from fle scores!");
        }

        StreamReader reader = new StreamReader(@".../.../scores/scores.txt");
        StreamReader newReader = new StreamReader(@".../.../scores/Names.txt");
        firstScore = int.Parse(reader.ReadLine());
        secondScore = int.Parse(reader.ReadLine());
        thirdScore = int.Parse(reader.ReadLine());
        firstName = newReader.ReadLine();
        secondJName = newReader.ReadLine();
        thirdName = newReader.ReadLine();
        reader.Close();
        newReader.Close();

        StreamWriter writer = new StreamWriter(@".../.../scores/scores.txt");
        StreamWriter newwriter = new StreamWriter(@".../.../scores/Names.txt");

        Console.SetCursorPosition(38, 17);
        using (writer)
        {
            if (points > firstScore)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine().PadRight(10, ' ');
                writer.WriteLine(points);
                writer.WriteLine(firstScore);
                writer.WriteLine(secondScore);
                writer.Close();
                newwriter.WriteLine(name);
                newwriter.WriteLine(firstName);
                newwriter.WriteLine(secondJName);
                newwriter.Close();
                Console.SetCursorPosition(0, 39);
                System.Environment.Exit(0);

            }
            else if (points > secondScore && points <= firstScore)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine().PadRight(10, ' ');
                writer.WriteLine(firstScore);
                writer.WriteLine(points);
                writer.WriteLine(thirdScore);
                writer.Close();
                newwriter.WriteLine(firstName);
                newwriter.WriteLine(name);
                newwriter.WriteLine(thirdName);
                newwriter.Close();
                Console.SetCursorPosition(0, 39);
                System.Environment.Exit(0);
            }
            else if (points > thirdScore && points <= secondScore && points <= firstScore)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine().PadRight(10, ' ');
                writer.WriteLine(firstScore);
                writer.WriteLine(secondScore);
                writer.WriteLine(points);
                writer.Close();
                newwriter.WriteLine(firstName);
                newwriter.WriteLine(secondJName);
                newwriter.WriteLine(name);
                newwriter.Close();
                Console.SetCursorPosition(0, 39);
                System.Environment.Exit(0);
            }
            else
            {
                writer.WriteLine(firstScore);
                writer.WriteLine(secondScore);
                writer.WriteLine(thirdScore);
                writer.Close();
                newwriter.WriteLine(firstName);
                newwriter.WriteLine(secondJName);
                newwriter.WriteLine(thirdName);
                newwriter.Close();
                Console.SetCursorPosition(0, 39);

            }
        }
    }

    static void CheckForScoresFile()
    {
        //check if files exist and if not create file with dummy values
        try
        {
            if (!File.Exists(pathScores))
            {
                if (!Directory.Exists(pathFiles)) Directory.CreateDirectory(pathFiles);

                using (StreamWriter sw = new StreamWriter(pathScores, true))
                {
                    sw.WriteLine("0");
                    sw.WriteLine("0");
                    sw.WriteLine("0");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Operation failed: \n {0}", e.ToString());
        }

    }

    static void CheckForNamesFile()
    {
        //check if files exist and if not create file with dummy values
        try
        {
            if (!File.Exists(pathNames))
            {
                if (!Directory.Exists(pathFiles)) Directory.CreateDirectory(pathFiles);
                using (StreamWriter sw = new StreamWriter(pathNames, true))
                {
                    sw.WriteLine("Player1");
                    sw.WriteLine("Player2");
                    sw.WriteLine("Player3");
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Operation failed: \n {0}", e.ToString());
        }
    }

    static void printFooterGameOver()
    {
        Console.SetCursorPosition(28, 25);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                          __
                         {OO}
                         \__/
                          |^|             Thanks for playing !            /\
                          | |____________________________________________/ /
                          \_______________________________________________/
");

        Console.SetCursorPosition(0, 38);
        Console.ForegroundColor = ConsoleColor.Yellow;
        string dash = new string('-', 35);
        Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);

    }

    static void printHighScores()
    {
        StreamReader score = new StreamReader(@".../.../scores/scores.txt");
        StreamReader names = new StreamReader(@".../.../scores/Names.txt");
        StreamWriter concat = new StreamWriter(@".../.../scores/Concat.txt");
        string scores = score.ReadLine();
        string winners = names.ReadLine().PadRight(10, ' ');
        concat.WriteLine(winners + new string(' ', 15) + scores + " " + "points");

        for (int i = 0; i < 2; i++)
        {
            scores = score.ReadLine();
            winners = names.ReadLine().PadRight(10, ' ');
            concat.WriteLine(winners + new string(' ', 15) + scores + " " + "points");
        }

        score.Close();
        names.Close();
        concat.Close();
        StreamReader allstars = new StreamReader(@".../.../scores/Concat.txt");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                             ____             _      _     _     _   
                            |  _ \ __ _ _ __ | | __ | |   (_)___| |_ 
                            | |_) / _` | '_ \| |/ / | |   | / __| __|
                            |  _ < (_| | | | |   <  | |___| \__ \ |_ 
                            |_| \_\__,_|_| |_|_|\_\ |_____|_|___/\__|
");
        Console.ForegroundColor = ConsoleColor.Blue;
        string line;
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine();
        }

        while ((line = allstars.ReadLine()) != null)
        {
            Console.WriteLine(new string(' ', 32) + line);
            Console.WriteLine();
        }

        allstars.Close();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                               _________         _________
                              /         \       /         \  
                             /  /~~~~~\  \     /  /~~~~~\  \ 
                             |  |     |  |     |  |     |  |
                             |  |     |  |     |  |     |  |
                             |  |     |  |     |  |     |  |         /
                             |  |     |  |     |  |     |  |       //
                            (o  o)    \  \_____/  /     \  \_____/ /
                             \__/      \         /       \        /
                              |         ~~~~~~~~~         ~~~~~~~~
                              ^


");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 32);
        Console.Write("---> BACK TO MENU");
        Console.SetCursorPosition(0, 38);
        Console.ForegroundColor = ConsoleColor.Yellow;
        string dash = new string('-', 35);
        Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);
        ConsoleKeyInfo backspace = new ConsoleKeyInfo();
        backspace = Console.ReadKey();
        if (backspace.Key == ConsoleKey.Backspace)
        {
            Console.Clear();
            ConsoleMenu();
            MoveCursor();
        }
        else
        {
            Console.Clear();
            ConsoleMenu();
            MoveCursor();
        }

    }


    private static void MoveCursor()
    {
        PrintCursor(x, y);
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (y < 29)
                {
                    PrintCursor(x, y += 3);
                    Console.Beep(1000, 100);
                }
                else
                {
                    PrintCursor(x, y);
                }
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                if (y > 22)
                {
                    PrintCursor(x, y -= 3);
                    Console.Beep(1000, 100);
                }
                else
                {
                    PrintCursor(x, y);
                }

            }
            if (key.Key == ConsoleKey.Enter && y == 20)
            {
                Console.Clear();
                PlayGame();
            }
            else if (key.Key == ConsoleKey.Enter && y == 23)
            {
                Console.Clear();
                printHighScores();
            }
            else if (key.Key == ConsoleKey.Enter && y == 26)
            {
                Console.Clear();
                Console.WriteLine(@"
                       ___           _                   _   _                 
                      |_ _|_ __  ___| |_ _ __ _   _  ___| |_(_) ___  _ __  ___ 
                       | || '_ \/ __| __| '__| | | |/ __| __| |/ _ \| '_ \/ __|
                       | || | | \__ \ |_| |  | |_| | (__| |_| | (_) | | | \__ \
                      |___|_| |_|___/\__|_|   \__,_|\___|\__|_|\___/|_| |_|___/
                                                                      
");
                Console.SetCursorPosition(0, 15);
                Console.ForegroundColor = ConsoleColor.Blue;
                string[] message = {
                    "Control your direction by pressing arrow keys on your keyboard.",
                    "Eat bonus to grow and get points.",
                    "Don't run into yourself.",
                    "Hurry up, you can run out of time."
                     };
                for (int i = 0; i < message.Length; i++)
                {
                    Console.Write(new string(' ', (Console.WindowWidth - message[i].Length) / 2));
                    Console.WriteLine(message[i] + "\n");
                }

                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 15);
                Console.Write("---> BACK TO MENU");
                Console.SetCursorPosition(0, 38);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string dash = new string('-', 35);
                Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);
                ConsoleKeyInfo backspace = Console.ReadKey();
                if (backspace.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    ConsoleMenu();
                    MoveCursor();
                }
                else
                {
                    Console.Clear();
                    ConsoleMenu();
                    MoveCursor();
                }

            }
            else if (key.Key == ConsoleKey.Enter && y == 29)
            {
                Console.Clear();
                Console.WriteLine(@"

    
                                        ---_ ......._-_--.
                                      (|\ /      / /| \  \
                                      /  /     .'  -=-'   `.
                                     /  /    .'             )
                                   _/  /   .'        _.)   /
                                  / o   o        _.-' /  .'
                                  \          _.-'    / .'*|
                                   \______.-'//    .'.' \*|
                                    \|  \ | //   .'.' _ |*|
                                     `   \|//  .'.'_ _ _|*|
                                      .  .// .'.' | _ _ \*|
                                      \`-|\_/ /    \ _ _ \*\
                                       `/'\__/      \ _ _ \*\
                                      /^|            \ _ _ \*
                                     '  `             \ _ _ \      

");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(@"


                            ___ _  _ ____ _  _ _  _ ____    ____ ____ ____   
                             |  |__| |__| |\ | |_/  [__     |___ |  | |__/   
                             |  |  | |  | | \| | \_ ___]    |    |__| |  \   
                                 ___  _    ____ _   _ _ _  _ ____
                                 |__] |    |__|  \_/  | |\ | | __
                                 |    |___ |  |   |   | | \| |__]
                                                                                                               
");

                Console.SetCursorPosition(0, 38);
                Console.ForegroundColor = ConsoleColor.Yellow;
                string dash = new string('-', 35);
                Console.WriteLine("     " + dash + " Team Scarlet Witch " + dash);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                System.Environment.Exit(0);
            }
            else
            {
                MoveCursor();
            }
        }
    }

    private static void Main()
    {
        Console.Title = "Snake Game by Scarlet Witch team";

        //check if files scores and Names exists
        CheckForScoresFile();
        CheckForNamesFile();

        PrintConsole();
        ConsoleMenu();
        MoveCursor();
    }
    }