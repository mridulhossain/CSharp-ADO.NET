using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();

            var connectionString = "Server = .\\SQLEXPRESS; Database = CSharpB8; User Id = mridul; Password = 123456;";
            var insertQury = "insert into students([name],dateofbirth,cgpa) values(@Name,'2/2/2020',3.40)";
            var deleteQury = "delete from students where name = 'noor' ";
            var studentsDataQury = "select * from students";


            ExecuteNonQury(connectionString, insertQury, new List<SqlParameter> { new SqlParameter("@Name", name) });

            var data = ExecuteQury(connectionString, studentsDataQury);

            foreach (var item in data)
            {
                Console.WriteLine($"{item.Id}/ Name: {item.Name}, Date Of Birth: {item.DateOfBirth}, Cgpa : {item.CGPA}");
            }

        }


        public static void ExecuteNonQury(string connectionString,string sql,List<SqlParameter> perameters)
        {

          
            using SqlCommand command = new SqlCommand();
            command.Connection = GetCommand(connectionString);

            command.CommandText = sql;
            command.Parameters.AddRange(perameters.ToArray());
            command.ExecuteNonQuery();
            //command.Dispose();

            Console.WriteLine("NonQury Executed");


            //if (connection.State == System.Data.ConnectionState.Open)
            //    connection.Close();

            //connection.Dispose();
            //connection.Close();
        }



        //ExecuteQury
        public static List<Student> ExecuteQury(string connectionString, string sql)
        {
            using SqlCommand command = new SqlCommand();
            command.Connection = GetCommand(connectionString);
            command.CommandText = sql;

            var reader = command.ExecuteReader();

            List<Student> students = new List<Student>();
            while (reader.Read())
            {
                var id = (int)reader[0];
                var name = (string)reader[1];
                var dob = (DateTime)reader[2];
                var cgpa = (decimal)reader["CGPA"];

                students.Add(new Student
                {
                    Id = id,
                    Name = name,
                    DateOfBirth = dob,
                    CGPA = cgpa
                }); 

            }

            Console.WriteLine("Qury Executed");
            return students;
        }



        //sql connection setup
        private static SqlConnection GetCommand(string connectionString)
        {

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;

        }



        //data structure for student
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public decimal CGPA { get; set; }
        }
    }
}
