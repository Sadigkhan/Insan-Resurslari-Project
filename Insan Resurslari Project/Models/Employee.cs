using System;
using System.Collections.Generic;
using System.Text;

namespace Insan_Resurslari_Project.Models
{
    class Employee
    {
        public static int count = 1000;
        public string No { get; set; }
        public string Fullname { get; set; }
        public string Position 
        {
            get
            {
                return _position;
            }

            set
            {
                if (value.Length < 2)
                {
                    Console.WriteLine("Vezife adinda minimum 2 herf olmalidir. Zehmet olmasa yeniden daxil edin...");
                    return;
                }
                _position = value;
                
            }
            
            
        }
        private string _position;
        public double Salary {
            get
            {
                return _salary; ;
            }
            set
            {
                if (value < 250)
                {
                    Console.WriteLine("Isciye teyin oluna bilecek minimal maas 250 AZN-dir. Zehmet olmasa maasi yeniden teyin edin...");
                    return;
                }
                _salary = value;
            } 
        }
        private double _salary;
        public string DepartmentName { get; set; }
        public Employee(string fullname,string position,double salary,string departmentName)
        {
            Fullname = fullname;
            Position = position;
            Salary = salary;
            DepartmentName = departmentName;
            count++;
            No = departmentName.ToUpper().Substring(0, 2) + count;
        }
        public override string ToString()
        {
            return $"Fullname: {Fullname}\nPosition: {Position}\nSalary: {Salary}\nDepartment: {DepartmentName}\nWorker number: {No}";
        }
    }
}
