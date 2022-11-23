using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//General: generate a VFX.  +  method;
//shooter => bullet moving
//Float => object moving;
//Fire => Special skill, combo vfx, wait time.
//buff/debuf => interact with other;

namespace WarpedCityPackage
{
    public class SkillEffectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] prefabs = new GameObject[4];

        public void OnShoot(object param, object param2){
            Vector3 srcPos = (Vector3) param;
            Vector3 desPos = (Vector3) param2;

            if (prefabs[0] == null) return;
            GameObject bullet = Instantiate(prefabs[0], srcPos, Quaternion.identity);
            BulletSetting(ref bullet, srcPos, desPos);
        }

        private void BulletSetting(ref GameObject vfx, Vector3 srcPos, Vector3 desPos){
            //TODO;

        }

        public void OnFlash(object param){ //lướt
            Vector3 srcPos = (Vector3) param;

            if (prefabs[1] != null){
                GameObject vfx = Instantiate(prefabs[1], srcPos, Quaternion.identity);
            };

            //Player Movement;
        }

        public void OnFire(){ //special skill
            if (prefabs[2] == null) return;
        }

        public void OnBuffDebuff(int type){
            switch (type){
                case 0:
                    Buff();
                    break;
                case 1:
                    Debuff();
                    break;
                default:
                    break;
            }
        }

        private void Buff(){
            if (prefabs[3] == null) return;
        }

        private void Debuff(){
            if (prefabs[3] == null) return;
        }
    }
}


