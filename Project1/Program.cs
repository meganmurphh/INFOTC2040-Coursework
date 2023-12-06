using System;
using System.IO;
using System.Collections.Generic;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ask for user name
            Console.WriteLine("Please Enter you name: ");
            string userName = Console.ReadLine();

            //ask user for file name
            string file = getDocumentName();

            Console.WriteLine($"\nHello {userName}.\nWelcome to the Payroll Processing Application\n------------------------------");
            //read file and calculate pay
            
            //Get list of salaries to process summary report
            List<float> salaries = processPayroll(file);

            //Process Summary report
            processReport(salaries);

            Console.WriteLine("\nGoodbye!");

        }

        static void processReport(List<float> salaryList){
            //number of Employees
            int numberOfEmployees = salaryList.Count;

            //calculate total salary
            float totalSalary = 0;

            foreach(float salary in salaryList){
                totalSalary += salary;
            }
            //Avg Salary
            float averageSalary = totalSalary/numberOfEmployees;

            //max salary
            float max = maxSalary(salaryList);

            //min
            float min = minSalary(salaryList);

            //write to file
            using(StreamWriter fileWriter = new StreamWriter("salarySummary.txt")){
                fileWriter.WriteLine("Payroll Summary\n----------------");
                fileWriter.WriteLine($"Number of Employees Paid: {numberOfEmployees}");
                fileWriter.WriteLine($"Total Payroll: ${totalSalary:N2}");
                fileWriter.WriteLine($"Average Salary: ${averageSalary:N2}");
                fileWriter.WriteLine($"Maximun Salary: ${max:N2}");
                fileWriter.WriteLine($"Minimum Salary: ${min:N2}");
            }

            Console.WriteLine("\nThe salarySummary.txt file was successfully written.\n");
        }

        static float minSalary(List<float> salaryList){
            float min = salaryList[0];
            foreach(float salary in salaryList){
                if(salary < min){
                    min = salary;
                }
            }
            return min;
        }

        static float maxSalary(List<float> salaryList){
            float max = salaryList[0];
            foreach(float salary in salaryList){
                if(salary > max){
                    max = salary;
                }
            }
            return max;
        }

        static List<float> processPayroll(string fileName){
            List<float> employeePay = new List<float>();
            //open file
            try{
                using(StreamReader fileReader = new StreamReader(fileName)){
                    while(!fileReader.EndOfStream){
                        //read the next line of data
                        string lineOfData = fileReader.ReadLine();

                        //get the name, hours, and pay rate
                        string[] payrollData = lineOfData.Split(",");
                        string employeeName = payrollData[0] + " " + payrollData[1];
                        int hours = int.Parse(payrollData[2]);
                        float payRate = float.Parse(payrollData[3]);
                        float salary = hours * payRate;

                        //print Salary data to console
                        Console.Write($"{employeeName}: ${salary:N2}\n");

                        //add salary to the list
                        employeePay.Add(salary);
                    }
                    //close file
                    fileReader.Close();
                }
            }catch(Exception){
                //if an exception occurs, exit the program
                Console.WriteLine("There was an error processing the payroll. Exiting the Application...");
                Environment.Exit(0);
            }
            return employeePay;
        }

        /*
        Function to get the document name. Function also calls the 
        Parameters: none
        Returns: the document name ending with ".csv"
        */
        static string getDocumentName(){
            string docName;
            while(true){
                Console.WriteLine("\nEnter payroll file name to be processed: ");
                docName = Console.ReadLine();
                
                if(docName == ""){
                    //throw new Exception("Error: Document name is null.\n You must enter a document name.\n");
                    Console.WriteLine("Error: You must enter a document name.\n");
                    continue;
                }else{
                    break;
                }
            }
            return checkDocumentName(docName);
        }

        /*
        Function to check the ending of the document name. Appends ".csv" to document name if .csv not present
        Parameters: string document name
        Returns: the document name ending with ".csv"
        */
        static string checkDocumentName(string documentName){
            if(documentName.EndsWith(".csv")){
                return documentName;
            }else{
                return documentName + ".csv";
            }
        }
    }
}
