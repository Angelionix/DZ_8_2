using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    class Department
    {
        private string depName;                                 //Название департамента
        private DateTime depCreationDate;                       //Дата создания депортамента
        private int workersCount;                               //Количество рабоников в депортаменте
        private int projectCount;                               //Количество проектов в депортаменте
        private int depID;                                      //Ид депортамента
        //Набор заголовков для красивого вывода инфы о департаментах
        private string[] depTitles = new string[5] {"№", "Название", "Кол-во сотрудников", "Кол-во проектов", "Дата создания" };
        #region Properties
        public Department(string name)
        {
            depName = name;
            depCreationDate = DateTime.Now;
            workersCount = 0;
            projectCount = 0;
        }

        public string DepartmentName
        {
            get
            {
                return depName;
            }
            set
            {
                depName = value;
            }
        }

        public DateTime DepartmentCreationDate
        {
            get
            {
                return depCreationDate;
            }
        }

        public int DepartmentWorkersCount        
        {
            get
            {
                return workersCount;
            }
            set
            {
                workersCount = value;
            }
        }

        public int DepartmentID
        {
            get 
            {
                return depID;
            }
            set
            {
                depID = value;
            }
        }
        #endregion
        /// <summary>
        /// Вывод информации о депортаменте в консоль
        /// </summary>
        public void ShowDepInfo()
        {
            Console.WriteLine($"{depTitles[0],4}| {depTitles[1],10}| {depTitles[2],19}| {depTitles[3],15}| {depTitles[4],19}");
            Console.WriteLine($"{depID,4}| {depName,10}| {workersCount,19}| {projectCount,15}| {depCreationDate,19}");
        }
    }
}
