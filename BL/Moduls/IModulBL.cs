using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Modul;

namespace BL.Moduls
{
    public interface IModulBL
    {
        bool CreateModul(int courseId, CreateModulFormModel modulModel);

        Task<List<InformationModulFormModel>> IndexModul(int courseId);

        InformationModulFormModel DetailsModul(int modulId);

        bool UpdateModul(int modulId, UpdateModulFormModel model);

        bool DeleteModul(int modulId);
    }
}
