using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

namespace TL.Core
{
    public abstract class Action : ScriptableObject
    {
        public string Name;
        private float _score;
        public float score
        {
            get {return _score;}
            set{
                this._score = Mathf.Clamp01(value); //we need to normalize them to compare things across
            }
        }

        public Consideration[] consideration;//information about the world that says how urgent an action is

        public virtual void Awake()
        {
            score = 0;
        }

        public abstract void Execute(NPCController npc);
    }

}
