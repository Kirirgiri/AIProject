using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TL.Core
{
    public class AIBrain : MonoBehaviour
    {
        public bool finishDeciding {get; set;}
        public Action bestAction {get; set;}
        private NPCController npc;

        // Start is called before the first frame update
        void Start()
        {
            npc = GetComponent<NPCController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(bestAction is null)
            {
                DecideBestAction(npc.actionsAvailable);
            }
        }

        //loop through all the available actions
        //choose the highest scoring action
        public void DecideBestAction(Action[] actionsAvailable)
        {
            float score = 0f;
            int nextBestActionIndex = 0;
            for (int i=0; i<actionsAvailable.Length;i++)
            {
                if (ScoreAction(actionsAvailable[i]) > score)
                {
                    nextBestActionIndex = i;
                    score = actionsAvailable[i].score;
                }
            }

            bestAction = actionsAvailable[nextBestActionIndex];
            finishDeciding = true;
        }

        //loop through all the considerations of the action
        //score all the considerations
        //average the consideration scores==>overall action score
        public float ScoreAction(Action action)
        {
            float score = 1f;
            for (int i = 0; i < action.consideration.Length; i++)
            {
                float considerationScore = action.consideration[i].ScoreConsideration(npc);
                score *= considerationScore;

                if(score==0)
                {
                    action.score = 0;
                    return action.score; //no point in computing further
                }
            }

            //averaging scheme of overall score
            //referenced algorithm made by Dave Mark
            float originalScore = score;
            float modFactor = 1-(1 / action.consideration.Length);
            float makeupValue = (1-originalScore)*modFactor;
            action.score = originalScore+(makeupValue*originalScore);

            return action.score;
        }
    }
}
