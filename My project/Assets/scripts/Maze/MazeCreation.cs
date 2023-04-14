using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze{

    public class MazeCreation : MonoBehaviour
    {
        Stack<Pair<int, int>> myPairStack = new Stack<Pair<int, int>>();
        private int numberOfCellsVisited;
        [Tooltip("The Maze Size")]
        public int mazeWidth;
        public int mazeHeight;
        public int[,] m_Maze;
        public int sizeMultipler;
        private int m_visitedCells;
        
        [Tooltip("Objects used to generate the maze")]
        public GameObject block;
        public GameObject wall;

        void Start()
        {
            //create the size of the maze
            m_Maze = new int[mazeWidth,mazeHeight];

            //create the first point of the maze
            int a = UnityEngine.Random.Range(0,mazeWidth);
            int b = UnityEngine.Random.Range(0,mazeHeight);
            Pair<int, int> myPair = new Pair<int, int>(a,b);
            myPairStack.Push(myPair);
            for (int i = 0; i < mazeWidth; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    m_Maze[i, j] = 0;
                }
            }
            m_Maze[a,b] = 1;
            m_visitedCells = 1;
            
            //example how to get a single value from an element in a stack
            //var secondFromTop = myPairStack.Peek().y;
        }

        void Update()
        {
            //maze algorithm
            if(m_visitedCells<mazeHeight*mazeWidth)
            {
                //in case there are no cells in stack
                if(myPairStack.Count==0)
                    {
                        CreateAnotherFrontier(); 
                    }
                    
                var neighbours = new List<int>();
                //create a set of unvisited neighbours
                var _top = new int[]{myPairStack.Peek().x,myPairStack.Peek().y};

                SearchNeighbours(_top[0],_top[1],neighbours);

                //are the neighbours available?
                if(neighbours.Count > 0)
                {
                    //randomly choose next cell
                    int _nextDir = UnityEngine.Random.Range(0,neighbours.Count);

                    //Debug.Log(_nextDir+","+neighbours.Count);
                    
                    //create a path between the previous and new cell
                    switch(neighbours[_nextDir])
                    {
                        case 0:
                                if(m_Maze[_top[0],_top[1]-1] == 0)
                                {
                                    m_Maze[_top[0],_top[1]-1] = 1;
                                    //Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.Euler(0,90,0));
                                    Instantiate(block,new Vector3(myPairStack.Peek().x*sizeMultipler,0,(myPairStack.Peek().y-0.5f)*sizeMultipler), Quaternion.Euler(0,90,0));
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(_top[0],_top[1]-1)); 
                            break;
                        case 1:   
                                if(m_Maze[_top[0]+1,_top[1]] == 0)
                                {
                                    m_Maze[_top[0]+1,_top[1]] =1;
                                    //Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    Instantiate(block,new Vector3((myPairStack.Peek().x+0.5f)*sizeMultipler,0,myPairStack.Peek().y*sizeMultipler), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(1+_top[0],_top[1])); 
                            break;
                        case 2:
                                if(m_Maze[_top[0],_top[1]+1]==0){
                                    m_Maze[_top[0],_top[1]+1] = 1;
                                    //Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.Euler(0,90,0));
                                    Instantiate(block,new Vector3(sizeMultipler*myPairStack.Peek().x,0,(myPairStack.Peek().y+0.5f)*sizeMultipler), Quaternion.Euler(0,90,0));
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(_top[0],_top[1]+1));   
                            break;
                        case 3:
                                if(m_Maze[_top[0]-1,_top[1]] ==0)
                                {
                                    m_Maze[_top[0]-1,_top[1]] = 1;
                                    //Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    Instantiate(block,new Vector3((myPairStack.Peek().x-0.5f)*sizeMultipler,0,myPairStack.Peek().y*sizeMultipler), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(-1+_top[0],_top[1]));  
                            break;
                    }
                    
                }else{
                    myPairStack.Pop();
                    //no available neighbours so were going back
                }
            }
        }

        private void CreateAnotherFrontier()
        {
            for (int i = 0; i < mazeWidth; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    if(m_Maze[i,j] == 0)
                    {
                        m_Maze[i,j] = 1;
                        myPairStack.Push(new Pair<int, int>(i,j));
                        break;
                    }
                }
            }
        }
        
        void SearchNeighbours(int a, int b, List<int> neighbours)
        {
            var _top = new int[]{a,b};
                //North
                if(b>0 && m_Maze[a,b-1]==0)
                {
                    neighbours.Add(0);
                }
                //East
                if(a<mazeWidth-1 && m_Maze[a+1,b]==0)
                {
                    neighbours.Add(1);
                }
                //South
                if(b<mazeHeight-1 && m_Maze[a,b+1]==0)
                {
                    neighbours.Add(2);
                }
                //West
                if(a>0 && m_Maze[a-1,b]==0)
                {
                    neighbours.Add(3);
                }
        }
    }
}
