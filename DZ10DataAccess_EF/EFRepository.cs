//using DataAccess.ADO;
using Microsoft.Extensions.Options;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DZ10DataAccess_EF
{
    class EFRepository : IRepository
    {
        public string ConnectionString { get; set; }

        public EFRepository()
        {
            this.ConnectionString = @"Data Source=ACERE5-521\SQLEXPRESS01;Initial Catalog=BaseCourse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public Course CreateCourse(Course course)
        {
            using (var context = new StudyContext())
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
            return course;
        }

        public HomeTask CreateHomeTask(HomeTask homeTask, int courseId)
        {
            using (var context = new StudyContext())
            {
                context.HomeTasks.Add(new HomeTask
                {
                    Title = homeTask.Title,
                    Date = homeTask.Date,
                    Description = homeTask.Description,
                    Number = homeTask.Number,
                    CourseId = courseId
                });
                context.SaveChanges();
            }
            return homeTask;
        }

        public void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskAssessments)
        {
            using (var context = new StudyContext())
            {
                foreach (var assessment in homeTaskAssessments)
                {
                    context.HomeTaskAssessments.Add(new HomeTaskAssessment
                    {
                        Date = assessment.Date,
                        HomeTaskId = assessment.HomeTaskId,
                        StudentId = assessment.StudentId,
                        IsComplete = assessment.IsComplete
                    });
                }
                context.SaveChanges();
            }
        }

        public Lecturer CreateLecturer(Lecturer lecturer)
        {
            using (var context = new StudyContext())
            {
                context.Lecturers.Add(lecturer);
                context.SaveChanges();
            }
            return lecturer;
        }

        public Student CreateStudent(Student student)
        {
            using (var context = new StudyContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
            return student;
        }

        public void DeleteCourse(int courseId)
        {
            using (var context = new StudyContext())
            {
                var course = context.Courses.Find(courseId);
                context.Courses.Remove(course);
                context.SaveChanges();
            }
        }

        public void DeleteHomeTask(int homeTaskId)
        {
            using (var context = new StudyContext())
            {
                var homeTask = context.HomeTasks.Find(homeTaskId);
                context.HomeTasks.Remove(homeTask);
                context.SaveChanges();
            }
        }

        public void DeleteLecturer(int lecturerId)
        {
            using (var context = new StudyContext())
            {
                var lecturer = context.Lecturers.Find(lecturerId);
                context.Lecturers.Remove(lecturer);
                context.SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (var context = new StudyContext())
            {
                var student = context.Students.Find(studentId);
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> result = new List<Course>();
            using (var context = new StudyContext())
            {
                result = context.Courses.ToList();
            }
            return result;
        }

        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> result = new List<Lecturer>();
            using (var context = new StudyContext())
            {
                result = context.Lecturers.ToList();
            }
            return result;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> result = new List<Student>();
            using (var context = new StudyContext())
            {
                result = context.Students.ToList();
            }
            return result;
        }

        public Course GetCourse(int id)
        {
            Course result = new Course();
            using (var context = new StudyContext())
            {
                result = context.Courses.Find(id);
            }
            return result;
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByHomeTaskId(int homeTaskId)
        {
            List<HomeTaskAssessment> result = new List<HomeTaskAssessment>();
            using (var context = new StudyContext())
            {
                IEnumerable<HomeTaskAssessment> k = from h in context.HomeTaskAssessments
                                                    where h.HomeTask.HomeTaskId == homeTaskId
                                                    select new HomeTaskAssessment
                                                    {
                                                        Date = h.Date,
                                                        HomeTask = h.HomeTask,
                                                        HomeTaskAssessmentId = h.HomeTaskAssessmentId,
                                                        IsComplete = h.IsComplete,
                                                        Student = h.Student
                                                    };
                result = k.ToList();
            }
            return result;
        }

        public HomeTask GetHomeTaskById(int id)
        {
            HomeTask result = new HomeTask();
            using (var context = new StudyContext())
            {
                result = context.HomeTasks.Find(id);
            }
            return result;
        }

        public Lecturer GetLecturerById(int id)
        {
            Lecturer result = new Lecturer();
            using (var context = new StudyContext())
            {
                result = context.Lecturers.Find(id);
            }
            return result;
        }

        public Student GetStudentById(int id)
        {
            Student result = new Student();
            using (var context = new StudyContext())
            {
                result = context.Students.Find(id);
            }
            return result;
        }

        public List<Student> GetStudentsByCourseId(int courseId)
        {
            List<Student> result = new List<Student>();
            using (var context = new StudyContext())
            {
                IEnumerable<Student> k = from s in context.Students
                                         join c in context.CourseStudents on s.StudentId equals c.StudentId
                                         where c.CourseId == courseId
                                         select new Student
                                         {
                                             Name = s.Name,
                                             PhoneNumber = s.PhoneNumber,
                                             Email = s.Email,
                                             StudentId = s.StudentId,
                                             BirthDate = s.BirthDate,
                                             GitHubLink = s.GitHubLink,
                                             Notes = s.Notes,
                                             CourseStudents = s.CourseStudents,
                                             HomeTaskAssessments = s.HomeTaskAssessments
                                         };
                result = k.ToList();
            }
            return result;
        }

        public void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds)
        {
            List<CourseLecturer> courseLecturers = new List<CourseLecturer>();
            using (var context = new StudyContext())
            {
                courseLecturers = context.CourseLecturers.Where(p => p.CourseId == courseId).ToList();
                foreach (var courseLecturer in courseLecturers)
                {
                    context.CourseLecturers.Remove(courseLecturer);
                }

                foreach (var lecturerId in lecturerIds)
                {
                    context.CourseLecturers.Add(new CourseLecturer { CourseId = courseId, LecturerId = lecturerId });
                }
                context.SaveChanges();
            }
        }

        public void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId)
        {
            List<CourseStudent> courseStudents = new List<CourseStudent>();
            using (var context = new StudyContext())
            {
                courseStudents = context.CourseStudents.Where(p => p.CourseId == courseId).ToList();
                foreach (var courseStudent in courseStudents)
                {
                    context.CourseStudents.Remove(courseStudent);
                }

                foreach (var studentId in studentsId)
                {
                    context.CourseStudents.Add(new CourseStudent { CourseId = courseId, StudentId = studentId });
                }
                context.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (var context = new StudyContext())
            {
                context.Courses.Update(course);
                context.SaveChanges();
            }
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            using (var context = new StudyContext())
            {
                context.HomeTasks.Update(homeTask);
                context.SaveChanges();
            }
        }

        public void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            using (var context = new StudyContext())
            {
                foreach (var homeTaskAssessment in homeTaskHomeTaskAssessments)
                {
                    context.HomeTaskAssessments.Update(homeTaskAssessment);
                }
                context.SaveChanges();
            }
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            using (var context = new StudyContext())
            {
                context.Lecturers.Update(lecturer);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var context = new StudyContext())
            {
                context.Students.Update(student);
                context.SaveChanges();
            }
        }
    }
}
