﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypeModel
{
    public class WeightRecorderGetAllFishtypeResModel
    {
        public int ID { get; set; }
        public string FishName { get; set; }
        public string Description { get; set; }
        public float MinWeight { get; set; }
        public float MaxWeight { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double TransactionPrice { get; set; }
    }
}
