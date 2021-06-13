﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class PurchaseDetailRepository : RepositoryBase<PurchaseDetail>, IPurchaseDetailRepository
    {
        public PurchaseDetailRepository(TnR_SSContext context) : base(context) { }
    }
}
