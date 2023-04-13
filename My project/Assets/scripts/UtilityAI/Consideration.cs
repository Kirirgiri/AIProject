using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TL.Core
{
    public abstract class Consideration : ScriptableObject 
    // there can be different considerations for each action so it is not efficient to have it in the Action script itself
    {
        public string Name;
        private float _score; //need to have a score to consider a specific action
        public float score
        {
            get {return _score;}
            set{
                this._score = Mathf.Clamp01(value); //we need to normalize them to compare things across, its easier to compare
            }
        }

        public virtual void Awake()
        {
            score = 0;
        }
        public abstract float ScoreConsideration(NPCController npc); //all actions are scored the same way but considerations are not
    }
}
