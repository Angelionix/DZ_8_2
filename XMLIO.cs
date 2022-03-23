using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OrgInfoSystem_prototype
{
    class XMLIO
    {
        /// <summary>
        /// Метод для загрузк информации об организации из файла ХМЛ
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Обьект типа организация</returns>
        public Organization Import(string path)
        {
            Organization tempOrg = new Organization();
            string xml = System.IO.File.ReadAllText(path);

            tempOrg.OrganizationTitle = XDocument.Parse(xml).Element("Organisation").Attribute("Title").Value;

            var colDep = XDocument.Parse(xml).Descendants("Department").ToList();

            foreach (var item in colDep)
            {
                tempOrg.DepEditor.AddDepartment(item.Attribute("DepartmentTitle").Value, tempOrg.IndexOfDepartment,ref tempOrg);

                var colWor = item.Descendants("Worker").ToList();

                foreach (var worker in colWor)
                {
                    string tempName = worker.Attribute("Name").Value;
                    string tempLastName = worker.Attribute("LastName").Value;
                    int tempAge = int.Parse(worker.Attribute("Age").Value);
                    int tempID = int.Parse(worker.Attribute("ID").Value);
                    int tempSalary = int.Parse(worker.Attribute("Salary").Value);
                    int tempProjectCount = int.Parse(worker.Attribute("ProjectsCount").Value);
                    Department tempDepartment = tempOrg.DepEditor.ViewDepartmentByIndex(tempOrg.IndexOfDepartment,ref tempOrg);
                    tempOrg.WorkerEditor.WorkerInvite(tempName, tempLastName, 10, tempDepartment, tempOrg.IndexOfWorker,tempSalary,tempProjectCount,ref tempOrg);
                    tempOrg.WorkerEditor.EditWorker(tempOrg.IndexOfWorker, tempID, tempOrg);
                }
            }
            return tempOrg;
        }

        /// <summary>
        /// экспорт информации об организации в файл в формате ХМЛ
        /// </summary>
        /// <param name="org">Обьект типа организация</param>
        /// <param name="path">путь для созранения</param>
        public void Export(Organization org, string path)
        {
            XElement organiztion = new XElement("Organisation");

            XAttribute orgTitle = new XAttribute("Title",org.OrganizationTitle);
            organiztion.Add(orgTitle);

            for (int i = 1; i <= org.IndexOfDepartment; i++)
            {
                XElement department = new XElement("Department");
                string depTitle = org.DepEditor.ViewDepartmentByIndex(i,ref org).DepartmentName;
                XAttribute departmantTitle = new XAttribute("DepartmentTitle", depTitle);

                department.Add(departmantTitle);

                for (int j = 1; j <= org.IndexOfWorker; j++)
                {
                    Worker currentWorker = org.WorkerEditor.TakeWorker(j, org);
                    if (currentWorker.Department.DepartmentName == depTitle)
                    {
                        XElement worker = new XElement("Worker");
                        XAttribute name = new XAttribute("Name",currentWorker.Name);
                        XAttribute lastName = new XAttribute("LastName",currentWorker.LastName);
                        XAttribute age = new XAttribute("Age", currentWorker.Age);
                        XAttribute id = new XAttribute("ID", currentWorker.ID);
                        XAttribute salary = new XAttribute("Salary", currentWorker.Salary);
                        XAttribute projectsCount = new XAttribute("ProjectsCount", currentWorker.ProjectCount);

                        worker.Add(projectsCount);
                        worker.Add(salary);
                        worker.Add(age);
                        worker.Add(name);
                        worker.Add(lastName);
                        worker.Add(id);
                        department.Add(worker);
                    }
                }
                organiztion.Add(department);
            }
            organiztion.Save(path);
        }
    }
}
