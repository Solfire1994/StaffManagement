using System;
using TaskSeven;

namespace TaskSix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string start = StartScreen();
            Repository rep = new();
            Console.Clear();
            switch (start)
            {
                case "1": Worker[] work = rep.GetAllWorkers(); Print(work); Main(args); break; //Вывод всех записей
                case "2": Worker worker = rep.GetWorkerById(); Print(worker); Main(args); break; //Вывод по ID
                case "3": rep.AddWorker(); Main(args); break; //Создать
                case "4": rep.DeleteWorker(); Main(args); break; //Удалить
                case "5": Print(rep.GetWorkersBetweenTwoDates()); Main(args); break; //Вывод в диапазоне дат рождения
                case "6": rep.EditWorker(); Main(args); break; //Редактировать
                case "7": rep.GenerateWorker(); Main(args); break; //Сгенерировать
                case "8": Worker[] sortingWorkers = rep.ViewSortingWorker(); Print(sortingWorkers); Main(args); break; //Oтсортировать записи
                case "9": Console.WriteLine("Спасибо что воспользовались программой, хорошего дня."); break; //Завершить работу
                default: Console.WriteLine("Вы ввели некоректную цифру, повторите ввод"); Main(args) ; break;
            }
        }


        /// <summary>
        /// Метод для вывода стартового диалогового окна, возвращает введенное число, для продолжения работы
        /// </summary>
        static string StartScreen()
        {
            Console.WriteLine("Приветствую, что Вы хотите сделать? Введите цифру.");
            Console.WriteLine("1. Просмотреть все записи" +
                "\n2. Просмотеть запись по номеру ID" +
                "\n3. Создать запись" +
                "\n4. Удалить запись" +
                "\n5. Вывести записи в указанном диапазоне дат рождения" +
                "\n6. Редактировать запись" +
                "\n7. Автоматическая генерация записей"+
                "\n8. Отсортировать записи" +
                "\n9. Завершить работу с модулем");

            return Console.ReadLine();            
        }

        /// <summary>
        /// Метод для вывода данных массива сотрудников
        /// </summary>
        /// <param name="allWork">Массив сотрудников</param>
        static void Print(Worker[] allWork)
        {
            // Количество символов заложенных на данные
            // Id - 3 символа
            // DateOfCreation, DayOfBirth - 24 символа
            // FullName - 35 символов
            // Age - 5 символов
            // Height - 5 символов
            // PlaceOfBirth - 20 символов
            
            foreach (Worker worker in allWork)
            {
                string div1 = new(' ', 3 - worker.Id.ToString().Length);
                string div2 = new(' ', 24 - worker.DateOfCreation.ToString().Length);
                string div3 = new(' ', 35 - worker.FullName.ToString().Length);
                string div4 = new(' ', 5 - worker.Age.ToString().Length);
                string div5 = new(' ', 5 - worker.Height.ToString().Length);
                string div6 = new(' ', 24 - worker.DayOfBirth.ToString().Length);
                Console.WriteLine(@"{0}{7}{1}{8}{2}{9}{3}{10}{4}{11}{5}{12}{6}", worker.Id, worker.DateOfCreation,
                        worker.FullName, worker.Age, worker.Height, worker.DayOfBirth, worker.PlaceOfBirth, div1,
                        div2, div3, div4, div5, div6);
            }
        }

        /// <summary>
        /// Метод для вывода данных одного сотрудника
        /// </summary>
        /// <param name="worker">Данные сотрудника</param>
        static void Print(Worker worker)
        {
            Console.WriteLine(@"{0} {1} {2} {3} {4} {5} {6}", worker.Id, worker.DateOfCreation,
                        worker.FullName, worker.Age, worker.Height, worker.DayOfBirth, worker.PlaceOfBirth);
        }
    }
}