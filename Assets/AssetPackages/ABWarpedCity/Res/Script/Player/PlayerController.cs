using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public class PlayerController : Listener
    {
        [SerializeField] PlayerMovement m_movementController;
        [SerializeField] AnimationController m_animController;
        [SerializeField] SkillEffectSpawner vfxSpawner;
        PlayerState currenState;
        AnimationState animationState;

        protected override void Awake() {
            base.Awake();
            animationState = new StateIdle(m_animController);            
        }

        protected override void RegisterListener(){
            this.RegisterListener(EventID.OnMoveLeft, (param) => OnMoveLeft(param));
            this.RegisterListener(EventID.OnMoveRight, (param) => OnMoveRight(param));
            this.RegisterListener(EventID.OnJump, (param) => OnJump());
            this.RegisterListener(EventID.OnShoot, (param) => OnShoot(param));
            this.RegisterListener(EventID.OnDuck, (param) => OnDuck());
            // this.RegisterListener(EventID.OnMove, (param) => OnMove(param));
            this.RegisterListener(EventID.OnIdle, (param) => Idle());
        }

        // private void OnMove(object param){
        //     Debug.Log("OnMove");
        //     m_movementController.OnMove((float) param);
        //     m_animController.Move();
        // }

        private void Idle(){
            m_movementController.Idle();
            animationState.Idle();
        }

        private void OnMoveLeft(object param){
            Debug.Log("OnMoveLeft"); 
            m_movementController.OnMoveLeft((float) param);        
            // m_animController.MoveLeft();   
            animationState.MoveLeft();     
        }

        private void OnMoveRight(object param){
            Debug.Log("OnMoveRight");
            m_movementController.OnMoveRight((float) param);
            // m_animController.MoveRight();
            animationState.MoveRight();
        }

        private void OnJump(){
            Debug.Log("OnJump");
            m_movementController.OnJump();
            // m_animController.Jump();
            animationState.Jump();
        }

        private void OnShoot(object param){
            Debug.Log("OnShoot");
            Vector3 desPos = (Vector3) param;
            vfxSpawner.OnShoot((object)transform.position, (object)desPos);
            // m_animController.Shoot();
            animationState.Shoot();
        }

        private void OnDuck(){
            Debug.Log("OnDuck");
            m_movementController.OnDuck();
            // m_animController.Duck();
            animationState.Duck();
        }

        protected override void OnDestroy(){
            base.OnDestroy();
        }

        protected override void RemoveListener()
        {
            this.RemoveListener(EventID.OnMoveLeft, (param) => OnMoveLeft(param));
            this.RemoveListener(EventID.OnMoveRight, (param) => OnMoveRight(param));
            this.RemoveListener(EventID.OnJump, (param) => OnJump());
            this.RemoveListener(EventID.OnShoot, (param) => OnShoot(param));
            this.RemoveListener(EventID.OnDuck, (param) => OnDuck());
            // this.RemoveListener(EventID.OnMove, (param) => OnMove(param));
            this.RemoveListener(EventID.OnIdle, (parma) => Idle());
        }
    }
}
