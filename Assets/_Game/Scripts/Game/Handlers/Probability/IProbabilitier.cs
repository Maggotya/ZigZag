using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.Game.Handlers.Probability
{
    interface IProbabilitier : IResetable
    {
        float successProbability { get; }
        bool IsSuccess();
    }
}
