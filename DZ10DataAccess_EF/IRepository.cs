using System.Collections.Generic;
using Models.Models;

namespace DZ10DataAccess_EF
{
    public interface IRepository
    {
        string ConnectionString { get; set; }

        Course CreateCourse(Course course);
        HomeTask CreateHomeTask(HomeTask homeTask, int courseId);
        void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        Lecturer CreateLecturer(Lecturer lecturer);
        Student CreateStudent(Student student);
        void DeleteCourse(int courseId);
        void DeleteHomeTask(int homeTaskId);
        void DeleteLecturer(int id);
        void DeleteStudent(int studentId);
        List<Course> GetAllCourses();
        List<Lecturer> GetAllLecturers();
        List<Student> GetAllStudents();
        Course GetCourse(int id);
        List<HomeTaskAssessment> GetHomeTaskAssessmentsByHomeTaskId(int homeTaskId);
        HomeTask GetHomeTaskById(int id);
        Lecturer GetLecturerById(int id);
        Student GetStudentById(int id);
        List<Student> GetStudentsByCourseId(int courseId);
        void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds);
        void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId);
        void UpdateCourse(Course course);
        void UpdateHomeTask(HomeTask homeTask);
        void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        void UpdateLecturer(Lecturer lecturer);
        void UpdateStudent(Student student);
    }
}