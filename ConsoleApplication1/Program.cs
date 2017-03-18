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
        static bool playerWin;
        static bool wannaPlay = true;
        static long playerAmount = 1000;
        static long playerStake;

        static void Main(string[] args)
        {
            while (wannaPlay)
            {
                //Initialize GameLoop
                playerStake = AskForStake();
                Init();

                //Getting cards for player
                consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";
                consoleIter++;
                playerFirst = GetRandomNumber(firstParametr, 4);
                playerSecond = GetRandomNumber(secondParametr, 13);
                playerSum += GetCardValue(playerSecond, true);
                InitNewCard(playerFirst, playerSecond); // 1 - карта игрока
                consoleIter++;

                do
                {
                    playerFirst = GetRandomNumber(firstParametr, 4);
                    playerSecond = GetRandomNumber(secondParametr, 13);
                } while (cards[playerFirst, playerSecond]);
                playerSum += GetCardValue(playerSecond, true);
                InitNewCard(playerFirst, playerSecond); // 2 - карта игрока
                consoleIter++;
                consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";

                // Вывод в консоль
                WriteToConsole();

                //While Sum under 21 asking player to take the card and if he agreed, take it
                while (playerSum <= 21)
                {
                    if (AskQuestion("Хотите ли вы взять еще карту?"))
                    {
                        //MoveConsole();
                        //Getting new card
                        do
                        {
                            playerFirst = GetRandomNumber(firstParametr, 4);
                            playerSecond = GetRandomNumber(secondParametr, 13);
                        } while (cards[playerFirst, playerSecond]); // If card is exist, trying to get another one

                        playerSum += GetCardValue(playerSecond, true);
                        consoleOutput[0] = "Сумма ваших карт: " + Convert.ToString(playerSum) + "\n";
                        InitNewCard(playerFirst, playerSecond);
                        consoleIter++;
                        WriteToConsole();
                        //MoveConsole();
                    }
                    else
                    {
                        break;
                    }
                }
                WriteToConsole();
                if (playerSum <= 21)
                {
                    //Computer logic
                    //if Sum under or equals 17 take card else no
                    int indexOfComputerSum = consoleIter + 1;
                    while (computerSum <= 17)
                    {
                        System.Threading.Thread.Sleep(1000);
                        do
                        {
                            computerFirst = GetRandomNumber(firstParametr, 4);
                            computerSecond = GetRandomNumber(secondParametr, 13);
                        } while (cards[computerFirst, computerSecond]);
                        computerSum += GetCardValue(computerSecond, false);
                        InitNewCard(computerFirst, computerSecond);
                        consoleOutput[indexOfComputerSum] = "Сумма карт компьютера: " + Convert.ToString(computerSum) + "\n";
                        consoleIter++;
                        WriteToConsole();
                    }
                }
                else
                {
                    playerWin = false;
                    consoleIter++;
                    consoleOutput[consoleIter] = "Вы проиграли! Перебор! (Нужно набрать 21 и менее очков, чтобы выиграть)\n";
                    playerAmount -= playerStake;
                    WriteToConsole();
                }

                if ((computerSum <= playerSum || computerSum > 21) && playerWin)
                {
                    consoleIter++;
                    consoleOutput[consoleIter] = "Вы победили!\n";
                    playerAmount += playerStake;
                    WriteToConsole();
                }
                else
                {
                    if (playerWin)
                    {
                        consoleIter++;
                        consoleOutput[consoleIter] = "Вы проиграли! Компьютер оказался ближе к 21 чем вы!\n";
                        playerAmount -= playerStake;
                        WriteToConsole();
                    }
                }


                consoleIter++;
                consoleOutput[consoleIter] = "Хотите сыграть еще раз?[y/n]:";
                WriteToConsole();
                try
                {
                    char stop = char.Parse(Console.ReadLine());
                    if (stop == 'n' || stop == 'т')
                        wannaPlay = false;
                }
                catch (Exception e)
                {
                    //doNothing
                }
            }
        }

        static void Init()
        {
            Console.Clear();
            System.GC.Collect();
            playerSum = 0;
            computerSum = 0;
            consoleOutput = new string[100];
            playerWin = true;
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

        static long AskForStake()
        {
            consoleOutput = new string[100];
            while (true)
            {
                consoleOutput[0] = "У вас осталось " + Convert.ToString(playerAmount) + "\n";
                consoleOutput[1] = "Введите вашу ставку: ";
                consoleIter = 5;
                WriteToConsole();
                try
                {
                    long toReturn = long.Parse(Console.ReadLine());
                    if (toReturn > playerAmount || toReturn <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return toReturn;
                    }
                } 
                catch (Exception e)
                {

                }
            }

        }

        static void WriteToConsole()
        {
            Console.Clear();
            for(int i = 0; i <= consoleIter; i++)
            {

                Console.Write(consoleOutput[i]);
            }
        }

        //static Card GetNewCard()

        static int GetRandomNumber(Random rand, int max) // Rendomizing new card
        {
            for (int i = 0; i < DateTime.Now.Millisecond; i++)
            {
                rand.Next();
            }
            return rand.Next(max);
        }

        static bool AskQuestion(string question)
        {
            
            consoleIter++;
            consoleOutput[consoleIter] = question + "[y/n]:";
            WriteToConsole();
//            consoleIter--; comment because it doesn't work for ace
            char answer = char.Parse(Console.ReadLine());
            consoleOutput[consoleIter] += "\n";
            while (true)
            {
                if (answer == 'y' || answer == 'н')
                {
                    consoleOutput[consoleIter] = "";
                    return true;
                }
                else if (answer == 'n' || answer == 'т')
                    consoleOutput[consoleIter] = "";
                    return false;
            }

        }

        static int GetCardValue(int cardValue, bool isPlayer)
        {

            if (cardValue == 0 && isPlayer) // туз
            {
                consoleIter++;
                if (AskQuestion("Взять туза как единицу или одиннадцать?(y - 1; n - 11)"))
                    return 1;
                else
                    return 11;
            }
            else if (cardValue == 0 && !isPlayer)
            {
                if (computerSum >= 3 && computerSum <= 10)
                    return 11;
                else
                    return 1;
            }
            else if (cardValue <= 8) // от 2 до 9
                return cardValue + 1;
            else if (cardValue <= 12) // от 10 до короля
                return 10;
            else
                throw new Exception("Illegal Argument Exception");
                
        }

        static void MoveConsole()
        {
            string buf = consoleOutput[consoleIter];
            consoleOutput[consoleIter + 1] = buf;
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
            consoleIter += 2;
            consoleOutput[consoleIter] = buildString;
        }
    }
}
