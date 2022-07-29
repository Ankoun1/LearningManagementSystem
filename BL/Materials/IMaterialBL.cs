using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.Material;

namespace BL.Materials
{
    public interface IMaterialBL
    {
        bool CreateMaterial(int courseId,CreateMaterialFormModel materialModel);

        Task<List<InformationMaterialFormModel>> IndexMaterial(int courseId);

        InformationMaterialFormModel DetailsMaterial(int materialId);

        bool UpdateMaterial(int materialId, UpdateMaterialFormModel model);

        bool DeleteMaterial(int materialId);
    }
}
