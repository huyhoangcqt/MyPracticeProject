using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] Animator anim;

        public void Idle(){
            ResetAnimations();
            anim.SetBool("idle", true);
        }

        public void Move(){
            ResetAnimations();
            anim.SetBool("isRun", true);
        }

        public void MoveLeft(){
            ResetAnimations();
            anim.SetBool("isRun", true);
        }

        public void MoveRight(){
            ResetAnimations();
            anim.SetBool("isRun", true);
        }

        public void Jump(){
            ResetAnimations();
            anim.SetBool("isJump", true);
        }

        public void Fall(){
            ResetAnimations();
            anim.SetBool("isFall", true);
        }

        public void Shoot(){
            ResetAnimations();
            anim.SetBool("isShoot", true);
        }

        public void Duck(){
            ResetAnimations();
            anim.SetBool("isDuck", true);
        }

        private void ResetAnimations(){
            anim.SetBool("isRun", false);
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
            anim.SetBool("isShoot", false);
            anim.SetBool("isDuck", false);
            anim.SetBool("idle", false);
        }
    }
}
