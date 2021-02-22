﻿using DemoBranch.Webapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Appliction.Contracts
{
    public interface IDemoEventQuery
    {

        IQueryable<Guid> GetDistinctAggegateId();
        List<DemoEvent> GetEvents(Guid AggegateId);

    }
}