using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
    public class objectClass: MonoBehaviour
    {
         public class Puzzle
        {
            public string puzzle_name;
            public bool puzz_activation;

        public Puzzle(string p_nam, bool p_acti)
        {
            puzzle_name = p_nam;
            puzz_activation = p_acti;
        }
    }

    public Puzzle myPuzzle1 = new Puzzle("Puzzle 01", false);

    public class Clue
    {
        public string clue_name;
        public bool clue_activation;

        public Clue(string c_nam, bool c_acti)
        {
            clue_name = c_nam;
            clue_activation = c_acti;
        }
    }

    public Clue myClue1 = new Clue("Clue 01", false);

    public class Prop
    {
        public string prop_name;
        public bool prop_activation;
        public BoxCollider _col;

        public Prop(string pr_nam, bool pr_acti)
        {
            prop_name = pr_nam;
            prop_activation = pr_acti;
        }
    }

    public Prop myProp1 = new Prop ("Prop 01", false);

    // Start is called before the first frame update
    void Start()
        {
   
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
