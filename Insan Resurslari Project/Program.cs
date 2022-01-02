using Insan_Resurslari_Project.Models;
using Insan_Resurslari_Project.Sevice;
using System;

namespace Insan_Resurslari_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanResourceManager humanResourceManager = new HumanResourceManager();
            do
            {
                Console.WriteLine("***************Human Resource Manager***************");
                Console.WriteLine("Etmek istediyiniz emeliyyatin qarsisindaki nomreni daxil edin:");
                Console.WriteLine("\n1-Departamentlerin siyahisini goster:");
                Console.WriteLine("\n2-Departament yarat:");
                Console.WriteLine("\n3-Departamentde deyisiklik et:");
                Console.WriteLine("\n4-Iscilerin siyahisini goster:");
                Console.WriteLine("\n5-Departamentdeki iscilerin siyahisini goster:");
                Console.WriteLine("\n6-Isci elave et:");
                Console.WriteLine("\n7-Isci uzerinde deyisiklik et:");
                Console.WriteLine("\n8-Departamentdeki iscini sil:");
                Console.WriteLine("\nDaxil et:");

                string enter = Console.ReadLine();
                int checkEnter;
                int.TryParse(enter, out checkEnter);
                switch (checkEnter)
                {
                    case 1:
                        Console.Clear();
                        GetDepartments(ref humanResourceManager);
                        break;
                    case 2:
                        Console.Clear();
                        AddDepatment(ref humanResourceManager);
                        break;
                    case 3:
                        Console.Clear();
                        EditDepartment(ref humanResourceManager);
                        break;
                    case 4:
                        Console.Clear();
                        GetEmployees(ref humanResourceManager);
                        break;
                    case 5:
                        Console.Clear();
                        GetEmployeeByDepartment(ref humanResourceManager);
                        break;
                    case 6:
                        Console.Clear();
                        AddEmployee(ref humanResourceManager);
                        break;
                    case 7:
                        Console.Clear();
                        EditEmployee(ref humanResourceManager);
                        break;
                    case 8:
                        Console.Clear();
                        RemoveEmployee(ref humanResourceManager);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Duzgun daxil edin");
                        break;


                }

            } while (true);
        }

        private static void GetDepartments(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length > 0)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    int counter = 0;

                    for (int i = 0; i < item.Employees.Length; i++)
                    {
                        if (item.Employees[i] != null)
                        {
                            counter++;
                        }

                    }
                    if (item.Employees.Length == 0)
                    {
                        Console.WriteLine($"\n{item}\nMaas ortalamasi: {0}\nHazirki isci sayi: {counter}");
                        Console.WriteLine("_____________________________________________________________________________");

                    }
                    else
                    {
                        Console.WriteLine($"\n{item}\nMaas ortalamasi: {item.CalcSalaryAverage()}\nHazirki isci sayi: {counter}");
                        
                    }

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Departament movcud deyil. Departament daxil edin.\n ");
            }
        }

        private static void AddDepatment(ref HumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Departamentin adini daxil edin: ");
            reEnterDepartmentName:
            string departmentName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(departmentName))
            {
                Console.WriteLine("Departamentin adini duzgun daxil edin");
                goto reEnterDepartmentName;
            }
            Console.WriteLine("\nDepartamentin maksimal isci sayini daxil edin: ");
            checkWorkerlimit:
            string workersNum = Console.ReadLine();
            int workersNum1 = 0;
            while (!int.TryParse(workersNum, out workersNum1) || workersNum1 < 1)
            {
                Console.WriteLine("Isci sayini duzgun daxil edin...");
                goto checkWorkerlimit;
            }
            Console.WriteLine("\nDepartamentde butun iscilere verilecek ayliq cemi meblegi daxil edin: ");
            checkSalaryLimit:
            string salary = Console.ReadLine();
            double salaryNum = 0;
            while (!double.TryParse(salary, out salaryNum) || salaryNum < 250)
            {
                Console.WriteLine("Meblegi duzgun daxil edin...");
                goto checkSalaryLimit;
            }

            humanResourceManager.AddDepartment(departmentName, workersNum1, salaryNum);

        }

        private static void EditDepartment(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Hec bir departament movcud deyil...\n");
                return;
            }

            Console.WriteLine("Departamentlerin siyahisi:");
            Console.WriteLine("------------------------------");
            foreach (Department item in humanResourceManager.Departments)
            {
                Console.WriteLine($"{item}------------------------------");
            }

            Console.WriteLine("\nDeyisiklik etmek istediyiniz departamentin adini daxil edin: ");
            reEnterNameNow:
            string nameNow = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nameNow))
            {
                Console.WriteLine("Duzgun daxil edin:");
                goto reEnterNameNow;
            }

            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToLower() == nameNow.ToLower())
                {
                    Console.WriteLine("Secdiyiniz departament adini hansi ada deyismek isteyirsiniz? Daxil edin: ");
                    reEnterNewName:
                    string newName = Console.ReadLine();

                    if (String.IsNullOrWhiteSpace(newName))
                    {
                        Console.WriteLine("Duzgun daxil edin:");
                        goto reEnterNewName;
                    }


                    item.Name = newName;
                    break;
                }
                Console.WriteLine("Daxil etdiyiniz adda departament movcud deyil. Duzgun daxil edin: ");
                goto reEnterNameNow;
            }
        }

        private static void GetEmployees(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length > 0)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Employees.Length > 0)
                    {
                        Console.WriteLine("Iscilerin siyahisi:");
                        foreach (Employee emp in item.Employees)
                        {
                            Console.WriteLine(emp);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Departamentde isci movcud deyil");
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Isci movcud deyil. Ilk once isci elave edin.\n");
            }
        }

        private static void GetEmployeeByDepartment(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length > 0)
            {
                foreach (Department item in humanResourceManager.Departments)
                {

                    Console.WriteLine($"{item}\n");



                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Departament movcud deyil. Departament daxil edin.\n ");
            }
            Console.WriteLine("Iscilerini gormek istediyinz departamentin adini daxil edin: ");
            Console.WriteLine("--------------------------------------------------------------");
            string departmentname = Console.ReadLine();
            bool checkdepartmentname = true;
            int count1 = 0;
            while (checkdepartmentname)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToLower() == departmentname.ToLower())
                    {
                        count1++;
                    }
                }

                if (count1 <= 0)
                {
                    Console.WriteLine("Daxil Etdiyniz Adda Department Movcud Deyil");
                    Console.Write("Duzgun Departament Adi Daxil Et: ");
                    departmentname = Console.ReadLine();
                }
                else
                {
                    checkdepartmentname = false;
                }

                count1 = 0;
            }

            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Siyahi Bosdur. Once Daxil Edin");
                return;
            }
            foreach (var item in humanResourceManager.Departments)
            {
                foreach (Employee item1 in item.Employees)
                {
                    Console.WriteLine(item1);
                    Console.WriteLine("------------------------------------");
                }
            }


        }

        private static void AddEmployee(ref HumanResourceManager humanResourceManager)
        {

            Console.WriteLine("Elave etmek istediyiniz iscinin ad ve soyadini daxil edin: ");
            reEnterFullname:
            string tamAd = Console.ReadLine();
            string[] tam = tamAd.Split(' ');
            if (String.IsNullOrWhiteSpace(tamAd) || tam.Length < 2)
            {
                Console.WriteLine("Iscinin ad ve soyadini duzgun daxil edin");
                goto reEnterFullname;
            }

            Console.WriteLine("\nIscinin vezifesini daxil edin: ");
            reEnterPositionName:
            string vezifeAdi = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(vezifeAdi) || vezifeAdi.Length < 2)
            {
                Console.WriteLine("Vezife adini duzgun qeyd edin...");
                goto reEnterPositionName;
            }

            Console.WriteLine("\nElave etmek istediyiniz iscinin ayliq maasini daxil edin: ");
            reEnterSalary:
            string maas = Console.ReadLine();
            double checkmaas = 0;
            while (!double.TryParse(maas, out checkmaas) || checkmaas < 250)
            {
                Console.WriteLine("Maasi duzgun daxil edin.");
                goto reEnterSalary;
            }

            Console.WriteLine("\nIscini elave etmek istediyiniz departament adini daxil edin: ");
            reEnterDepartmentName:

            string departmentName = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(departmentName))
            {
                Console.WriteLine("Departament adini duzgun daxil edin...");
                goto reEnterDepartmentName;
            }
            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Hazirda departament movcud deyil. Ilk once departament yaradin.");
                return;
            }

            Department department = null;
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToUpper() == departmentName.ToUpper())
                {

                    department = item;
                    break;
                }
            }
            if (department != null)
            {
                double average = department.CalcSalaryAverage();
                double empCount = department.Employees.Length;

                double salaryLimit = ((average * empCount) + checkmaas);
                if (salaryLimit >= department.SalaryLimit)
                {
                    Console.Clear();
                    Console.WriteLine($"Departamente teyin edilmis maas limitini {department.SalaryLimit - (department.CalcSalaryAverage() * department.Employees.Length)} AZN kecdiniz");
                    return;
                }
                foreach (Department item in humanResourceManager.Departments)
                {
                    int nullcounter = 0;
                    int counter = 0;

                    for (int i = 0; i < item.Employees.Length; i++)
                    {
                        if (item.Employees[i] != null)
                        {
                            counter++;
                        }
                        else
                        {                                                                 //Departamentden isci silinerse yerine isci elave etmek mumkundur
                            nullcounter++;
                        }
                        if (counter - nullcounter >= department.WorkerLimit||counter-nullcounter==0)
                        {
                            Console.WriteLine("Teyin edilmis isci limitini kecdiniz...");
                            return;
                        }
                    }
                }
                {

                }
            
            }
            else
            {
                Console.WriteLine("Daxil Etdiyniz Add Department Yoxdur");
                return;
            }




            humanResourceManager.AddEmployee(tamAd, vezifeAdi, checkmaas, departmentName);
        }

        private static void EditEmployee(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length > 0)
            {


                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Employees.Length > 0)
                    {
                        Console.WriteLine("Iscilerin siyahisi:");
                        foreach (Employee emp in item.Employees)
                        {
                            Console.WriteLine(emp);
                        }
                        Console.WriteLine("Deyisiklik etmek istediyiniz iscinin nomresinbi daxil edin: ");
                        reEnterWorkerNo:
                        string workerNo = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(workerNo))
                        {
                            Console.WriteLine("Duzgun daxil edin:");
                            goto reEnterWorkerNo;
                        }
                        foreach (Employee item2 in item.Employees)
                        {
                            if (item2.No.ToUpper() == workerNo.ToUpper())
                            {
                                Console.WriteLine("Etmek istediyiniz emeliyyatin qarsisindaki nomreni daxil edin:\n");
                                Console.WriteLine("\t1 - Iscinin aldigi maasda duzelis etmek");
                                Console.WriteLine("\t2 - Iscinin vezifesinde duzelis etmek");

                                reEnterEditWorker:
                                string editWorker = Console.ReadLine();
                                int editWorkerNum = 0;
                                if (!int.TryParse(editWorker, out editWorkerNum))
                                {
                                    Console.WriteLine("Duzgun daxil edin...");
                                    goto reEnterEditWorker;
                                }

                                switch (editWorkerNum)
                                {
                                    case 1:
                                        Console.WriteLine("Iscinin yeni maasini daxil edin: ");

                                        reEnternewSalary:
                                        string newSalary = Console.ReadLine();
                                        int newSalaryNum = 0;
                                        if (!int.TryParse(newSalary, out newSalaryNum))
                                        {
                                            Console.WriteLine("Duzgun daxil edin...");
                                            goto reEnternewSalary;
                                        }

                                        item2.Salary = newSalaryNum;
                                        Console.WriteLine("Maasda duzelis olundu...");
                                        break;
                                    case 2:
                                        Console.WriteLine("Iscinin yeni vezifesini daxil edin: ");

                                        reEnternewPosition:
                                        string newPosition = Console.ReadLine();
                                        if (String.IsNullOrWhiteSpace(newPosition))
                                        {
                                            Console.WriteLine("Duzgun daxil edin...");
                                            goto reEnternewPosition;
                                        }

                                        item2.Position = newPosition;
                                        Console.WriteLine("Vezifede duzelis olundu...");
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Departamentde isci movcud deyil");
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Isci movcud deyil. Ilk once isci elave edin.\n");
            }
        }

        private static void RemoveEmployee(ref HumanResourceManager humanResourceManager)
        {
            if (humanResourceManager.Departments.Length > 0)
            {
                foreach (Department item in humanResourceManager.Departments)
                {

                    Console.WriteLine($"{item}\n");



                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Departament movcud deyil. Departament daxil edin.\n ");
            }
            Console.WriteLine("Iscisini silmek istediyiniz departamentin adini daxil edin: ");
            Console.WriteLine("--------------------------------------------------------------");
            string departmentname = Console.ReadLine();
            bool checkdepartmentname = true;
            int count1 = 0;
            while (checkdepartmentname)
            {
                foreach (Department item in humanResourceManager.Departments)
                {
                    if (item.Name.ToLower() == departmentname.ToLower())
                    {
                        count1++;
                    }
                }

                if (count1 <= 0)
                {
                    Console.WriteLine("Daxil Etdiyniz Adda Department Movcud Deyil");
                    Console.Write("Duzgun Departament Adi Daxil Et: ");
                    departmentname = Console.ReadLine();
                }
                else
                {
                    checkdepartmentname = false;
                }

                count1 = 0;
            }
            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Siyahi Bosdur. Once Daxil Edin");
                return;
            }
            foreach (var item in humanResourceManager.Departments)
            {
                foreach (Employee item1 in item.Employees)
                {
                    Console.WriteLine(item1);
                    Console.WriteLine("------------------------------------");
                }
                Console.WriteLine("Silmek istediyiniz iscinin nomresinbi daxil edin: ");
                reEnterWorkerNo:
                string workerNo = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(workerNo))
                {
                    Console.WriteLine("Duzgun daxil edin:");
                    goto reEnterWorkerNo;
                }


                for (int i = 0; i < item.Employees.Length; i++)
                {
                    if (item.Employees[i].No.ToUpper() == workerNo.ToUpper())
                    {
                        item.Employees[i] = null;
                        Console.WriteLine("Silmek istediyiniz isci ugurla silindi");
                    }
                }


            }
        }
    }
}
