using Models.Models;
using System;
using System.Collections.Generic;

namespace DZ10DataAccess_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new StudyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            EFRepository eFRepository = new EFRepository();
            Course course = eFRepository.CreateCourse(new Course
            {
                Name = "C#",
                StartDate = new DateTime(2019, 6, 13),
                EndDate = new DateTime(2019, 8, 13)
            });
            Course course1 = eFRepository.CreateCourse(new Course
            {
                Name = "C++",
                StartDate = new DateTime(2019, 6, 13),
                EndDate = new DateTime(2019, 8, 13)
            });

            HomeTask homeTask = eFRepository.CreateHomeTask(new HomeTask
            {
                Title = "Ho",
                Date = new DateTime(2019, 7, 13),
                Description = "Yop",
                Number = 2,
                CourseId = 2
            },
            1);

            Student student = eFRepository.CreateStudent(new Student
            {
                Name = "Lukiian",
                PhoneNumber = "0971252805",
                Email = "lukian37@gmail.com"
            });
            Student student1 = eFRepository.CreateStudent(new Student
            {
                Name = "Vlad",
                PhoneNumber = "0971250430",
                Email = "vladyslavm@gmail.com"
            });
            Student student2 = eFRepository.CreateStudent(new Student
            {
                Name = "Klymko",
                PhoneNumber = "0971255040",
                Email = "klymko37@gmail.com"
            });

            Lecturer lecturer = eFRepository.CreateLecturer(new Lecturer
            {
                Name = "Oles",
                BirthDate = new DateTime(1978, 10, 21)
            });
            Lecturer lecturer1 = eFRepository.CreateLecturer(new Lecturer
            {
                Name = "Taras",
                BirthDate = new DateTime(1992, 10, 12)
            });

            List<HomeTaskAssessment> homeTaskAssessments = new List<HomeTaskAssessment>(){
                new HomeTaskAssessment { Date = new DateTime(2019,06,21), IsComplete = false, StudentId = 2, HomeTaskId = 1},
                new HomeTaskAssessment { Date = new DateTime(2019,06,21), IsComplete = true, StudentId = 1, HomeTaskId = 1}
            };
            eFRepository.CreateHomeTaskAssessments(homeTaskAssessments);

            
            List<Course> listCourses = eFRepository.GetAllCourses();
            List<Student> listStudents = eFRepository.GetAllStudents();
            List<Lecturer> listLecturers = eFRepository.GetAllLecturers();

            Course getCourse = eFRepository.GetCourse(2);
            List<HomeTaskAssessment> homeTaskAssessment = eFRepository.GetHomeTaskAssessmentsByHomeTaskId(homeTask.HomeTaskId);
            HomeTask homeTask1 = eFRepository.GetHomeTaskById(1);
            Lecturer lecturer2 = eFRepository.GetLecturerById(2);
            Student student3 = eFRepository.GetStudentById(2);

            int[] studentsID = new int[] { 1, 3 };
            int[] studentsID1 = new int[] { 1, 2, 3 };
            eFRepository.SetStudentsToCourse(1, studentsID);
            eFRepository.SetStudentsToCourse(2, studentsID1);
            int[] lecturersID = new int[] { 1, 2 };
            int[] lecturersID1 = new int[] { 1 };
            eFRepository.SetLecturersToCourse(1, lecturersID);
            eFRepository.SetLecturersToCourse(2, lecturersID1);

            var l = eFRepository.GetStudentsByCourseId(1);
            var l1 = eFRepository.GetStudentsByCourseId(2);




            eFRepository.UpdateCourse(new Course { CourseId = 2, Name = "JAVA", StartDate = new DateTime(2019, 6, 13), EndDate = new DateTime(2019, 8, 13) });
            eFRepository.UpdateHomeTask(new HomeTask
            {
                HomeTaskId = 1,
                Title = "HomeTaskUpdated",
                Date = new DateTime(2019, 7, 13),
                Description = "Yop",
                Number = 2,
                CourseId = 2
            });
            homeTaskAssessments[0].IsComplete = true;
            homeTaskAssessments[0].HomeTaskAssessmentId = 1;
            homeTaskAssessments[1].HomeTaskAssessmentId = 2;
            eFRepository.UpdateHomeTaskAssessments(homeTaskAssessments);
            eFRepository.UpdateLecturer(new Lecturer
            {
                LecturerId = 2,
                Name = "Lukiian",
                BirthDate = new DateTime(1992, 10, 12)
            });
            eFRepository.UpdateStudent(new Student
            {
                StudentId = 2,
                Name = "Ostap",
                PhoneNumber = "0971250430",
                Email = "ostap@gmail.com"
            });


            eFRepository.DeleteCourse(1);
            eFRepository.DeleteStudent(3);
            eFRepository.DeleteLecturer(2);
            eFRepository.DeleteHomeTask(1);
        }
    }
}
