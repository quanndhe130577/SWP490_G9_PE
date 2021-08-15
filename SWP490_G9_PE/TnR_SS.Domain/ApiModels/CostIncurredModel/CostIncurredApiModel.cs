using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.CostIncurredModel
{
    public class CostIncurredApiModel
    {
        public int ID { get; set; }

        //[Required(ErrorMessage = "Loại chi phí không được để trống!")]
        public string TypeOfCost { get; set; }

        //[Required(ErrorMessage = "Tên chi phí không được để trống!")]
        public string Name { get; set; }

        //[RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Chi phí không hợp lệ")]
        //[Required(ErrorMessage = "Chi phí không được để trống!")]
        public double Cost { get; set; }

        public string Note { get; set; }

        //[Required(ErrorMessage = "Ngày không được để trống!")]
        public DateTime Date { get; set; }

        
    }
}
