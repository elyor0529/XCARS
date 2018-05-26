using XCars.Model;
using System.Collections.Generic;
using System.Web;
using ExcelDataReader;

namespace XCars.Service.Interfaces
{
    public interface IScriptsService
    {
        void UpdateMakes(IExcelDataReader reader);
        void UpdateModels(IExcelDataReader reader);
        void UpdateBothMakeAndModel(IExcelDataReader reader);
    }
}
