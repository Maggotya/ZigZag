using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityEngine
{
    static class Vector3Extensions
    {
        public static Vector3 NormalOnHorizontalPlane(this Vector3 host)
            => new Vector3(host.z, host.y, host.x);

        public static int MaxValueIndex(this Vector3 vector)
        {
            var indexCombinations = new (int, int, int)[] {
                (0, 1, 2), (1, 0, 2), (2, 0, 1)
            };

            foreach (var comb in indexCombinations)
                if (vector[comb.Item1] >= vector[comb.Item2] && vector[comb.Item1] >= vector[comb.Item3])
                    return comb.Item1;

            return 0;
        }
    }
}
