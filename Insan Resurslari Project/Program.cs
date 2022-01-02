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
                        Console.WriteLine("______________________________________________________________________________");

                    }

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Departament movcud deyil. Departament daxil edin.\n ");
            }
            humanResourceManager.GetDepartments();
        }

        private static void AddDepatment(ref HumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Departamentin adini daxil edin: ");
            reEnterDepartmentName:
            string departmentName = Console.ReadLine();
            foreach (Department dep in humanResourceManager.Departments)
            {
                if (dep.Name == departmentName)
                {
                    Console.WriteLine("Bu adda departament movcuddur.Yeni ad teyin edin:");
                    goto reEnterDepartmentName;
                }
            }
            if (String.IsNullOrWhiteSpace(departmentName))
            {
                Console.WriteLine("Departamentin adini duzgun daxil edin");
                goto reEnterDepartmentName;
            }
            Console.WriteLine("\nDepartamentin maksimal isci sayini teyin  edin: ");
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
            string slry = Console.ReadLine();
            double salaryLim = 0;
            while (!double.TryParse(slry, out salaryLim) || salaryLim < 250)
            {
                Console.WriteLine("Meblegi duzgun daxil edin...");
                goto checkSalaryLimit;
            }

            humanResourceManager.AddDepartment(departmentName, workersNum1, salaryLim);

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
            reEntercurrentName:
            string currentName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(currentName))
            {
                Console.WriteLine("Duzgun daxil edin:");
                goto reEntercurrentName;
            }

            bool find = false;
            string yeniAd = string.Empty;
            foreach (Department item in humanResourceManager.Departments)
            {
                if (item.Name.ToLower() == currentName.ToLower())
                {
                    Console.WriteLine("Departamentin yeni adini daxil edin: ");
                    reEnterNewName:
                    yeniAd = Console.ReadLine();

                    if (String.IsNullOrWhiteSpace(yeniAd) || yeniAd.Length < 2)
                    {
                        Console.WriteLine("Duzgun daxil edin:");
                        goto reEnterNewName;
                    }

                    find = true;
                    break;
                }
            }

            if (find)
            {
                Console.Clear();
                Console.WriteLine("Departamentin adi yenisi ile evez olundu...");
            }

            if (find == false)
            {
                Console.WriteLine("Daxil etdiyiniz adda departament tapilmadi. Departament adini duzgun daxil edin: ");
                goto reEntercurrentName;
            }

            humanResourceManager.EditDepartment(currentName, yeniAd);

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
                            Console.WriteLine("---------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Departamentde isci movcud deyil");
                        Console.WriteLine("--------------------------------");
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
            if (humanResourceManager.Departments.Length <= 0)
            {
                Console.WriteLine("Hazirda departament movcud deyil.Departament yaradin:\n");
                return;
            }

            Console.WriteLine("Sistemde movcud olan departamentler:\n");
            Console.WriteLine("------------------------------------------");
            foreach (Department item in humanResourceManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("------------------------------------------");
            }

            Console.WriteLine("\nIscilerini gormek istediyiniz departamentin adini daxil edin...");

            reEnterDepartmentName:

            string departmentName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(departmentName) || departmentName.Length < 2)
            {
                Console.WriteLine("Departament adini duzgun daxil edin: ");
                goto reEnterDepartmentName;
            }

            bool check = true;
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name.ToLower() == departmentName.ToLower())
                {
                    Console.Clear();
                    int count = 0;
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        Console.WriteLine("Secilmis departamentde isci movcud deyil...\n");
                        return;
                    }

                    foreach (Employee emp in department.Employees)
                    {
                        if (emp != null)
                        {
                            Console.WriteLine(emp);
                            Console.WriteLine("------------------------------------------");
                        }
                    }
                    check = false;
                    break;
                }
            }

            if (check)
            {
                Console.Clear();
                Console.WriteLine("Bu adda departament movcud deyil...\n");
                return;
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
                    if (item.WorkerLimit % 2 != 0)
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
                            if (counter - nullcounter >= department.WorkerLimit || counter - nullcounter == 0)
                            {
                                Console.WriteLine("Teyin edilmis isci limitini kecdiniz...");
                                return;
                            }
                        }

                    }
                    else
                    {
                        int nullcounter1 = 0;
                        int counter1 = 0;
                        for (int j = 0; j < item.Employees.Length; j++)
                        {
                            if (item.Employees[j] != null)
                            {
                                counter1++;
                            }
                            else
                            {                                                                 //Departamentden isci silinerse yerine isci elave etmek mumkundur
                                nullcounter1++;
                            }
                            if (counter1 - nullcounter1 >= department.WorkerLimit || counter1 - nullcounter1 == 0)
                            {
                                Console.WriteLine("Teyin edilmis isci limitini kecdiniz...");
                                return;
                            }
                        }
                    }
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
            string IdNo = string.Empty;
            string YeniVezife = string.Empty;
            double YeniMaas = 0;
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
                        Console.WriteLine("Deyisiklik etmek istediyiniz iscinin ID nomresini daxil edin: ");
                        reEnterWorkerNo:
                        IdNo = Console.ReadLine();
                        if (String.IsNullOrWhiteSpace(IdNo))
                        {
                            Console.WriteLine("Duzgun daxil edin:");
                            goto reEnterWorkerNo;
                        }
                        foreach (Employee item2 in item.Employees)
                        {
                            if (item2.No.ToUpper() == IdNo.ToUpper())
                            {
                                Console.WriteLine("Etmek istediyiniz emeliyyatin qarsisindaki nomreni daxil edin:\n");
                                Console.WriteLine("\t1 - Iscinin aldigi maasda duzelis etmek");
                                Console.WriteLine("\t2 - Iscinin vezifesinde duzelis etmek");

                                reEnterEditWorker:
                                string EmeliyyatNo = Console.ReadLine();
                                int EmeliyyatNom = 0;
                                if (!int.TryParse(EmeliyyatNo, out EmeliyyatNom))
                                {
                                    Console.WriteLine("Duzgun daxil edin...");
                                    goto reEnterEditWorker;
                                }

                                switch (EmeliyyatNom)
                                {
                                    case 1:
                                        Console.WriteLine("Iscinin yeni maasini daxil edin: ");

                                        reEnternewSalary:
                                        string newSalary = Console.ReadLine();
                                        YeniMaas = 0;
                                        if (!double.TryParse(newSalary, out YeniMaas))
                                        {
                                            Console.WriteLine("Duzgun daxil edin...");
                                            goto reEnternewSalary;
                                        }
                                        item2.Salary = YeniMaas;
                                        foreach (Department dep in humanResourceManager.Departments)
                                        {
                                            if (dep.SalaryCounter() > dep.SalaryLimit)
                                            {
                                                Console.WriteLine("Departamente teyin olunmus maas limitini kecmemelisiniz");
                                                goto reEnternewSalary;
                                            }
                                        }
                                        Console.WriteLine("Maasda duzelis olundu...");
                                        return;
                                    case 2:
                                        Console.WriteLine("Iscinin yeni vezifesini daxil edin: ");

                                        reEnternewPosition:
                                        YeniVezife = Console.ReadLine();
                                        if (String.IsNullOrWhiteSpace(YeniVezife))
                                        {
                                            Console.WriteLine("Duzgun daxil edin...");
                                            goto reEnternewPosition;
                                        }

                                        item2.Position = YeniVezife;
                                        Console.WriteLine("Vezifede duzelis olundu...");
                                        return;
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
            humanResourceManager.EditEmployee(IdNo, YeniMaas, YeniVezife);
        }

        private static void RemoveEmployee(ref HumanResourceManager humanResourceManager)
        {
            int counter = 0;
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Employees.Length > 0)
                {
                    counter++;
                }
            }

            if (counter == 0)
            {
                Console.WriteLine("Hec bir isci movcud deyil. Emeliyyati icra etmek ucun hec olmasa 1 departament ve 1 nefer isci olmalidir.\n");
                return;
            }

            Console.WriteLine("Departamentlerin siyahisi:\n");
            Console.WriteLine("------------------------------------------");
            foreach (Department item in humanResourceManager.Departments)
            {
                Console.WriteLine(item);
                Console.WriteLine("------------------------------------------");
            }

            Console.WriteLine("\nHansi departamenten isci silmek isteyirsiniz?");
            Console.Write("Departament adini daxil edin: ");
            reEnterSelectDepartment:
            string selectDepartment = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(selectDepartment) || selectDepartment.Length < 2)
            {
                Console.WriteLine("Duzgun daxil edin:");
                goto reEnterSelectDepartment;
            }

            string DelWorker = string.Empty;
            bool find = true;
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name.ToLower() == selectDepartment.ToLower())
                {
                    find = false;
                    int cntworker = 0;
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            cntworker++;
                        }
                    }
                    if (cntworker == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"\"{department.Name}\" departamentinde isci yoxdur...\n");
                        return;
                    }

                    Console.Clear();
                    Console.WriteLine($"\"{department.Name}\" departamentindeki iscilerin siyahisi:\n");
                    Console.WriteLine("------------------------------------------");
                    foreach (Employee employee in department.Employees)
                    {
                        if (employee != null)
                        {
                            Console.WriteLine(employee);
                            Console.WriteLine("------------------------------------------");
                        }
                    }

                    Console.WriteLine("\nSilmek istediyiniz iscinin nomresini daxil edin:");
                    reEnterDelWorker:
                    DelWorker = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(DelWorker) || DelWorker.Length < 6)
                    {
                        Console.WriteLine("Duzgun daxil edin:");
                        goto reEnterDelWorker;
                    }

                    bool deleted = false;
                    for (int i = 0; i < department.Employees.Length; i++)
                    {
                        if (department.Employees[i] != null)
                        {
                            if (department.Employees[i].No.ToLower() == DelWorker.ToLower())
                            {
                                deleted = true;
                                break;
                            }
                        }
                    }

                    if (deleted)
                    {
                        Console.Clear();
                        Console.WriteLine("Isci silindi...\n");
                    }

                    if (deleted == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Daxil etdiyiniz nomrede isci movcud deyil...\n");
                        return;
                    }

                    break;
                }
            }

            if (find)
            {
                Console.Clear();
                Console.WriteLine("Daxil etdiyiniz adda departament yoxdur...\n");
                return;
            }

            humanResourceManager.RemoveEmployee(DelWorker, selectDepartment);

        }

    }


}


