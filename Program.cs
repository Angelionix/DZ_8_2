using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace OrgInfoSystem_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLIO xmlIO = new XMLIO();                                              ///Экземпляр класса для работы с импортом\экспортом в  ХМЛ
            JsonIO jsonIO = new JsonIO();                                           ///Экземпляр класса для работы с ипортом\экспортом в ДжейСон
            Organization org = new Organization();                                  ///Экземпляр класса организации
            int choise = 0;                                                         ///Вспомогательная переменная для работы с системой меню

            bool isWorkWithProgramm = true;                                         ///Переменная для определния выхода из программы
            bool isWorkWithDep;                                                     ///Переменная для определения завершения работы с разделами
            bool isWorkWithWorkers;                                                 ///Переменная для определения завершения работы с сотрудниками
                
            string xmlPath = @"organisation.xml";                                   ///Путь для файла ХМЛ
            string jsonPath = @"organisation.json";                                 ///Путь до Файла ДжейСон
            org.OrganizationTitle = "Umbrella";                                     ///Название Организации

            Filler(ref org);                                                           ///Заполняем базу случайным значениями

            Console.WriteLine($"Добро пожаловать в Информационную систему Компании {org.OrganizationTitle}\n");
            Console.WriteLine($"На данный момен компания имеет {org.IndexOfDepartment} отделов");
            Console.WriteLine($"Суммарно в компании {org.IndexOfWorker} сотрудников\n");

            do
            {
                Console.WriteLine("1. Работа c департаментами");
                Console.WriteLine("2. Работа с сотрудниками");
                Console.WriteLine("3. Импорт данных из файла");
                Console.WriteLine("4. Экспорт данных в файл");
                Console.WriteLine("5. Завершение работы\n");

                choise = Utility.SafeInputing(1, 5);

                switch (choise)
                {
                    case 1:
                    #region WorkWithDepartments
                        isWorkWithDep = true;
                        do
                        {
                            Console.WriteLine("Список отделов:");
                            org.DepEditor.PrintingListOfDep(ref org);
                            Console.WriteLine();

                            Console.WriteLine("1. Добавить Отдел");
                            Console.WriteLine("2. Редактировать раздел");
                            Console.WriteLine("3. Удалить Отдел");
                            Console.WriteLine("4. Закончить работу");

                            choise = Utility.SafeInputing(1, 6);

                            switch (choise)
                            {
                                case 1:
                                    Console.WriteLine("Укажите пожалуйста Название нового отдела");
                                    string title = Console.ReadLine();
                                    org.DepEditor.AddDepartment(title, org.IndexOfDepartment,ref org);
                                    break;
                                case 2:
                                    Console.WriteLine("Выберите отдел для редактирования\n");
                                    org.DepEditor.PrintingListOfDep(ref org);

                                    int n = Utility.SafeInputing(1, org.IndexOfDepartment);
                                    Department tempDep = org.DepEditor.ViewDepartmentByIndex(n,ref org);
                                    tempDep.ShowDepInfo();

                                    Console.WriteLine("Укажите название департамента");
                                    string newName = Console.ReadLine();
                                    if (newName == String.Empty)
                                    {
                                        newName = tempDep.DepartmentName;
                                    }
                                    tempDep.DepartmentName = newName;
                                    org.DepEditor.EdtitingDeparment(tempDep, n,ref org);
                                    break;
                                case 3:
                                    Console.WriteLine("Выберите отдел для Удаления");
                                    int i = Utility.SafeInputing(1, org.IndexOfDepartment + 1);
                                    org.DepEditor.RemoveDepartment(i,ref org);
                                    break;
                                case 4:
                                    isWorkWithDep = false;
                                    break;
                            }
                        }
                        while (isWorkWithDep);
                        break;
                    #endregion
                    case 2:
                    #region WorkWithWorkers                       
                        do
                        {
                            isWorkWithWorkers = true;
                            Console.WriteLine("Список сотрудников компании");
                            org.WorkerEditor.PrintingAllWorkersToConsole(org);
                            Console.WriteLine("1. Добавить запись о сотруднике");
                            Console.WriteLine("2. Редактировать запись о сотруднике");
                            Console.WriteLine("3. Уволить сотрудника");
                            Console.WriteLine("4. Сортировать список сотрудников");
                            Console.WriteLine("5. Вернуться на уровень выше");

                            choise = Utility.SafeInputing(1, 5);

                            switch (choise)
                            {
                                case 1:
                                    #region AddingWorker
                                        Console.WriteLine("Пожалуйста укажите Имя");
                                        string name = Console.ReadLine();

                                        Console.WriteLine("Пожалуйста укажите Фамилию");
                                        string lastName = Console.ReadLine();

                                        Console.WriteLine("Пожалуйста укажите возраст");
                                        int age = Utility.SafeInputing(20, 100);

                                        Console.WriteLine("Пожалуйста укажите Отдел:");
                                        org.DepEditor.PrintingListOfDep(ref org);
                                        choise = Utility.SafeInputing(1, org.IndexOfDepartment);
                                        Department tempDep = org.DepEditor.ViewDepartmentByIndex(choise,ref org);

                                        Console.WriteLine("Пожалуйста укажите зарплату");
                                        int salary = Utility.SafeInputing(5000, 100000);

                                        Console.WriteLine("Пожалуйста укажите кол-во проектов");
                                        int projectsCount = Utility.SafeInputing(1, 5);

                                    org.WorkerEditor.WorkerInvite(name, lastName, age, tempDep, org.IndexOfWorker,salary,projectsCount, ref org);

                                        break;
                                    #endregion
                                case 2:
                                    #region EditingWorker
                                    Console.WriteLine("Пожалуйста укажите ID сотрудника");
                                    int id = Utility.SafeInputing(1, org.IndexOfWorker);
                                    Worker curWorker = org.WorkerEditor.TakeWorker(id,org);

                                    Console.WriteLine("Желаете изменить имя?\n1.Да\n2.Нет");
                                    choise = Utility.SafeInputing(1, 2);
                                    string newName = string.Empty;
                                    if (choise == 1)
                                    {
                                        Console.WriteLine("Введите новое имя:");
                                        newName = Console.ReadLine();
                                    }
                                    else
                                    {
                                        newName = curWorker.Name;
                                    }

                                    Console.WriteLine("Желаете изменить Фамилию?\n1.Да\n2.Нет");
                                    choise = Utility.SafeInputing(1, 2);
                                    string newLastName = string.Empty;
                                    if (choise == 1)
                                    {
                                        newLastName = Console.ReadLine();
                                    }
                                    else
                                    {
                                        newLastName = curWorker.LastName;
                                    }

                                    Console.WriteLine("Желаете изменить возраст?\n1.Да\n2.Нет");
                                    choise = Utility.SafeInputing(1, 2);
                                    int newAge = 0;
                                    if (choise == 1)
                                    {
                                        newAge = Utility.SafeInputing(18,90);
                                    }
                                    else
                                    {
                                        newAge = curWorker.Age;
                                    }

                                    Console.WriteLine("Желаете изменить зарплату?\n1.Да\n2.Нет");
                                    choise = Utility.SafeInputing(1, 2);
                                    int newSalary = 0;
                                    if (choise == 1)
                                    {
                                        newSalary = Utility.SafeInputing(0,100000);
                                    }
                                    else
                                    {
                                        newSalary = curWorker.Salary;
                                    }

                                    Console.WriteLine("Желаете изменить Кол-во проектов?\n1.Да\n2.Нет");
                                    choise = Utility.SafeInputing(1, 2);
                                    int newProjCount = 0;
                                    if (choise == 1)
                                    {
                                        newProjCount = Utility.SafeInputing(0,10);
                                    }
                                    else
                                    {
                                        newProjCount = curWorker.ProjectCount;
                                    }

                                    Department newDep = curWorker.Department;
                                    if (org.IndexOfDepartment > 1)
                                    {
                                        Console.WriteLine("Желаете изменить отдел?\n1.Да\n2.Нет");
                                        choise = Utility.SafeInputing(1, 2);
                                        if (choise == 1)
                                        {
                                            Console.WriteLine("Пожалуйста укажите Новый Отдел:");
                                            org.DepEditor.PrintingListOfDep(ref org);
                                            choise = Utility.SafeInputing(1, org.IndexOfDepartment);
                                            newDep = org.DepEditor.ViewDepartmentByIndex(choise, ref org);
                                        }
                                    }
                                    else 
                                    {
                                        newDep = curWorker.Department;
                                    }

                                    org.WorkerEditor.EditWorker(id, newName, newLastName, newAge, newSalary, newProjCount, newDep,org);
                                    break;
                                    #endregion
                                case 3:
                                    #region DeletingWorker
                                    Console.WriteLine("Укажите пожалуйста ID сотрудника для увольнения");
                                    id = Utility.SafeInputing(1,org.IndexOfWorker);
                                    org.WorkerEditor.FiringWorker(id,ref org);
                                    break;
                                    #endregion
                                case 4:
                                    #region SortingWorkers
                                    List<int> sortingFields = new List<int>();
                                    List<int> dirs = new List<int>();
                                    choise = 2;
                                    do 
                                    {
                                        Console.WriteLine("Укажите пожалуйста по скольки полям вы хотите провести сортировку");
                                        Console.WriteLine("1.ИД\n2.Имя.\n3.Фамилия\n4.Возраст\n" +
                                            "5.Департамент.\n6.Зарплата.\n7.Кол-во проектов.\n8.Выйти из сортировки");
                                        int i = Utility.SafeInputing(1, 7);
                                        if (!sortingFields.Contains(i))
                                        {
                                            sortingFields.Add(i);

                                            Console.WriteLine("Укажите пожалуйста сортировку производить по возрастанию или убыванию");
                                            Console.WriteLine("1.По возрастанию \n2.По убыванию");
                                            int dir = Utility.SafeInputing(1, 2);
                                            dirs.Add(dir);

                                            Console.WriteLine("Хотите доавить еще поле для сортировки?\n1.Да\n2.Нет");
                                            choise = Utility.SafeInputing(1, 2);
                                        }
                                        else if(i == 8)
                                        {
                                            choise = 1;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Вы уже выбирали данное поле. Выберите другое");
                                        }
                                        
                                    } while(choise == 1);
                                    Console.Clear();
                                    Console.WriteLine("Список работников до сортировки");
                                    org.WorkerEditor.PrintingAllWorkersToConsole(org);
                                    org.SortingWorker(sortingFields, dirs);
                                    Console.WriteLine("Список работников после сортировки");
                                    org.WorkerEditor.PrintingAllWorkersToConsole(org);                               
                                    #endregion
                                    break;
                                case 5:
                                    /// Завершавем работу с сотрудниками
                                    isWorkWithWorkers = false;
                                    break;
                            }
                        }
                        while (isWorkWithWorkers);                      
                        break;
                    #endregion
                    case 3:
                    #region ImportFromFile
                        Console.WriteLine("Выберите формат файла для импорта:");
                        Console.WriteLine("1.Из xml");
                        Console.WriteLine("2.Из json");

                        choise = Utility.SafeInputing(1, 2);
                        if (choise == 1)
                        {
                            org = xmlIO.Import(xmlPath);
                        }
                        else
                        {
                            org = jsonIO.LoadFromJsonFile(jsonPath);
                        }
                        break;
                    #endregion
                    case 4:
                    #region ExportToFile
                        Console.WriteLine("Выберите формат файла для экспорта:");
                        Console.WriteLine("1.В xml");
                        Console.WriteLine("2.В json");

                        choise = Utility.SafeInputing(1, 2);

                        Worker tempWorker = new Worker();
                        if (choise == 1)
                        {
                            xmlIO.Export(org, xmlPath);
                        }
                        else
                        {
                            File.Create(jsonPath).Dispose();
                            jsonIO.SaveToFile(org, jsonPath);

                        }
                        break;
                    #endregion
                    case 5:
                        /// Завершаем работы с программой
                        isWorkWithProgramm = false;
                        break;
                }
            }
            while (isWorkWithProgramm);
            Console.WriteLine("Спасибо за работу с нашей системой. Хорошего вам дня");
        }

        public static void Filler(ref Organization org)
        {
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                string title = $"Отдел {i + 1}";
                org.DepEditor.AddDepartment(title, i, ref org);
            }

            for (int i = 0; i < 5; i++)
            {
                string name = $"Имя {rnd.Next(1, 99)}";
                string lastName = $"Фамилия {rnd.Next(1, 99)}";
                int age = rnd.Next(20, 91);
                int salary = rnd.Next(5000, 100000);
                int projectsCount = rnd.Next(1, 5);
                int j = rnd.Next(0, org.IndexOfDepartment);
                Department dep = org.Departments[j];
                org.WorkerEditor.WorkerInvite(name, lastName, age, dep, i, salary, projectsCount,ref org);
            }
        }

    }

}
