using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public abstract class PlayerState
    {
        protected abstract void Handle();
    }

    public class NormalState : PlayerState{
        protected override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ImortalState : PlayerState{
        protected override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DeadState : PlayerState{
        protected override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}