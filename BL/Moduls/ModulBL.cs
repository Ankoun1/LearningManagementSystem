using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using DAL.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Modul;

namespace BL.Moduls
{
    public class ModulBL : IModulBL
    {
        private readonly AcademyDbContext data;

        public ModulBL(AcademyDbContext data)
        {
            this.data = data;
        }

        public bool CreateModul(int courseId, CreateModulFormModel modulModel)
        {
            if (data.Moduls.Any(x =>  x.Titel == modulModel.Titel))
            {
                return false;
            }

            data.Moduls.Add(new Modul { Titel = modulModel.Titel, Description = modulModel.Description, CourseId = courseId, IsDelited = false });
            data.SaveChanges();
            return true;
        }

        public bool DeleteModul(int modulId)
        {
            var modul = data.Moduls.Where(x => x.Id == modulId && x.IsDelited == false ).FirstOrDefault();
            if (modul == null)
            {
                return false;
            }

            modul.IsDelited = true;
            data.SaveChanges();

            return true;
        }

        public InformationModulFormModel DetailsModul(int modulId)
        {
            return data.Moduls.Where(x => x.Id == modulId && x.IsDelited == false).Select(x => new InformationModulFormModel {Id = x.Id, Titel = x.Titel, Description = x.Description }).FirstOrDefault();
        }

        public async Task<List<InformationModulFormModel>> IndexModul(int courseId)
        {
            return await data.Moduls.Where(x => x.CourseId == courseId && x.IsDelited == false).Select(x => new InformationModulFormModel {Id = x.Id, Titel = x.Titel, Description = x.Description }).ToListAsync();
        }

        public bool UpdateModul(int modulId, UpdateModulFormModel model)
        {
            if (!data.Moduls.Any(x => x.Id == modulId))
            {
                return false;
            }

            var modul = data.Moduls.Find(modulId);
            modul.Description = model.Description;
            data.SaveChanges();

            return true;
        }
    }
}
