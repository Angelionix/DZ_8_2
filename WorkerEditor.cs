using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    class WorkerEditor
    {
        Worker[] allWorkers;
        private string[] workerTitles = new string[7] { "№", "Имя", "Фамилия", "Возраст", "Департамент", "Оклад", "Кол-во проектов" };

        /// <summary>
        /// Метод добавления сотрудника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника </param>
        /// <param name="age"> возраст</param>
        /// <param name="dep"> Департамент в который мы добавляем сотрудника</param>
        /// <param name="index"> Индекс куда мы добаляем сотрудника нового</param>
        /// <param name="salary"> Зарплата сотрудника</param>
        /// <param name="projectsCount"> Кол-во проектов у сотрудника</param>
        public void WorkerInvite(string name, string lastName, int age, Department dep, int index, int salary, int projectsCount, ref Organization org)
        {
            allWorkers = org.AllWorkers;
            Resize(index >= org.AllWorkers.Length, 2, ref allWorkers);
            Worker newWorker = new Worker();
            newWorker.Name = name;
            newWorker.LastName = lastName;
            newWorker.Age = age;
            newWorker.ID = index + 1;
            newWorker.ProjectCount = projectsCount;
            newWorker.Salary = salary;
            org.AllWorkers[index] = newWorker;
            newWorker.Department = dep;
            newWorker.Department.DepartmentWorkersCount += 1;
            org.IndexOfWorker++;
        }

        /// <summary>
        /// Получаем Обьект котрудника тз базы по его ИД
        /// </summary>
        /// <param name="id">ИД сотрудника</param>
        /// <returns></returns>
        public Worker TakeWorker(int id, Organization org)
        {
            allWorkers = org.AllWorkers;
            return allWorkers[id - 1];
        }

        /// <summary>
        /// Метод редактировния инфы
        /// </summary>
        /// <param name="id"> ИД записи</param>
        /// <param name="name"> Новое имя работника</param>
        /// <param name="lastName"> Новая фамилия работника</param>
        /// <param name="age"> Новый возраст работника</param>
        /// <param name="sal"> Новая зарплата</param>
        /// <param name="age"> Новое количество проэктов</param>      
        /// <param name="newDep"> Новый департамент</param>    
        /// <param name="projectCount"> Новое кол-во проектов</param>   
        public void EditWorker(int id, string name, string lastName, int age, int sal, int projectCount, Department newDep, Organization org)
        {
            allWorkers = org.AllWorkers;
            allWorkers[id - 1].Name = name;
            allWorkers[id - 1].LastName = lastName;
            allWorkers[id - 1].Age = age;
            allWorkers[id - 1].Salary = sal;
            allWorkers[id - 1].ProjectCount = projectCount;
            allWorkers[id - 1].Department = newDep;
        }

        /// <summary>
        /// метод для редактирования ид сотрудника после импорта
        /// </summary>
        /// <param name="index">временый ид после импорта</param>
        /// <param name="id">ИД который должен быть</param>
        public void EditWorker(int index, int id, Organization org)
        {
            allWorkers = org.AllWorkers;
            allWorkers[index - 1].ID = id;
        }

        /// <summary>
        ///  Увольняем работника и удаляем заись о нем из базы
        /// </summary>
        /// <param name="index"> ИД работника </param>
        public void FiringWorker(int index,ref Organization org)
        {
            allWorkers = org.AllWorkers;
            bool canResize = true;
            int i = index - 1;
            Array.Clear(allWorkers, i, 1);
            for (i = index - 1; i < org.IndexOfWorker - 1; index++)
            {
                allWorkers[i] = allWorkers[i + 1];
                allWorkers[i].ID = i + 1;
                if (allWorkers[i].ID == 0 && canResize)
                {
                    Resize(((float)allWorkers.Length / i > 2f), 0.5f,ref  allWorkers);
                    canResize = false;
                }
                i++;
            }
            org.IndexOfWorker--;
        }

        /// <summary>
        /// Метод для изменения емкости массива сотрудников
        /// </summary>
        /// <param name="flag"> Буулевая переменная показывает нужжно ли делать ресайз массива</param>
        /// <param name="i"> Интовый коэффициент изменения размера массива</param>
        /// <param name="allWorkers"> Массив сотрудников </param>
        private void Resize(bool flag, float i, ref Worker[] allWorkers)
        {
            if (flag)
            {
                Array.Resize(ref this.allWorkers, (int)(allWorkers.Length * i));
            }
        }

        /// <summary>
        /// Метод для вывода в консоль списка сотрудников организации
        /// </summary>
        public void PrintingAllWorkersToConsole(Organization org)
        {
            Console.WriteLine($"{workerTitles[0],10}| {workerTitles[1],15}| {workerTitles[2],15}| {workerTitles[3],8}| {workerTitles[4],15}| {workerTitles[5],6}| {workerTitles[6],16}");

            for (int i = 0; i < org.IndexOfWorker; i++)
            {
                NotePrintingToConsole(allWorkers[i]);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Метод выводит в консоль данные опеределнного работника
        /// </summary>
        /// <param name="worker">Мы передаем в метод экземпляр рабочего</param>
        public void NotePrintingToConsole(Worker worker)
        {
            if (worker != null)
            {
                Console.WriteLine($"{worker.ID,10}| {worker.Name,15}| {worker.LastName,15}| {worker.Age,8}| {worker.Department.DepartmentName,15}| {worker.Salary,6}| {worker.ProjectCount,16}");
            }
        }
    }
}
