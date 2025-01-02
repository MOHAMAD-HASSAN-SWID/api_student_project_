using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace clint_first_api
{
    internal class Program
    {
        public class students_deteils
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool gender { get; set; }
            public int grade { get; set; }

        }

        static readonly HttpClient httpClient = new HttpClient();

        static async Task get_all_students()
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\nFetching all students...\n");
                var reseponse = await httpClient.GetAsync("Get_Students");
                if (reseponse.IsSuccessStatusCode)
                {
                    var student = await reseponse.Content.ReadFromJsonAsync<List<students_deteils>>();
                    if (student == null || student.Count == 0)
                    {
                        Console.WriteLine("no studen..."); 
                        return; 
                    }
                    foreach (var stu in student)
                    {
                        Console.WriteLine($"ID: {stu.id}, Name: {stu.name}, Age: {stu.gender}, Grade: {stu.grade}");
                    }

                }
                else
                {
                    Console.WriteLine("no connection...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        static async Task find_student(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\n find student...\n");
                var reseponse = await httpClient.GetAsync($"Find_Student{id}");
                if (reseponse.IsSuccessStatusCode)
                {
                    var student = await reseponse.Content.ReadFromJsonAsync<students_deteils>();
                    Console.WriteLine($"ID: {student.id}, Name: {student.name}, Age: {student.gender}, Grade: {student.grade}");
                }
                else if (reseponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine("no conncetion");
                }
            }
            catch(Exception ex) { Console.WriteLine( ex.ToString()); }
        }

        static async Task add_new_student(students_deteils stu)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\n add new student...\n");
                var respons = await httpClient.PostAsJsonAsync($"Add_New_Student", stu);
                if (respons.IsSuccessStatusCode)
                {
                    Console.WriteLine("the studen add succseflly");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        static async Task update_student(int id , students_deteils stu)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\n add new student...\n");
                var respons = await httpClient.PutAsJsonAsync($"Update_inforamtion{id}", stu);
                var message = await respons.Content.ReadAsStringAsync();
                if (respons.IsSuccessStatusCode)
                {
                    Console.WriteLine(message);
                }
                else if (respons.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task delete_student(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine("\n delete student...\n");
                var respons = await httpClient.DeleteAsync($"Delete_Student{id}");
                var message = await respons.Content.ReadAsStringAsync();
                if (respons.IsSuccessStatusCode)
                {
                    Console.WriteLine(message);
                }
                else if (respons.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static async Task Main()
        {
            httpClient.BaseAddress = new Uri("http://localhost:5080/api/student/");
            await get_all_students();

            await find_student(1);

            await find_student(5);

            await add_new_student(new students_deteils{ id = 5 , name = "tasnim" , gender = false , grade = 88});

            await get_all_students();

            await update_student(5, new students_deteils { name = "hinata", gender = false, grade = 44 });

            await get_all_students();

            await delete_student(5);

            await delete_student(5);

            await get_all_students();

            
        }


    }
}
