using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers
{
    public class StudentController : ApiController
    {

        public IHttpActionResult GetStudentbyID (int id)
        {
            StudentViewModel student = null;

            SchoolDBEntities ctx = new SchoolDBEntities();

            student = ctx.Students.Where(x => x.StudentID == id).Select(x => new StudentViewModel
            {
                StudentName = x.StudentName,
                StudentID = x.StudentID,               
            }).FirstOrDefault<StudentViewModel>();

            if(student == null)
            {
                return NotFound();
            }

            return Ok(student);

        }

        public IHttpActionResult GetAllStudent()
        {
            IList<StudentViewModel> students = null;

            SchoolDBEntities ctx = new SchoolDBEntities();

            students = ctx.Students.Select(x => new StudentViewModel()
            {
                StudentID = x.StudentID,
                StudentName = x.StudentName,

            }).ToList<StudentViewModel >();



            if (students.Count == null)
            {
                return NotFound();
            }

            return Ok(students);

        }


        public IHttpActionResult PostNewStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using(var ctx = new SchoolDBEntities())
            {
                ctx.Students.Add(new Student()
                {
                    StudentID = student.StudentID,
                    StudentName = student.StudentName
                });


                ctx.SaveChanges();
            }

            return Ok();
        }
        

        public IHttpActionResult Put(StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using(var ctx = new SchoolDBEntities())
            {
                var existingStudent = ctx.Students.Where(x => x.StudentID == student.StudentID).FirstOrDefault<Student>();

                if(existingStudent != null)
                {
                    existingStudent.StudentName = student.StudentName;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new SchoolDBEntities())
            {
                var student = ctx.Students
                    .Where(x => x.StudentID == id)
                    .FirstOrDefault();
                ctx.Students.Remove(student);
                //ctx.Entry(student).state = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
