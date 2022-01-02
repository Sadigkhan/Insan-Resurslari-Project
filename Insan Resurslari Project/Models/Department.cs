using System;
using System.Collections.Generic;
using System.Text;

namespace Insan_Resurslari_Project.Models
{
    class Department
    {
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 2)
                {
                    Console.WriteLine("Departament adinda minimum 2 herf olmalidir.Yeniden teyin edin: ");
                    return;
                }
                _name = value;
            }
        }
        private string _name;
        public int WorkerLimit 
        {
            get
            {
                return _workerLimit;

            }
            set
            {
                if (value < 1)
                {
                    Console.WriteLine("Departamentde minimum 1 isci olmalidir");
                    return;
                }
                _workerLimit = value;
            }
        }
        private int _workerLimit;
        public double SalaryLimit 
        {
            get
            {
                return _salaryLimit;
            }
            set
            {
                if (value < 250)
                {
                    Console.WriteLine("Departamente teyin edile bilecek minimal maas 250 AZN-dir.Yeniden teyin edin: ");
                    return;
                }
                _salaryLimit = value;
            } 
        }
        private double _salaryLimit;
        public Employee[] Employees;
        
        public double CalcSalaryAverage()
        {
            double totalSalary = 0;
            int count = 0;
            foreach (Employee item in Employees)
            {
                if (item != null)
                {
                    totalSalary += item.Salary;
                    count++;                   
                }              
            }

            if (totalSalary == 0)
            {
                return 0;
            }
            else
            {
                return totalSalary / count;
            }
        }
        public double SalaryCounter()
        {
            double CurrenSalary = 0;

            foreach (Employee item in Employees)
            {
                if (item != null)
                {
                    CurrenSalary += item.Salary;
                }
            }

            return CurrenSalary;
        }

        public Department(string name,int workerlimit,double salarylimit)
        {
            Employees = new Employee[0];
            Name = name;
            WorkerLimit = workerlimit;
            SalaryLimit = salarylimit;
        }
       
        public override string ToString()
        {
            return $"Departanent adi: {Name}\nIsci sayi limiti: {WorkerLimit}\nIsci maasi limiti: {SalaryLimit}\n";
        }
    }
}