using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSeven;

namespace TaskSeven
{
    struct Repository
    {
        private const string path = @"Text.txt";
        /// <summary>
        /// Метод по получению списка всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников с данными</returns>
        public Worker[] GetAllWorkers()
        {
            using StreamReader sr = new(path);
            string[] result;
            Worker[] work = new Worker[20];
            string line = sr.ReadLine();
            Worker[] workResult = work;
            if (line == null) Console.WriteLine("Список сотрудников пуст");
            else
            {
                int k = 0;
                while (line != null)
                {
                    if(k >= work.Length) Array.Resize(ref work, work.Length * 2);
                    result = line.Split("#");
                    work[k] = new Worker(int.Parse(result[0]), DateTime.Parse(result[1]), result[2], 
                        byte.Parse(result[3]), byte.Parse(result[4]), DateTime.Parse(result[5]), result[6]);
                    k++;
                    line = sr.ReadLine();
                }
                workResult = new Worker[k];
                for (int i = 0; i < k; i++)
                {
                    workResult[i] = work[i];
                }
            }
            
            return workResult;
        }
        /// <summary>
        /// Метод получения сотрудника по введенному ИД
        /// </summary>
        /// <returns>Данные сотрудника</returns>
        public Worker GetWorkerById()
        {
            Console.WriteLine("Введите ID сотрудника, данные которого вы хотите найти");
            int id = int.Parse(Console.ReadLine());
            Worker[] work = GetAllWorkers();
            int count = -1;
            for (int i = 0; i < work.Length; i++)
            {
                if (work[i].Id == id)
                {
                    count = i; 
                    break;
                }
            }
            if (count != -1) return work[count]; 
            else
            {                
                Console.WriteLine("Сотрудник с таким ID не существует");
                return GetWorkerById();
            }
        }
        /// <summary>
        /// Удаление сотрудникка по введненному ИД
        /// </summary>
        public void DeleteWorker()
        {
            Worker worker = GetWorkerById();
            Console.WriteLine("Вы действительно хотите удалить данные сотрудника д/н");
            string str = Console.ReadLine();
            Console.Clear();
            if (str == "д")
            {
                Worker[] work = GetAllWorkers();
                Worker[] result = new Worker[work.Length - 1];
                int k = 0;
                for (int i = 0;i < work.Length; i++)
                {
                    if(work[i].Id != worker.Id)
                    {
                        result[k] = work[i];
                        k++;
                    }
                }
                PrintInFile(result);
            }
            else Console.WriteLine("Данные не были удалены");
        }
        /// <summary>
        /// Добавление сотрудника с присвоением уникального ИД
        /// </summary>
        public void AddWorker()
        {
            Worker[] work = GetAllWorkers();
            Worker[] result = new Worker[work.Length + 1];
            for (int i = 0; i < work.Length; i++) result[i] = work[i];
            Worker newWorker = new();
            newWorker.Id = work[^1].Id + 1;
            newWorker.DateOfCreation = DateTime.Now;
            Console.WriteLine("Введите ФИО сотрудника");
            newWorker.FullName = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника");
            newWorker.Age = byte.Parse(Console.ReadLine());
            Console.WriteLine("Введите рост сотрудника");
            newWorker.Height = byte.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату рождения сотрудника в пормате ДД.ММ.ГГГГ");
            newWorker.DayOfBirth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите место рождения сотрудника");
            newWorker.PlaceOfBirth = Console.ReadLine();
            result[result.Length - 1] = newWorker;
            PrintInFile(result);
        }
        /// <summary>
        /// Редактирование данных сотрудника по введенному ИД
        /// </summary>
        public void EditWorker()
        {
            Worker worker = GetWorkerById();
            Console.WriteLine("Какой параметр сотрудника Вы хотите изменить? Введите цифру.");
            Console.WriteLine("1. ФИО" + "\n2. Возраст" + "\n3. Рост" + "\n4. Дату рождения" + 
                "\n5. Место рождения" + "\n6. Изменить все параметры");
            string editor = Console.ReadLine();
            Console.Clear();
            switch (editor)
            {
                case "1": Console.WriteLine("Напишите новое имя"); worker.FullName = Console.ReadLine(); break;
                case "2": Console.WriteLine("Напишите новый возраст"); worker.Age = byte.Parse(Console.ReadLine()); break;
                case "3": Console.WriteLine("Напишите новый рост"); worker.Height = byte.Parse(Console.ReadLine()); break;
                case "4": Console.WriteLine("Напишите новую дату рождения"); worker.DayOfBirth = DateTime.Parse(Console.ReadLine()); break;
                case "5": Console.WriteLine("Напишите новое место рождения"); worker.PlaceOfBirth = Console.ReadLine(); break;
                case "6":
                    Console.WriteLine("Напишите новые имя, возраст, рост, дату рождения и место рождения");
                    worker.FullName = Console.ReadLine();
                    worker.Age = byte.Parse(Console.ReadLine());
                    worker.Height = byte.Parse(Console.ReadLine());
                    worker.DayOfBirth = DateTime.Parse(Console.ReadLine());
                    worker.PlaceOfBirth = Console.ReadLine();
                    break;
                default: Console.WriteLine("Вы ввели некоректную цифру, повторите ввод"); EditWorker(); break;
            }
            worker.DateOfCreation = DateTime.Now;
            Worker[] allWorkers = GetAllWorkers();
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (worker.Id == allWorkers[i].Id) allWorkers[i] = worker;
            }
            PrintInFile(allWorkers);
        }
        /// <summary>
        /// Метод по созданию случайных сотрудников
        /// </summary>
        public void GenerateWorker()
        {
            Worker[] oldWorkers = GetAllWorkers();
            Console.WriteLine("Напишите количество сотрудников, которых Вы хотите автоматически создать");
            byte newCount = byte.Parse(Console.ReadLine());
            Worker[] newWorkers = new Worker[newCount];
            for (int i = 0;i < newCount; i++)
            {
                newWorkers[i].Id = oldWorkers[^1].Id + 1 + i;
                newWorkers[i].DateOfCreation = DateTime.Now;
                newWorkers[i].FullName = "Фамилия_" + i + " Имя_" + i + " Отчество_" + i;
                newWorkers[i].Age = 99;
                newWorkers[i].Height = 199;
                newWorkers[i].DayOfBirth = new DateTime(1000, 1, 1);
                newWorkers[i].PlaceOfBirth = "город_" + i;
            }
            Worker[] allWorkers = new Worker[oldWorkers.Length + newWorkers.Length];
            int k = 0;
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (i < oldWorkers.Length) allWorkers[i] = oldWorkers[i];
                else
                {
                    allWorkers[i] = newWorkers[k];
                    k++;
                }
            }
            PrintInFile(allWorkers);
        }
        /// <summary>
        /// Метод сортировки списка сотрудников по указанному полю, без внесения изменений в файл
        /// </summary>
        public Worker[] ViewSortingWorker()
        {
            Worker[] allWorkers = GetAllWorkers();
            Console.WriteLine("Укажите по какому полю вы хотите отсортировать записи");
            Console.WriteLine("1. ФИО" + "\n2. Возраст" + "\n3. Рост" + "\n4. Дата рождения" + "\n5. Место рождения");
            string editor = Console.ReadLine();
            Console.Clear();
            Worker[] sortedWork;
            switch (editor)
            {
                case "1": sortedWork = allWorkers.OrderByDescending(w => w.FullName).ToArray(); break;
                case "2": sortedWork = allWorkers.OrderByDescending(w => w.Age).ToArray(); break;
                case "3": sortedWork = allWorkers.OrderByDescending(w => w.Height).ToArray(); break;
                case "4": sortedWork = allWorkers.OrderByDescending(w => w.DayOfBirth).ToArray(); break;
                case "5": sortedWork = allWorkers.OrderByDescending(w => w.PlaceOfBirth).ToArray(); break;
                default: Console.WriteLine("Вы ввели некоректную цифру, повторите ввод"); return ViewSortingWorker();
            }

            return sortedWork;
        }
        /// <summary>
        /// Метод вывода сотрудников в указанном диапазоне дат рождения
        /// </summary>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenTwoDates()
        {
            Worker[] allWorkers = GetAllWorkers();
            allWorkers = allWorkers.OrderByDescending(w => w.DayOfBirth).ToArray();
            Console.WriteLine("Модуль произведет вывод сотрудников в указанном диапазоне дат рождения, введите начальную дату");
            DateTime dateFrom = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите конечную дату");
            DateTime dateTo = DateTime.Parse(Console.ReadLine());
            Worker[] fullResult = new Worker[allWorkers.Length];
            int k = 0;
            for (int i = 0; i < allWorkers.Length; i++)
            {
                if (allWorkers[i].DayOfBirth >= dateFrom && allWorkers[i].DayOfBirth <= dateTo)
                {
                    fullResult[k] = allWorkers[i];
                    k++;
                }
            }
            Worker[] result = new Worker[k];
            for (int i = 0; i < k; i++)
            {
                result[i] = fullResult[i];
            }

            return result;
        }
        /// <summary>
        /// Метод записи данных в файл
        /// </summary>
        /// <param name="work"></param>
        private void PrintInFile(Worker[] work)
        {
            using StreamWriter sr = new(path);
            string str;
            for (int i = 0; i < work.Length; i++)
            {
                str = work[i].Id.ToString() + "#" + work[i].DateOfCreation.ToString() +
                    "#" + work[i].FullName.ToString() + "#" + work[i].Age.ToString() + 
                    "#" + work[i].Height.ToString() + "#" + work[i].DayOfBirth.ToString() +
                    "#" + work[i].PlaceOfBirth.ToString();
                sr.WriteLine(str);
            }
        }
    }
}
