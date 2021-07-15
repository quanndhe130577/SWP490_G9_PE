using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task CreateListTransactionAsync(CreateListTransactionModel apiModel, int wcId)
        {
            foreach (var item in apiModel.ListTraderId)
            {
                var listRole = await _unitOfWork.UserInfors.GetRolesAsync(item);
                if (listRole.Contains(RoleName.Trader))
                {
                    await _unitOfWork.Transactions.CreateAsync(new Transaction()
                    {
                        TraderId = item,
                        WeightRecorderId = wcId,
                        Date = apiModel.Date
                    });
                }
                else
                {
                    throw new Exception("Thông tin thương lái chưa chính xác !!!");
                }


            }

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<TransactionResModel>> GetAllTransactionAsync(int wcId, DateTime? date)
        {
            var listTran = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == wcId).OrderByDescending(x => x.Date).ToList();
            if (date != null)
            {
                listTran = listTran.Where(x => x.Date == date.Value).ToList();
            }

            List<TransactionResModel> list = new();
            foreach (var item in listTran)
            {
                TransactionResModel tran = new TransactionResModel();
                tran.ID = item.ID;
                tran.Date = item.Date;
                tran.Trader = _mapper.Map<UserInfor, TraderInformation>(await _unitOfWork.UserInfors.FindAsync(item.TraderId));

                list.Add(tran);
            }

            return list;
        }
    }
}
