using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarpedCityPackage{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float jumpForce;
        Rigidbody2D rgbd2D;

        private void Start() {
            rgbd2D = GetComponent<Rigidbody2D>();
        }

        public void Idle(){
            rgbd2D.velocity = new Vector2(0, 0);
        }

        public void OnMove(float axisX){
            rgbd2D.velocity = new Vector2(axisX, 0) * speed;
        }

        public void OnMoveLeft(float axisX){
            rgbd2D.velocity = new Vector2(axisX, 0) * speed;
        }

        public void OnMoveRight(float axisX){
            rgbd2D.velocity = new Vector2(axisX, 0) * speed;
        }

        public void OnJump(){
            rgbd2D.AddForce(Vector2.up * jumpForce);
        }

        public void OnFall(){

        }

        public void OnDuck(){
            rgbd2D.velocity = new Vector2(0, rgbd2D.velocity.y);
            rgbd2D.AddForce(Vector2.down * jumpForce);
        }
    }
}
