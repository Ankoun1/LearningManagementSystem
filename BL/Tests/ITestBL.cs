using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Test;

namespace BL.Tests
{
    public interface ITestBL
    {
        bool CreateTest(int courseId, CreateTestFormModel testModel);

        Task<List<InformationTestFormModel>> IndexTest(int courseId);

        InformationTestFormModel DetailsTest(int testId);        

        bool DeleteTest(int testId);

        bool AssessmentTest(int testId,int studentId);
    }
}
