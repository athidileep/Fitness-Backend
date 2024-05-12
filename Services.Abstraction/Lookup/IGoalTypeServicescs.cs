using Domain.Models.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Lookup
{
    public interface IGoalTypeServicescs
    {
        Task<List<GoalTypeModel>> GetGoalType(int iGoalTypeModelId);
    }
}
