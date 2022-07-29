using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Material;

namespace BL.Materials
{
    public class MaterialBL : IMaterialBL
    {
        private readonly AcademyDbContext data;

        public MaterialBL(AcademyDbContext data)
        {
            this.data = data;
        }
        public bool CreateMaterial(int courseId, CreateMaterialFormModel materialModel)
        {
            if(data.Materials.Any(x => x.Type == materialModel.Type || x.Link == materialModel.Link))
            {
                return false;
            }

            data.Materials.Add(new DAL.Repository.Models.Material { Type = materialModel.Type, Link = materialModel.Link, CourseId = courseId, IsDelited = false });
            data.SaveChanges();
            return true;
        }

        public bool DeleteMaterial(int materialId)
        {
            var material = data.Materials.Where(x => x.Id == materialId && x.IsDelited == false).FirstOrDefault();
            if (material == null)
            {
                return false;
            }

            material.IsDelited = true;
            data.SaveChanges();

            return true;
        }

        public InformationMaterialFormModel DetailsMaterial(int materialId)
        {
            return data.Materials.Where(x => x.Id == materialId && x.IsDelited == false).Select(x => new InformationMaterialFormModel {Id = x.Id, Type = x.Type, Link = x.Link }).FirstOrDefault();
        }

        public async Task<List<InformationMaterialFormModel>> IndexMaterial(int courseId)
        {
            return await data.Materials.Where(x => x.CourseId == courseId && x.IsDelited == false).Select(x => new InformationMaterialFormModel {Id = x.Id, Type = x.Type,Link = x.Link }).ToListAsync();
        }

        public bool UpdateMaterial(int materialId, UpdateMaterialFormModel model)
        {
            if(!data.Materials.Any(x => x.Id == materialId))
            {
                return false;
            }

            var material = data.Materials.Find(materialId);
            material.Link = model.Link;
            data.SaveChanges();

            return true;
        }
    }
}
