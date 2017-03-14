using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        
        static bool[,] cards;
        /* 0 - буби
         * 1 - черви
         * 2 - крести
         * 3 - пики
         */

        static int playerSum;
        static int computerSum;
        static Random firstParametr = new Random(4); // масть
        static Random secondParametr = new Random(13); // значение
        static int playerFirst;
        static int playerSecond;
        static int computerFirst;
        static int computerSecond;
        static string[] consoleOutput;
        static int consoleIter;

        static void Main(string[] args)
        {
            //Initialize GameLoop
            Init();

            //Getting cards for player
            consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";
            consoleIter++;
            playerFirst = GetRandomNumber(firstParametr, 4);
            playerSecond = GetRandomNumber(secondParametr, 13);
            playerSum += GetCardValue(playerSecond);
            InitNewCard(playerFirst, playerSecond); // 1 - карта игрока
            consoleIter++;

            do
            {
                playerFirst = GetRandomNumber(firstParametr, 4);
                playerSecond = GetRandomNumber(secondParametr, 13);
            } while (cards[playerFirst, playerSecond]);
            playerSum += GetCardValue(playerSecond);
            InitNewCard(playerFirst, playerSecond); // 2 - карта игрока
            consoleIter++;
            consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";

            //Getting cards for computer
            do
            {
                computerFirst = GetRandomNumber(firstParametr, 4);
                computerSecond = GetRandomNumber(secondParametr, 13);
            } while (cards[computerFirst, computerSecond]);
            computerSum += GetCardValue(computerSecond); // 3 - карта компьютера
            consoleIter++; 

            // Вывод в консоль
            WriteToConsole();

            //While Sum under 21 asking player to take the card and if he agreed, take it
            while (playerSum <= 21)
            {
                if (AskQuestion("Хотите ли вы взять еще карту?"))
                {
                    MoveComputerCards();
                    //Getting new card
                    do
                    {
                        playerFirst = GetRandomNumber(firstParametr, 4);
                        playerSecond = GetRandomNumber(secondParametr, 13);
                    } while (cards[playerFirst, playerSecond]); // If card is exist, trying to get another one

                    playerSum += GetCardValue(playerSecond);
                    consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";
                    consoleIter++;
                    WriteToConsole();
                } else
                {
                    break;
                }
            }

            //Computer logic
            //if Sum under or equals 17 take card else no
            while (computerSum <= 17)
            {
                do
                {
                    computerFirst = GetRandomNumber(firstParametr, 4);
                    computerSecond = GetRandomNumber(secondParametr, 13);
                } while (cards[computerFirst, computerSecond]);
                InitNewCard(computerFirst, computerSecond);
            }

            

            Console.Read();
        }

        static void Init()
        {
            Console.Clear();
            System.GC.Collect();
            consoleOutput = new string[100];
            consoleIter = 0;
            cards = new bool[4, 13];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards[i, j] = false;
                }
            }
        }

        static void WriteToConsole()
        {
            Console.Clear();
            for(int i = 0; i < consoleIter; i++)
            {
                Console.Write(consoleOutput[i]);
            }
        }

        //static Card GetNewCard()

        static int GetRandomNumber(Random rand, int max)
        {
            for (int i = 0; i < DateTime.Now.Millisecond; i++)
            {
                rand.Next();
            }
            return rand.Next(max);
        }

        static bool AskQuestion(string question)
        {
            while (true)
            {
                consoleOutput[consoleIter] = question + "[y/n]:";
                char answer = Convert.ToChar(Console.Read());
                if (answer == 'y' || answer == 'н')
                    return true;
                else if (answer == 'n'  || answer == 'т')
                    return false;
            }


        }

        static int GetCardValue(int cardValue)
        {

            if (cardValue == 0) // туз
                if (AskQuestion("Взять туза как единицу или одиннадцать?(y - 1; n - 11)"))
                    return 1;
                else
                    return 11;
            else if (cardValue <= 8) // от 2 до 9
                return cardValue + 1;
            else if (cardValue <= 12) // от 10 до короля
                return 10;
            else
                throw new Exception("Illegal Argument Exception");
                
        }

        static void MoveComputerCards()
        {
            string buf = consoleOutput[consoleIter - 1];
            consoleOutput[consoleIter] = buf;
        }

        static void InitNewCard(int first, int second)
        {
            string buildString = "";
            switch (second)
            {
                case 0:
                    buildString += "Туз";
                    break;
                case 1:
                    buildString += "Двойка";
                    break;
                case 2:
                    buildString += "Тройка";
                    break;
                case 3:
                    buildString += "Четверка";
                    break;
                case 4:
                    buildString += "Пятерка";
                    break;
                case 5:
                    buildString += "Шестерка";
                    break;
                case 6:
                    buildString += "Семерка";
                    break;
                case 7:
                    buildString += "Восьмерка";
                    break;
                case 8:
                    buildString += "Девятка";
                    break;
                case 9:
                    buildString += "Десятка";
                    break;
                case 10:
                    buildString += "Валет";
                    break;
                case 11:
                    buildString += "Дама";
                    break;
                case 12:
                    buildString += "Король";
                    break;
                default:
                    throw new Exception("Unknown exception");
                    break;
            }

            switch(first)
            {
                case 0:
                    buildString += " буби\n";
                    break;
                case 1:
                    buildString += " черви\n";
                    break;
                case 2:
                    buildString += " крести\n";
                    break;
                case 3:
                    buildString += " пики\n";
                    break;
                default:
                    throw new Exception("Unknown index");
            }
            cards[first, second] = true;

            consoleOutput[consoleIter] = buildString;
        }
    }
}
