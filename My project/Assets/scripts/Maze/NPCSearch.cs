using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze{
    public class NPCSearch : MonoBehaviour
    {
        private enum sides{
            moving,
            searching
        }
        private enum directions{
            right,
            left,
            up,
            down
        }
        private Dictionary<Vector3, Vector3> cameFrom;
        private float speed = 5f;
        [SerializeField] private sides action;
        [SerializeField] private directions dir;
        void Update()
        {
            if(action == sides.moving){
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }else{
                gameObject.transform.position += Vector3.back*speed*Time.deltaTime;
            }
/*             if(dir==directions.right){
                transform.position += Vector3.right*speed*Time.deltaTime;
            }else if(dir==directions.left)
            {
                transform.position += Vector3.left*speed*Time.deltaTime;
            }else if(dir==directions.up){
                gameObject.transform.position += Vector3.forward*speed*Time.deltaTime;
            }else if(dir==directions.down){
                gameObject.transform.position += Vector3.back*speed*Time.deltaTime;
            } */

        }
/*         void OnCollisionEnterEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("wall")){
                int rnd = UnityEngine.Random.Range(0,3);
                switch(rnd){
                    case 0:
                        dir = directions.right;
                        break;
                    case 1:
                        dir = directions.left;
                        break;
                    case 2:
                        dir = directions.up;
                        break;
                    case 3:
                        dir=directions.down;
                        break;
                }
            }
        } */
        void OnCollisionEnter(Collision collision){
            if(collision.gameObject.CompareTag("wall"))
            {
                Debug.Log("FSDGSDG");
                action = sides.searching;
            }
        }
        void OnCollisionExit(Collision collision)
        {
                int rnd = UnityEngine.Random.Range(0,1);
                switch (rnd){
                    case 0:
                    gameObject.transform.Rotate(0,90,0);
                    //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,gameObject.transform.rotation.y+90,0));
                    break;
                    case 1:
                    gameObject.transform.Rotate(0,-90,0);
                    //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,gameObject.transform.rotation.y-90,0));
                    break;
                }
                action=sides.moving;
                //Invoke("ChangeStatus",1f);
        }

        void ChangeStatus()
        {
            action = sides.moving;
        }
    }
}

