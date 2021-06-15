using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task CreateFishTypePricesAsync(List<FishTypeWithPriceApiModel> listType)
        {
            foreach (var obj in listType)
            {
                FishTypePrice ftp = new FishTypePrice()
                {
                    Date = obj.Date,
                    FishTypeID = obj.ID,
                    Price = obj.Price,
                };
                await _unitOfWork.FishTypePrices.CreateAsync(ftp);
            }
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
