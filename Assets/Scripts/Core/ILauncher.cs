using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ainst.Core
{
    public interface ILauncher
    {
        void Launch();
        void ShutDown();
    }
}