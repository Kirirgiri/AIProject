using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze;
public class SpawnNPC : MonoBehaviour
{
    private int width;
    private int height;
    private int sizeMultipler;
    [SerializeField] private int numberOfNPC;
    [SerializeField] private GameObject NPC;
    public GameObject mazeCreator;

    void Start()
    {
        width = mazeCreator.GetComponent<MazeCreation>().mazeWidth;
        height = mazeCreator.GetComponent<MazeCreation>().mazeHeight;
        sizeMultipler = mazeCreator.GetComponent<MazeCreation>().mazeHeight;

        for (int i=0;i<numberOfNPC;i++){
            Instantiate(NPC,new Vector3(UnityEngine.Random.Range(0,width-1),0,UnityEngine.Random.Range(0,height-1)),Quaternion.identity);
        }
    }

}
