using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public enum EventID
    {
        OnIdle,
        
        OnMoveLeft,
        OnMoveRight,
        OnJump,
        OnMove,
        OnFall,
        OnShoot,
        OnDuck,

        OnBulletOut,
        OnBulletCollision,

        OnPlayerHit,
        OnEnemyHit,
        OnEnemyDestroy,
    }
}
