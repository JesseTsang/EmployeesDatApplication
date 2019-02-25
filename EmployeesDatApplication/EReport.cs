using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace EmployeesDatApplication
{
    /// <summary>
    /// Namespace: EmployeesDatApplication     
    /// Class:  EReport          
    /// Author: Jesse Tsang (JesseTsang@Outlook.com)
    /// </summary>
    /// <remarks>
    /// It will read a employees.dat file and extract the employees info, then:
    /// 1. Sort the employees info in term of employee ID, then display the result.
    /// 2. Sort the employees info in term of employee family name, then display the result.
    /// </remarks>
    public class EReport
    {
        //Constant variable for Employees.dat filename
        private const string EmployeesFileName = "employees.dat";

        /// <summary>
        /// The StartProcess method.
        /// </summary>
        /// <remarks>
        /// This public method will interface other private methods to provide the main function of the program.
        /// </remarks>
        public void StartProcess()
        {
            IList<string> fileContentList = ReadFile();

            var sortByIdResult = SortById(fileContentList);
            var sortByFamilyNameResult = SortByFamilyName(fileContentList);
            
            DisplayOutput("Processing by employee number...", sortByIdResult);
            DisplayOutput("Processing by last (family) Name...", sortByFamilyNameResult);
        }
        
        /// <summary>
        /// The ReadFile method.
        /// </summary>
        /// <remarks>
        /// This method will read "employees.dat" and output IList of string.
        /// </remarks>
        private IList<string> ReadFile()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EmployeesFileName);
            IList<string> result = new List<string>();
            
            if (!(File.Exists(filePath)))
            {
                Console.WriteLine("File path is: " + filePath);
                Console.WriteLine("File does not exists. Please recheck. Closing program.");

                System.Environment.Exit(1);
            }
       
            try
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    //Discard all lines starting with "#"
                    if (line.Contains("#")) continue;
             
                    result.Add(line);
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: The file could not be read:");
                Console.WriteLine(e.Message);
                throw;
            }
               
            return result;  
        }
        
        /// <summary>
        /// The SortById method.
        /// </summary>
        /// <remarks>
        /// Input: IList &lt;string&gt; of ID,name value pairs
        /// Output: A list sorted by ID.
        /// </remarks>
        private dynamic SortById(IList<string> list)
        {
            var sortById = list.Select(x => x.Split(',').ToArray())
                                  .OrderBy(x => x[0])
                                  .Select(x => string.Join(",", x));
            
            return sortById;
        }
        
        /// <summary>
        /// The SortByFamilyName method.
        /// </summary>
        /// <remarks>
        /// Input: IList &lt;string&gt; of ID,name value pairs
        /// Output: A list sorted by family name.
        /// </remarks>
        private dynamic SortByFamilyName(IList<string> list)
        {
            var sortByFamilyName = list.Select(x => x.Split(' ').ToArray())
                                  .OrderBy(x => x[1])
                                  .Select(x => string.Join(" ", x));
            
            return sortByFamilyName;
        }
        
        /// <summary>
        /// The DisplayOutput method.
        /// </summary>
        /// <remarks>
        /// Input: A display message and IEnumerable  &lt;string&gt; of a query result.
        /// Output: Display the message and iterate over the query result.
        /// </remarks>
        private void DisplayOutput(string message, dynamic result)
        {
            Console.WriteLine(message);
 
            foreach(var line in result)
            {			
                Console.WriteLine(line);
            }
            
            Console.WriteLine(" ");
        }
        
        public static void Main(string[] args)
        {   
            EReport report1 = new EReport();
  
            report1.StartProcess();
        }
    }
}