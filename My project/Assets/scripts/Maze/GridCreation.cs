using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze{
    public class GridCreation : MonoBehaviour
    {
        [Tooltip("The Maze Size")]
            private int mazeWidth;
            private int mazeHeight;
            private int sizeMultipler;
            public int[,] m_Maze;
            private int m_visitedCells;
            private GameObject wall;

        // Start is called before the first frame update
        void Start()
        {
            wall = gameObject.GetComponent<MazeCreation>().wall;
            mazeHeight = gameObject.GetComponent<MazeCreation>().mazeHeight;
            mazeWidth = gameObject.GetComponent<MazeCreation>().mazeWidth;
            sizeMultipler = gameObject.GetComponent<MazeCreation>().sizeMultipler;
            m_Maze = new int[mazeWidth,mazeHeight];

            for(int i=0;i<mazeWidth;i++)
            {
                for(int j=0;j<mazeHeight;j++)
                {
                    Instantiate(wall,new Vector3(i*sizeMultipler,0,(j-0.5f)*sizeMultipler),Quaternion.identity);
                    Instantiate(wall,new Vector3((i+0.5f)*sizeMultipler,0,j*sizeMultipler),Quaternion.Euler(new Vector3(0,90,0)));
                }
            }
            for(int i=0;i<mazeHeight;i++)
            {
                Instantiate(wall,new Vector3(-0.5f*sizeMultipler,0,i*sizeMultipler),Quaternion.Euler(new Vector3(0,90,0)));
            }
            for(int i=0;i<mazeWidth;i++)
            {
                Instantiate(wall,new Vector3(i*sizeMultipler,0,(mazeHeight-0.5f)*sizeMultipler),Quaternion.identity);
            }
            
        }
    }
}


