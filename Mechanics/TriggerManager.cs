using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{

    public static bool Stage1_ProgressCheck;
    public static bool Stage2_ProgressCheck;

    private void Start()
    {
      
    }

    //When the "Object" collides with the node (put down on the node), node gives feedback to player
    public void OnTriggerEnter(Collider other)
    {
        //if the puzzle put into the correct space, increase the puzzle progression counter
        if (other.name == "Puzzle 01")
        {
            Debug.Log("PZ1");
            ActivationManager.stage1_puzzle_count += 1;
            Debug.Log(ActivationManager.stage1_puzzle_count);
        }
        
        if (other.name == "Puzzle 02")
        {
            Debug.Log("PZ2");
            ActivationManager.stage1_puzzle_count += 1;
            Debug.Log(ActivationManager.stage1_puzzle_count);
        }

        if(other.name == "Special Puzzle 01")
        {
            Debug.Log("SP1");
            ActivationManager.stage1_puzzle_count += 1;
            Debug.Log(ActivationManager.stage1_puzzle_count);
        }

        if (other.name == "Puzzle 03")
        {
            Debug.Log("PZ3");
            ActivationManager.stage2_puzzle_count += 1;
            Debug.Log(ActivationManager.stage2_puzzle_count);
        }

        if(other.name == "Puzzle 04")
        {
            Debug.Log("PZ4");
            ActivationManager.stage2_puzzle_count += 1;
            Debug.Log(ActivationManager.stage2_puzzle_count);
        }

        if (other.name == "Special Puzzle 02")
        {
            Debug.Log("SP2");
            ActivationManager.stage2_puzzle_count += 1;
            Debug.Log(ActivationManager.stage2_puzzle_count);
        }
    }

    public void Update()
    {
        //when 1st stage puzzle progression is completed, return yes to activation manager for activating 2nd stage objects
        if (ActivationManager.stage1_puzzle_count == 3)
        {
            Debug.Log("Stage 1 End");
            Stage1_ProgressCheck = true;
        }

        //when 2nd stage puzzle progression is completed, return yes to activation manager for activating 3rd stage objects
        if (ActivationManager.stage2_puzzle_count == 3)
        {
            Debug.Log("Stage 2 End");
            Stage2_ProgressCheck = true;
        }


    }

}
