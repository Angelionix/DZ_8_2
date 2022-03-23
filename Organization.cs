using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    class Organization
    {
        private string organisationTitle;                                   //Название организации
        private Department[] departments;                                   //Массив Депортаментов
        private Worker[] allWorkers;                                        //Массив сотрудников
        private int indexOfWorker;                                          //Индекс первого пустого элемента списка сотрудников
        private int indexOfDep;                                             //Индекс первого пустого элемента списка депортаментом
        private WorkerSorter ws;
        private WorkerEditor we;
        private DepartmentEditor dp;

        public Organization()
        {
            departments = new Department[1];
            indexOfDep = 0;
            indexOfWorker = 0;
            allWorkers = new Worker[1];
            ws = new WorkerSorter();
            dp = new DepartmentEditor();
            we = new WorkerEditor();
        }
        #region Properties
        public string OrganizationTitle
        {
            get
            {
                return organisationTitle;
            }
            set
            {
                organisationTitle = value;
            }
        }

        public int AllWorkersCount
            {
            get
            {
                return allWorkers.Length;
            }
        }

        public int IndexOfDepartment
        {
            get
            {
                return indexOfDep;
            }
            set
            {
                indexOfDep = value;
            }
        }

        public Department[] Departments
        {
            get
            {
                return departments;
            }
            set
            {
                departments = value;
            }
        }

        public Worker[] AllWorkers
        {
            get
            {
                return allWorkers;
            }
            set
            {
                allWorkers = value;
            }
        }

        public int IndexOfWorker
        {
            get
            {
                return indexOfWorker;
            }
            set
            {
                indexOfWorker = value;
            }
        }

        public WorkerSorter WorkerSorter
        {
            get
            {
                return ws;
            }          
        }

        public DepartmentEditor DepEditor
        {
            get 
            {
                return dp;
            }
        }
        public WorkerEditor WorkerEditor
        {
            get
            {
                return we;
            }
        }
        #endregion

        #region SortingWorkers
        /// <summary>
        /// Метод для выбора типа сортировки в зависимости от типа входных данных
        /// </summary>
        /// <param name="fieldForSort">Список выбранных полей для сортировки</param>
        /// <param name="dirs">список с выбором направления сортировки</param>
        public void SortingWorker(List<int> fieldForSort, List<int> dirs)
        {
            int sortingCycles = fieldForSort.Count;
            for (int i = 0; i < sortingCycles; i++)
            {
                //для числовых полей
                if (fieldForSort[i] == 1 || fieldForSort[i] == 4 || fieldForSort[i] == 6 || fieldForSort[i] == 7)
                {
                    QuickSort(allWorkers, 0, indexOfWorker - 1, fieldForSort[i], dirs[i]);
                }
                //для строковых
                else
                {
                    SelectionSort(allWorkers,dirs[i],fieldForSort[i]);
                }
            }
        }

        #region QuickSort
        /// <summary>
        /// Для сортировки числовых полей использую Быструю сортировку
        /// </summary>
        /// <param name="values">Массив сотрудников</param>
        /// <param name="left">левая граница массива</param>
        /// <param name="right">Правая граница массива</param>
        /// <param name="field">Поле по которому сортируем</param>
        /// <param name="dir">Направление сортировки</param>
        public void QuickSort(Worker[] values, int left, int right, int field, int dir)
        {
            if (left < right)
            {
                int opornyiElevemt = Partition(ref values, left, right, field, dir);
                QuickSort(values, left, opornyiElevemt - 1, field, dir);
                QuickSort(values, opornyiElevemt + 1, right, field, dir);
            }
        }
        /// <summary>
        /// собствеено метод для получения подмассивов и основной работы по сортировке
        /// </summary>
        /// <param name="values">Масиив рабочих</param>
        /// <param name="left">левая граница сортируемого диапазона</param>
        /// <param name="right">правая гранциа сортируемого дипазона</param>
        /// <param name="field">Поле для сортировки</param>
        /// <param name="dir">Направление сортировки</param>
        /// <returns>Возвращает индекс опорного элемента </returns>
        static int Partition(ref Worker[] values, int left, int right, int field, int dir)
        {
            int x = 0;
            switch (field)
            {
                case 1:
                    x = values[right].ID;
                    break;
                case 4:
                    x = values[right].Age;
                    break;
                case 6:
                    x = values[right].Salary;
                    break;
                case 7:
                    x = values[right].ProjectCount;
                    break;
            }
            
            int less = left;

            for (int i = left; i < right; i++)
            {
                int y = 0;
                switch (field)
                {
                    case 1:
                        y = values[i].ID;
                        break;
                    case 4:
                        y = values[i].Age;
                        break;
                    case 6:
                        y = values[i].Salary;
                        break;
                    case 7:
                        y = values[i].ProjectCount;
                        break;
                }
                if (dir == 1)
                {
                    if (y < x)
                    {
                        Swap(ref values[i], ref values[less]);
                        less++;
                    }
                }
                else
                {
                    if (y > x)
                    {
                        Swap(ref values[i], ref values[less]);
                        less++;
                    }
                }

            }
            Swap(ref values[less], ref values[right]);
            return less;
        }
        #endregion QuickSort
        /// <summary>
        /// Для сортировки строк использовал алгоритм выбора
        /// </summary>
        /// <param name="workers">массив работников</param>
        /// <param name="dir">Направление сортировки</param>
        /// <param name="field">поле для сортиовки</param>
        public  void SelectionSort(Worker[] workers, int dir, int field)
        {
            int minStringLenght = 3;  // переменная которая указывает на сколько символов в глубину сравнивать строки

            for (int z = 0; z < minStringLenght-1; z++)                                     //Внешний цикл делает переход по слоям букв
            {
                for (int i = 0; i < indexOfWorker; i++)
                {
                    int pick = i;
                    for (int j = i + 1; j < indexOfWorker; j++)
                    {
                        char[] temp = new char[4];
                        char[] pickStr = new char[4];
                        switch (field)
                        {
                            case 2:
                                temp = workers[j].Name.ToCharArray();
                                pickStr = workers[pick].Name.ToCharArray();
                                break;
                            case 3:
                                temp = workers[j].LastName.ToCharArray();
                                pickStr = workers[pick].LastName.ToCharArray();
                                break;
                            case 5:
                                temp = workers[j].Department.DepartmentName.ToCharArray();
                                pickStr = workers[pick].Department.DepartmentName.ToCharArray();
                                break;
                        }
                        if (dir == 1)
                        {
                            if ((z - 1 < 0 || temp[z-1]== pickStr[z-1]) && temp[z] < pickStr[z])
                            {
                                pick = j;
                            }
                        }
                        else
                        {
                            if ((z - 1 < 0 || temp[z - 1] == pickStr[z - 1]) && temp[z] > pickStr[z])
                            {
                                pick = j;
                            }
                        }
                    }
                    Swap(ref workers[i], ref workers[pick]);
                }
            }
            Console.ReadLine();
        }
        /// <summary>
        /// метод для переены меставми двух элементов в масиве
        /// </summary>
        /// <param name="a">Первый элемент</param>
        /// <param name="b">второй элемент</param>
        private static void Swap(ref Worker a,ref Worker b)
        {
            Worker temp = b;
            b = a;
            a = temp;
        }
        #endregion
    }
}
