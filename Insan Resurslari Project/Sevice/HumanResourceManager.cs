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
            
            foreach (Department dep in _departments)
            {
                if (dep.Name.ToUpper() == name.ToUpper())
                {
                    dep.Name = newName;
                    foreach (Employee emp in dep.Employees)
                    {
                        if (emp != null)
                        {
                            emp.DepartmentName = newName;
                            emp.No = emp.DepartmentName.ToUpper().Substring(0, 2) + emp.No.Remove(0, 2);
                        }
                    }
                    break;
                }
            }
        }

        public void EditEmployee(string no,double salary, string position)
        {
            Employee employee = null;
            foreach (Department item in _departments)
            {
                if (item != null)
                {
                    foreach (Employee item2 in item.Employees)
                    {
                        if (item2 != null && item2.No == no)
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
