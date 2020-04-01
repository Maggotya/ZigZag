using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Game.Scripts.Game.Objects.FallArea
{
    interface IFallArea
    {
        GameObject objectMustFall { get; }
        UnityEvent onObjectFallen { get; }
    }
}
