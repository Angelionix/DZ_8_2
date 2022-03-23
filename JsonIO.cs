using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrgInfoSystem_prototype
{
    class JsonIO
    {
        /// <summary>
        /// Метод для созранения информации об организации в файл Джейсон
        /// </summary>
        /// <param name="org">Обьект организация</param>
        /// <param name="path">Путь для созранения файла</param>
        public void SaveToFile(Organization org, string path)
        {
            JObject organisationObj = new JObject();
            organisationObj["organisation_title"] = org.OrganizationTitle;

            JArray arrayWorker = new JArray();
            JArray arrayDep = new JArray();

            for (int i = 1; i <= org.IndexOfDepartment; i++)
            {
                JObject depObject = new JObject();
                depObject["department"] = org.DepEditor.ViewDepartmentByIndex(i, ref org).DepartmentName;
                arrayDep.Add(depObject);
            }

            for (int j = 1; j <= org.IndexOfWorker; j++)
            {
                Worker tempWorker = org.WorkerEditor.TakeWorker(j,org);
                string depTitle = tempWorker.Department.DepartmentName;
                if (tempWorker.Department.DepartmentName == depTitle)
                {
                    JObject workerObj = new JObject();
                    workerObj["id"] = org.WorkerEditor.TakeWorker(j, org).ID;
                    workerObj["name"] = org.WorkerEditor.TakeWorker(j, org).Name;
                    workerObj["lastName"] = org.WorkerEditor.TakeWorker(j, org).LastName;
                    workerObj["age"] = org.WorkerEditor.TakeWorker(j, org).Age;
                    workerObj["salary"] = org.WorkerEditor.TakeWorker(j, org).Salary;
                    workerObj["projectsCount"] = org.WorkerEditor.TakeWorker(j, org).ProjectCount;
                    workerObj["department"] = depTitle;
                    workerObj["depID"] = tempWorker.Department.DepartmentID;
                    arrayWorker.Add(workerObj);
                }
            }

            organisationObj["departments"] = arrayDep;
            organisationObj["workers"] = arrayWorker;
            string json = organisationObj.ToString();

            File.WriteAllText(path,json);
        }
        /// <summary>
        /// Метод для загрузки инфы об организации из файла
        /// </summary>
        /// <param name="path">Путь где хранится файл</param>
        /// <returns>Обьект типа организация</returns>
        public Organization LoadFromJsonFile(string path)
        {
            Organization tempOrg = new Organization();
            tempOrg.IndexOfDepartment = 0;
            string json = File.ReadAllText(path);
            var departments = JObject.Parse(json)["departments"].ToArray();
            var workers = JObject.Parse(json)["workers"].ToArray();

            foreach (var dep in departments)
            {
                string depTitle = dep["department"].ToString();
                tempOrg.DepEditor.AddDepartment(depTitle, tempOrg.IndexOfDepartment, ref tempOrg);
            }

            foreach (var worker in workers)
            {
                string name = worker["name"].ToString();
                string lastName = worker["lastName"].ToString();
                int id = int.Parse(worker["id"].ToString());
                int age = int.Parse(worker["age"].ToString());
                int salary = int.Parse(worker["salary"].ToString());
                int projectsCount = int.Parse(worker["projectsCount"].ToString());
                Department department = tempOrg.DepEditor.ViewDepartmentByIndex(int.Parse(worker["depID"].ToString()), ref tempOrg);

                tempOrg.WorkerEditor.WorkerInvite(name,lastName,age, department , tempOrg.IndexOfWorker, salary, projectsCount,ref tempOrg);
                tempOrg.WorkerEditor.EditWorker(tempOrg.IndexOfWorker, id,tempOrg);
            }

            return tempOrg;
        }

    }
}
