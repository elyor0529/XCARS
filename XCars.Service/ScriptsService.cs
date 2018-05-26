using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;
using System.Web;
using ExcelDataReader;
using System;

namespace XCars.Service
{
    public class ScriptsService : IScriptsService
    {
        public IAutoMakeService AutoMakeService { get; set; }
        public IAutoModelService AutoModelService { get; set; }
        public IAutoService AutoService { get; set; }

        public ScriptsService()
        {
        }

        public void UpdateMakes(IExcelDataReader reader)
        {
            reader.Read();

            while (reader.Read())
            {
                int makeID = 0;
                int.TryParse(reader[0].ToString(), out makeID);
                if (makeID <= 0)
                    continue;

                AutoMake make = AutoMakeService.GetByID(makeID);
                if (make == null)
                {
                    make = new AutoMake()
                    {
                        ID = makeID,
                        Name = reader[1].ToString(),
                        Name_ru = reader[2].ToString()
                    };

                    AutoMakeService.Create(make);
                }
                else
                {
                    make.Name = reader[1].ToString();
                    make.Name_ru = reader[2].ToString();

                    AutoMakeService.Edit(make);
                }
            }
        }

        public void UpdateModels(IExcelDataReader reader)
        {
            reader.Read();

            while (reader.Read())
            {
                int modelID = 0;
                int.TryParse(reader[0].ToString(), out modelID);

                int makeID = 0;
                int.TryParse(reader[1].ToString(), out makeID);
                AutoMake make = AutoMakeService.GetByID(makeID);

                if (modelID <= 0 || make == null)
                    continue;

                AutoModel model = AutoModelService.GetByID(modelID);
                if (model == null)
                {
                    model = new AutoModel()
                    {
                        ID = modelID,
                        MakeID = makeID,
                        Name = reader[2].ToString(),
                        Name_ru = reader[3].ToString()
                    };

                    AutoModelService.Create(model);
                }
                else
                {
                    model.MakeID = makeID;
                    model.Name = reader[2].ToString();
                    model.Name_ru = reader[3].ToString();

                    AutoModelService.Edit(model);
                }
            }
        }

        public void UpdateBothMakeAndModel(IExcelDataReader reader)
        {
            //clear all unused makes and models
            AutoModelService.DeleteUnused();
            //List<AutoModel> unusedModels = AutoModelService.GetAll().Where(model => model.Autoes.FirstOrDefault() == null).ToList();
            //for (int i = 0; i < unusedModels.Count; i++)
            //    AutoModelService.Delete(unusedModels[i]);

            AutoMakeService.DeleteUnused();

            //List<AutoMake> unusedMakes = AutoMakeService.GetAll().Where(make => make.Autoes.FirstOrDefault() == null && make.AutoModels.Count == 0).ToList();
            //for (int i = 0; i < unusedMakes.Count; i++)
            //    AutoMakeService.Delete(unusedMakes[i]);

            //insert new makes and models
            reader.Read();

            List<AutoMake> newMakes = new List<AutoMake>();
            List<AutoModel> newModels = new List<AutoModel>();

            int row = 0;
            try
            {
                while (reader.Read())
                {
                    row++;
                    if (reader[0] == null)
                        break;

                    int modelID = 0;
                    int.TryParse(reader[0].ToString(), out modelID);

                    int makeID = 0;
                    int.TryParse(reader[1].ToString(), out makeID);

                    if (modelID <= 0 || makeID <= 0)
                        return;


                    //AutoMake make = AutoMakeService.GetByID(makeID);
                    //if (make == null)
                    //{
                    AutoMake make = new AutoMake()
                    {
                        ID = makeID,
                        Name = reader[5].ToString(),
                        Name_ru = reader[6].ToString()
                    };

                    if (newMakes.FirstOrDefault(item => item.ID == makeID) == null)
                        newMakes.Add(make);
                    //AutoMakeService.Create(make);
                    //}

                    //AutoModel model = AutoModelService.GetByID(modelID);
                    //if (model == null)
                    //{
                    AutoModel model = new AutoModel()
                    {
                        ID = modelID,
                        MakeID = makeID,
                        Name = reader[3].ToString(),
                        Name_ru = reader[4].ToString()
                    };
                    if (newModels.FirstOrDefault(item => item.ID == modelID) == null)
                        newModels.Add(model);
                    //    //AutoModelService.Create(model);
                    //}
                }

                AutoMakeService.CreateMany(newMakes);
                AutoModelService.CreateMany(newModels);
            }
            catch (Exception ex)
            {

            }

            
        }
    }
}
