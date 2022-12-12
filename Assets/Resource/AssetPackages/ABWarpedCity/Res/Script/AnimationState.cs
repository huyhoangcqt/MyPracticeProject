using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage {
    public abstract class AnimationState{
        protected AnimationController m_animController;

        public AnimationState(){

        }

        public AnimationState(AnimationController animController){
            m_animController = animController;
        }

        public abstract AnimationState Idle();
        public abstract AnimationState MoveLeft();
        public abstract AnimationState MoveRight();
        public abstract AnimationState Jump();
        public abstract AnimationState Fall();
        public abstract AnimationState Shoot();
        public abstract AnimationState Duck();
    }

    public static class AnimationStateExtension {

        public static void Idle(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.Idle();
        }
        public static void MoveLeft(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.MoveLeft();
        }
        public static void MoveRight(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.MoveRight();
        }
        public static void Jump(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.Jump();
        }
        public static void Fall(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.Fall();
        }
        public static void Shoot(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.Shoot();
        }
        public static void Duck(this AnimationState animState ){
            Debug.Log("AnimationStateExtension");
            animState = animState.Duck();
        }
    }

    public class StateIdle : AnimationState{

        public StateIdle(AnimationController animController) : base(animController){

        }

        public override AnimationState Idle( ){
            m_animController.Idle();
            return this;
        }

        public override AnimationState MoveLeft(  ){
            StateRun stateRun = new StateRun(m_animController);
            return stateRun.MoveLeft();
        }

        public override AnimationState MoveRight(  ){
            StateRun stateRun = new StateRun(m_animController);
            return stateRun.MoveRight();
        }

        public override AnimationState Jump(  ){
            StateJump stateJump = new StateJump(m_animController);
            return stateJump.Jump();
        }

        public override AnimationState Fall(  ){
            StateFall stateFall = new StateFall(m_animController);
            return stateFall.Fall();
        }

        public override AnimationState Shoot(  ){
            StateShoot stateShoot = new StateShoot(m_animController);
            return stateShoot.Shoot();
        }

        public override AnimationState Duck(  ){
            StateDuck stateDuck = new StateDuck(m_animController);
            return stateDuck.Duck();
        }
    }

    public class StateRun : AnimationState{
        public StateRun(AnimationController animController) : base(animController){
            
        }

        public override AnimationState Idle(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Idle();
        }

        public override AnimationState MoveLeft(  ){
            m_animController.MoveLeft();
            return this;
        }
        
        public override AnimationState MoveRight(  ){
            m_animController.MoveRight();
            return this;
        }

        public override AnimationState Jump(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Jump();
        }

        public override AnimationState Fall(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Fall();
        }

        public override AnimationState Shoot(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Shoot();
        }

        public override AnimationState Duck(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Duck();
        }
    }

    public class StateDuck : AnimationState{
        public StateDuck(AnimationController animController) : base(animController){
            
        }

        public override AnimationState Idle(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Idle();
        }

        public override AnimationState MoveLeft(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.MoveLeft();
        }

        public override AnimationState MoveRight(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.MoveRight();
        }

        public override AnimationState Jump(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Jump();
        }
        public override AnimationState Fall(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Fall();
        }

        public override AnimationState Shoot(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Shoot();
        }

        public override AnimationState Duck(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Duck();
        }
    }

    public class StateJump : AnimationState{
        public StateJump(AnimationController animController) : base(animController){
            
        }
    
        public override AnimationState Idle(  ){
            return this;
        }

        public override AnimationState MoveLeft(  ){
            return this;
        }

        public override AnimationState MoveRight(  ){
            return this;
        }

        public override AnimationState Jump(  ){
            return this;
        }
        public override AnimationState Fall(  ){
            StateFall stateFall = new StateFall(m_animController);
            return stateFall.Fall();
        }

        public override AnimationState Shoot(  ){
            return this;
        }

        public override AnimationState Duck(  ){
            StateFall stateFall = new StateFall(m_animController);
            return stateFall.Fall();
        }
    }

    public class StateFall : AnimationState{
        private bool m_inProgress = false;
        public StateFall(AnimationController animController) : base(animController){
            
        }

        public override AnimationState Idle(  ){
            bool onGround = true;
            if (onGround){
                StateIdle stateIdle = new StateIdle(m_animController);
                return stateIdle.Idle();
            }
            else
                return this;
        }

        public override AnimationState MoveLeft(  ){
            return this;
        }

        public override AnimationState MoveRight(  ){
            return this;
        }

        public override AnimationState Jump(  ){
            return this;
        }

        public override AnimationState Fall(  ){
            if (!m_inProgress){
                m_animController.Fall();
                m_inProgress = true;
            }
            return this;
        }

        public override AnimationState Shoot(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Shoot();
        }

        public override AnimationState Duck(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Duck();
        }
    }

    public class StateShoot : AnimationState{
        public StateShoot(AnimationController animController) : base(animController){
            
        }

        public override AnimationState Idle(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Idle();
        }

        public override AnimationState MoveLeft(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.MoveLeft();
        }

        public override AnimationState MoveRight(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.MoveRight();
        }

        public override AnimationState Jump(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Jump();
        }
        public override AnimationState Fall(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Fall();
        }

        public override AnimationState Shoot(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Shoot();
        }

        public override AnimationState Duck(  ){
            StateIdle stateIdle = new StateIdle(m_animController);
            return stateIdle.Duck();
        }
    }
}