using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Course;

namespace BL.Courses
{
    public interface ICourseBL
    {
        bool CreateCourse(CreateCourseFormModel courseModel);

        Task<bool> UpdateCourse(int courseId,UpdateCourseFormModel courseModel);

        Task<List<InformationCourseFormModel>> IndexCorse(int userId);

        InformationCourseFormModel DetailsCourse(int userId,int courseId);

        bool RegisteredCourse(int studentId,int courseId);

        bool GenereteEndResultStudent(int courseId,int studentId);

        bool DeleteCourse(int courseId);
    }
}
