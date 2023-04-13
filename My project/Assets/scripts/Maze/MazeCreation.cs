using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze{

    public class MazeCreation : MonoBehaviour
    {
        Stack<Pair<int, int>> myPairStack = new Stack<Pair<int, int>>();
        private int numberOfCellsVisited;
        public int mazeWidth;
        public int mazeHeight;
        public int[,] m_Maze;
        private int m_visitedCells;
        private int pointMultiplier = 5;
        public GameObject block;
        public GameObject wall;
        public GameObject wall1;

        void Start()
        {
            //create the size of the maze
            m_Maze = new int[mazeWidth,mazeHeight];
            for(int i=0;i<mazeHeight;i++)
            {
                Instantiate(wall,new Vector3(0,0,i),Quaternion.Euler(new Vector3(0,90,0)));
            }
            for(int i=0;i<mazeHeight;i++)
            {
                Instantiate(wall,new Vector3(mazeWidth,0,i),Quaternion.Euler(new Vector3(0,90,0)));
            }  
            for(int i=0;i<mazeWidth;i++)
            {
                Instantiate(wall1,new Vector3(i,0,mazeHeight),Quaternion.identity);
            } 
            for(int i=0;i<mazeWidth;i++)
            {
                Instantiate(wall1,new Vector3(i,0,0),Quaternion.identity);
            } 
            //create the first point of the maze
            int a = UnityEngine.Random.Range(5,mazeWidth);
            int b = UnityEngine.Random.Range(5,mazeHeight);
            Pair<int, int> myPair = new Pair<int, int>(a,b);
            myPairStack.Push(myPair);
            for (int i = 0; i < mazeWidth; i++)
            {
                for (int j = 0; j < mazeHeight; j++)
                {
                    m_Maze[i, j] = 0;
                    //Debug.Log(m_Maze[i,j]);
                }
            }
            m_Maze[a,b] = 1;
            m_visitedCells = 1;
            
            
            //example how to get a single value from an element in a stack
            //var secondFromTop = myPairStack.Peek().y;
        }

        private void Instantiate(GameObject wall, int i, int v1, int v2, Quaternion identity)
        {
            throw new System.NotImplementedException();
        }

        void Update()
        {
            //maze algorithm
            if(m_visitedCells<mazeHeight*mazeWidth)
            {
                //in case there are no cells in stack
                if(myPairStack.Count==0)
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
                //Debug.Log("working");
                var neighbours = new List<int>();
                //create a set of unvisited neighbours
                var _top = new int[]{myPairStack.Peek().x,myPairStack.Peek().y};
                //Debug.Log(_top[0]+","+_top[1]);
                //North
                if(_top[1]>0 && _top[0]!=mazeWidth)
                {
                    if(m_Maze[_top[0],_top[1]-1]==0)
                    {
                        neighbours.Add(0);
                    }
                }
                //East
                if(_top[0]<mazeWidth-1 &&_top[1]>=0)
                {
                    if(m_Maze[_top[0]+1,_top[1]]==0)
                        {
                            neighbours.Add(1);
                        }
                }
                //South
                if(_top[1]<mazeHeight-1 && _top[0]>=0)
                {
                    if(m_Maze[_top[0],_top[1]+1]==0)
                        {
                            neighbours.Add(2);
                        }
                }
                //West
                if(_top[0]>0 && _top[1]!=mazeHeight)
                {
                    if(m_Maze[_top[0]-1,_top[1]]==0)
                    {
                        neighbours.Add(3);
                    }
                }
                //are the neighbours available?
                if(neighbours.Count > 0)
                {
                    //randomly choose next cell
                    int _nextDir = UnityEngine.Random.Range(0,neighbours.Count);

                    Debug.Log(_nextDir+","+neighbours.Count);
                    //create a path between the previous and new cell
                    switch(_nextDir)
                    {
                        case 0:
                            if(_top[1]>0)
                            {
                                if(m_Maze[_top[0],_top[1]-1] == 0)
                                {
                                    m_Maze[_top[0],_top[1]-1] = 1;
                                    Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                
                                myPairStack.Push(new Pair<int, int>(_top[0],_top[1]-1)); 
                            }else{
                                myPairStack.Pop();
                            }
                            break;
                        case 1:   
                            if(_top[0]<mazeWidth-1)
                            {
                                if(m_Maze[_top[0]+1,_top[1]] == 0)
                                {
                                    m_Maze[_top[0]+1,_top[1]] =1;
                                    Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(1+_top[0],_top[1])); 
                            }else{
                                myPairStack.Pop();
                            }
                            break;
                        case 2:
                            if(_top[1]<mazeHeight-1)
                            {
                                if(m_Maze[_top[0],_top[1]+1] ==0)
                                {
                                    m_Maze[_top[0],_top[1]+1] = 1;
                                    Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(_top[0],_top[1]+1));   
                            } else{
                                myPairStack.Pop();
                            }
                            break;
                        case 3:
                            if(_top[0]>0)
                            {
                                if(m_Maze[_top[0]-1,_top[1]] ==0)
                                {
                                    m_Maze[_top[0]-1,_top[1]] = 1;
                                    Instantiate(block,new Vector3(myPairStack.Peek().x,0,myPairStack.Peek().y), Quaternion.identity);
                                    m_visitedCells++;
                                }
                                myPairStack.Push(new Pair<int, int>(-1+_top[0],_top[1]));  
                            }else{
                                myPairStack.Pop();
                            }
                            break;
                    }
                    
                }else{
                    myPairStack.Pop();
                    //no available neighbours so were going back
                }
            }
        }

        int RandomNumber(int endNumber)
        {
            int randomNumber = Random.Range(0,endNumber);
            return randomNumber;
        }
    }
}
