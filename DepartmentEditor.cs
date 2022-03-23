using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    class DepartmentEditor
    {
        private Department[] departments;

        /// <summary>
        /// Метод для получения отдела по его ИД
        /// </summary>
        /// <param name="i">ИД отдела</param>
        /// <returns></returns>
        public Department ViewDepartmentByIndex(int i, ref Organization org)
        {
            departments = org.Departments;
            return departments[i - 1];
        }

        /// <summary>
        /// Метод для редактировани назвавния отдела
        /// </summary>
        /// <param name="dep"></param>
        /// <param name="index"></param>
        public void EdtitingDeparment(Department dep, int index,ref Organization org)
        {
            departments = org.Departments;
            departments[index - 1] = dep;
        }

        /// <summary>
        /// Метод для вывода в консоль списка департаментов
        /// </summary>
        public void PrintingListOfDep(ref Organization org)
        {
            departments = org.Departments;
            Console.WriteLine($"{"№",4}| {"Навзвание",10}");
            foreach (Department dep in departments)
            {
                if (dep != null)
                {
                    Console.WriteLine($"{dep.DepartmentID,4}| {dep.DepartmentName,10}");
                }
            }
        }

        /// <summary>
        /// Метод для добвления нового Департамента
        /// </summary>
        /// <param name="title">Название департамента</param>
        /// <param name="index">индекс первог опустого элемента массива департаментов</param>
        public void AddDepartment(string title, int indexOfDep,ref Organization org)
        {
            departments = org.Departments;
            Department dep = new Department(title);
            dep.DepartmentID = indexOfDep + 1;
            Resize(indexOfDep >= departments.Length, 2, ref departments);
            departments[indexOfDep] = dep;
            org.Departments = departments;
            org.IndexOfDepartment++;
        }

        /// <summary>
        /// Метод для удаления департамента
        /// </summary>
        /// <param name="index"> индекс удаляемого департамента</param>
        public void RemoveDepartment(int id,ref Organization org)
        {
            departments = org.Departments;
            bool canResize = true;
            int i = id - 1;
            Array.Clear(departments, i, 1);
            for (int index = i; index < departments.Length - 1; index++)
            {
                departments[index] = departments[index + 1];
                if (departments[index] != null)
                {
                    departments[index].DepartmentID = index + 1;
                }

                if (departments[index] == null && canResize)
                {
                    Resize(((float)departments.Length / index > 2f), 0.5f, ref departments);
                    canResize = false;
                }
            }
        }

        /// <summary>
        /// Метод Resize удваивает емость массива в случае если индекс добавляемого элемента больше длины текущего массива
        /// </summary>
        /// <param name="flag"> Параметр который показывает нужно ли увеличить обьем массива </param>
        /// <param name="i"> Интовый коэффициент изменения размера массива</param>
        /// <param name="departments"> Массив департаментов </param>
        private void Resize(bool flag, float i,ref Department[] departments)
        {
            if (flag)
            {
                Array.Resize(ref departments, (int)(departments.Length * i));
            }
        }

        /// <summary>
        /// Метод для заполнения базы рандомными значениями
        /// </summary>

    }
}
    