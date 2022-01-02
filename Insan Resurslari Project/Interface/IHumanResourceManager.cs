using Insan_Resurslari_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insan_Resurslari_Project.Interface
{
    interface IHumanResourceManager
    {
        Department[] Departments { get; }
        public Department[] GetDepartments();
        void AddDepartment(string departmentname, int workerlimit, double salarylimit);
        void EditDepartment(string name,string newName);
        void AddEmployee(string fullname, string position, double salary, string departmentName);
        void RemoveEmployee(string no, string departmentName);
        void EditEmployee(string no,double salary,string position);

    }
}
