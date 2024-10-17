using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessS
{
    internal class Program
    {
        // Здесь указываем возможные ходы для коня соответсвенно по XY координатам
        static int[] motionX = { 2, 2, -2, -2, 1, 1, -1, -1 };
        static int[] motionY = { 1, -1, 1, -1, 2, -2, 2, -2 };
        static void Main(string[] args)
        {
            Console.Write("Введите координаты коня по X: ");
            int horseX = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите координаты коня по Y: ");
            int horseY = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите количество пешек(не больше 8): ");
            int pawCount = Convert.ToInt32(Console.ReadLine());

            // Координаты пешек
            var pawnsCordinate = new List<(int, int)>();

            for (int i = 0; i < pawCount; i++)
            {
                Console.Write($"Введите координаты пешки {i + 1} по X (0-7): ");
                int pawnX = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Введите координаты пешки {i + 1} по Y (0-7): ");
                int pawnY = Convert.ToInt32(Console.ReadLine());
                pawnsCordinate.Add((pawnX, pawnY));
            }

            // Количество ходов, за которое прошел конь, за исполнение
            int totalMoves = 0;

            foreach (var pawn in pawnsCordinate)
            {
                int moves = BFS(horseX, horseY, pawn.Item1, pawn.Item2);
                // Если наш умный алгоритм не смог найти пешки, то возвращается -1
                if (moves == -1)
                {
                    Console.WriteLine("Пешка недоступна.");
                    return;
                }
                // Добавление нужных ходов к итогу. Обновление позиции коня
                totalMoves += moves;
                horseX = pawn.Item1;
                horseY = pawn.Item2;
            }
            Console.WriteLine($"Общее количество ходов: {totalMoves}");

        }
        // Собственно алгоритм, называется "Поиск в ширину"
        static int BFS(int startX, int startY, int targetX, int targetY)
        {
            // Создаем очередь, в параметрах указывается позиция коня и ходы
            var queue = new Queue<(int, int, int)>();
            // visited - проверка на то, посещали ли мы эту позицию
            var visited = new bool[8, 8];
            queue.Enqueue((startX, startY, 0));
            visited[startX, startY] = true;

            // Цикл работает пока не закончатся ходы
            while (queue.Count > 0)
            {

                var (x, y, moves) = queue.Dequeue();

                if (x == targetX && y == targetY)
                {
                    return moves;
                }
                for (int i = 0; i < 8; i++)
                {
                    int newX = x + motionX[i];
                    int newY = y + motionY[i];

                    if (IsValid(newX, newY, visited))
                    {
                        visited[newX, newY] = true;
                        queue.Enqueue((newX, newY, moves + 1));
                    }
                }
            }
            return -1;
        }
        // Проверка: находится ли конь за границей доски, посещал ли он эту позицию
        static bool IsValid(int x, int y, bool[,] visited)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8 && !visited[x, y];
        }
    }
}

