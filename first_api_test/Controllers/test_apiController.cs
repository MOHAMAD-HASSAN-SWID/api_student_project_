//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace first_api_test.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/student")]
    [ApiController]
    public class studentController : ControllerBase
    { 
        public class students_deteils 
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool gender { get; set; }
            public int grade { get; set; }

        }

        static List<students_deteils>  students = new List<students_deteils> 
        { 
            (new students_deteils { id = 1, name = "hassan", gender = true, grade = 85 }),
            (new students_deteils { id = 2, name = "mohamad", gender = true, grade = 55 }),
            (new students_deteils { id = 3, name = "abdo", gender = true, grade = 44 }),
            (new students_deteils { id = 4, name = "sana", gender = false, grade = 99 })
        };

        [HttpGet("Get_Students", Name = "Get_Students")]
        public ActionResult<IEnumerable<students_deteils>> get_all_student()
        {
            return Ok(students);
        }

        [HttpGet("Find_Student" , Name = "Find_Student")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<students_deteils> find_studen(int id)
        {
            students_deteils stud = students.Find(x => x.id == id);
            if (stud == null)
                return NotFound("this student not found");
            return Ok(stud);
        }

        //if gander is man enter true or for femal enter false
        [HttpPost("Add_New_Student" , Name = "Add_New_Student")]
        public ActionResult<string> add_new_student(int id , string name , bool gander , int grade)
        {
            
            students_deteils new_student = new students_deteils();
            new_student.id = id;
            new_student.name = name;
            new_student.gender = gander;
            new_student.grade = grade;
            students.Add(new_student);

            return Ok("the studen add succseflly");
        }

        [HttpPut("Update_inforamtion" , Name = "Update_inforamtion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> update_info_student(int id, string name, bool gander, int grade)
        {
            students_deteils student_up = students.Find(x => x.id == id);
            if (student_up != null)
            {
                student_up.name = name;
                student_up.gender = gander;
                student_up.grade = grade;
                return Ok("update seccsuflly");
            }

            return NotFound("this student not found");
        }

        [HttpDelete("Delete_Student" , Name = "Delete_Student")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string> delete_student(int id )
        {
            students_deteils student_up = students.Find(x => x.id == id);
            if (student_up != null)
            {
                students.Remove(student_up);
                return Ok("update seccsuflly");
            }
            return NotFound("this student not found");
        }




    }



}
