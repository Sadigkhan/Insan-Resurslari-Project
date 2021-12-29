using Insan_Resurslari_Project.Interface;
using Insan_Resurslari_Project.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insan_Resurslari_Project.Sevice
{
    class HumanResourceManager : IHumanResourceManager
    {
        private Department[] _departments;
        public Department[] Departments => _departments;
        public HumanResourceManager()
        {
            _departments = new Department[0];
        }

        public void AddDepartment(string departmentname, int workerlimit, double salarylimit)
        {
            Department department = new Department(name: departmentname, workerlimit:  workerlimit,salarylimit: salarylimit); ;
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;
        }

        public void AddEmployee(string fullname, string position, double salary, string departmentName)
        {
            Employee employee = new Employee(fullname: fullname, position: position, salary: salary, departmentName: departmentName);
            foreach (Department item in _departments)
            {
                if (employee.DepartmentName.ToUpper() == item.Name.ToUpper()) 
                {
                    Array.Resize(ref item.Employees, item.Employees.Length + 1);
                    item.Employees[item.Employees.Length - 1] = employee;
                }
            }
        }

        public void EditDepartment(string name, string newName)
        {
            Department department = null;
            foreach (Department item in _departments)
            {
                if (item.Name.ToUpper() == name.ToUpper())
                {
                    department = item;
                    break;
                }
            }
                department.Name = newName;

            //foreach (Department item in _departments)
            //{
            //    foreach (Employee item2 in item.Employees)
            //    {
            //        if (item.Name.ToUpper() == item2.DepartmentName.ToUpper())
            //        {
            //            item2.DepartmentName = newName;
            //        }
            //    }
            //}

            
        }

        public void EditEmployee(string no, string fullname,double salary, string position)
        {
            Employee employee = null;
            foreach (Department item in _departments)
            {
                if (item != null)
                {
                    foreach (Employee item2 in item.Employees)
                    {
                        if (item2 != null && item2.No == no && item2.Fullname == fullname)
                        {
                            employee = item2;
                        }
                    }
                }
            }
            employee.Position = position;
            employee.Salary = salary;
        }

        public void RemoveEmployee(string no, string departmentName)
        {
            foreach (Department item in _departments)
            {
                if (item != null)
                {
                    for (int i = 0; i < item.Employees.Length; i++)
                    {
                        if (item.Employees[i].No == no && item.Employees[i].DepartmentName == departmentName)
                        {
                            item.Employees[i] = null;
                            return;
                        }
                    }
                }                             
            }
        }

        public Department[] GetDepartments()
        {
            Department[] departments = new Department[0];
            foreach(Department item in _departments)
            {
                if (item != null)
                {
                    Array.Resize(ref departments, departments.Length + 1);
                    departments[departments.Length - 1] = item;
                }
            }
            return departments;
        }
    }
}
