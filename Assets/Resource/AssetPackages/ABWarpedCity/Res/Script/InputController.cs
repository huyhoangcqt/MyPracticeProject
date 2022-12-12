using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public class InputController : MonoBehaviour
    {
        private void Update() {
            RunningDectection();
            JumpDetection();
            ShootDetection();
            DuckDectection();
        }

        public void RunningDectection(){
            float axisX = Input.GetAxisRaw("Horizontal");
            if (axisX != 0){
                if (axisX < 0){
                    this.PostEvent(EventID.OnMoveLeft, axisX);
                }
                else {
                    this.PostEvent(EventID.OnMoveRight, axisX);
                }
                return;
            }
            this.PostEvent(EventID.OnIdle);
        }

        public void JumpDetection(){
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)){
                this.PostEvent(EventID.OnJump);
            }
        }

        public void ShootDetection(){
            if (Input.GetMouseButtonDown(0)){
                Vector3 clickedPos = Input.mousePosition;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(clickedPos);
                this.PostEvent(EventID.OnShoot, worldPos);
            }
            if (Input.GetMouseButtonUp(0)){
                this.PostEvent(EventID.OnIdle);
                //PostEvent Release Mouse 0
            }
        }

        public void DuckDectection(){
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
                this.PostEvent(EventID.OnDuck);
                return;
            }
        }
    }
}
