using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public abstract class MovementSate{
        protected abstract void Handle();
    }

    public class OnGroundState : MovementSate{
        protected override void Handle(){
            throw new System.NotImplementedException();
        }
    }

    public class OnSkyState : MovementSate{
        protected override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}
