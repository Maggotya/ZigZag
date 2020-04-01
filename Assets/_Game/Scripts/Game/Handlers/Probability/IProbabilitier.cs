using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Game.Scripts.Game.Handlers.Probability
{
    interface IProbabilitier
    {
        float successProbability { get; }
        bool IsSuccess();
    }
}
