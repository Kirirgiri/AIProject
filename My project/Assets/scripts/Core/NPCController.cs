using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TL.Core
{
    public class NPCController : MonoBehaviour
    {
        public MoveController mover{get; set;}
        public AIBrain aiBrain {get; set;}
        public NPCInventory Inventory { get; set; }
        public Stats stats { get; set; }
        public Action[] actionsAvailable;
        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<MoveController>();
            aiBrain = GetComponent<AIBrain>();
            Inventory = GetComponent<NPCInventory>();
            stats = GetComponent<Stats>();
        }

        // Update is called once per frame
        void Update()
        {
            if(aiBrain.finishDeciding)
            {
                aiBrain.finishDeciding = false;
                aiBrain.bestAction.Execute(this);//dependency injection
            }
        }

        public void OnFinishedAction()
        {
            aiBrain.DecideBestAction(actionsAvailable);
        }

        #region Coroutine

        public void DoWork(int time)
        {
            StartCoroutine(WorkCoroutine(time));
        }

        public void DoSleep(int time)
        {
            StartCoroutine(SleepCoroutine(time));
        }

        IEnumerator WorkCoroutine(int time)
        {
            int counter = time;
            while (counter>0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }
            Debug.Log("Harvested 1 resource");
            //logic to update inventory

            Inventory.AddResource(ResourceType.wood, 10);

            OnFinishedAction();
        }

        IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter>0)
            {
                yield return new WaitForSeconds(1);
                counter--;
            }
            Debug.Log("Gained 1 energy");
            //logic to update energy

            stats.energy += 1;
            OnFinishedAction();
        }

        #endregion
    }
}
