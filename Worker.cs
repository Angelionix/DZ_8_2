using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    class Worker
    {
        private string name;                //Имя сотрудника
        private string lastName;            //Фамилия сотрудника
        private int age;                    //Возраст сотрудника
        private int id;                     //ИД сотруднгика
        private int salary;                 //Зарплата сотрудника
        private int projectsCount;          //Кол-во проектов сотрудника
        private Department department;      //Департамент к которому приписан сотрудник
        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
            }
        }

        public int ProjectCount
        {
            get
            {
                return projectsCount;
            }
            set
            {
                projectsCount = value;
            }
        }

        public Department Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
            }
        }
        #endregion
    }
}
