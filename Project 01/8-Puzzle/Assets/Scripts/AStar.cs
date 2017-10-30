using System.Collections;
using System.Text;
using UnityEngine;

public class AStar
{
    ArrayList openList = new ArrayList();
    Queue closedList = new Queue();
    string initialState;
    string goalState;
    int heuristic;

    public AStar(string initialState, string goalState)
    {
        this.initialState = initialState;
        this.goalState = goalState;
    }

    public void Search(int heuristic)
    {
        this.heuristic = heuristic;
        openList.Add(new AStarNode(0, 
            CalculateH(initialState, goalState, heuristic),
            initialState, null, null, 0));
        
       

        while (!((AStarNode)openList[0]).State.Equals(goalState))
        {
            Debug.Log(((AStarNode)openList[0]).State);
            int lowestF = ((AStarNode)openList[0]).F;
            int expandIndex = 0;

            for(int i = 0; i < openList.Count; i++)
            {
                if (((AStarNode)openList[i]).F <= lowestF)
                    expandIndex = i;

            }

            //ExpandNode((AStarNode)openList[expandIndex]);
        }

        


        //        while()


        //        open < - []
        //        closed < - []
        //        next < -start

        //        while next is not goal {
        //                    add next to closed
        //            add successors of next not in closed to open
        //            remove next from open
        //            next < -select from open
        //}

        //        return next
    }

    //public void ExpandNode(AStarNode node)
    //{
    //    string nextState;

    //    switch(GetEmptyTileLocation(node.State))
    //    {
    //        case 0:
    //            nextState = GetNextState(node.State, 0, 1);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 0, 3);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 1:
    //            nextState = GetNextState(node.State, 1, 0);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 1, 2);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 1, 4);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 2:
    //            nextState = GetNextState(node.State, 2, 1);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 2, 5);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 3:
    //            nextState = GetNextState(node.State, 3, 0);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 3, 4);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 3, 6);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 4:
    //            nextState = GetNextState(node.State, 4, 1);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 4, 3);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 4, 5);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 4, 7);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 5:
    //            nextState = GetNextState(node.State, 5, 2);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 5, 4);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 5, 8);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "DOWN", node.Depth + 1));
    //            break;
    //        case 6:
    //            nextState = GetNextState(node.State, 6, 3);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 6, 7);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));
    //            break;
    //        case 7:
    //            nextState = GetNextState(node.State, 7, 4);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 7, 6);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));

    //            nextState = GetNextState(node.State, 7, 8);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "RIGHT", node.Depth + 1));
    //            break;
    //        case 8:
    //            nextState = GetNextState(node.State, 8, 5);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "UP", node.Depth + 1));

    //            nextState = GetNextState(node.State, 8, 7);
    //            openList.Add(new AStarNode(node.G + 1,
    //                CalculateH(nextState, goalState, heuristic),
    //                nextState, node, "LEFT", node.Depth + 1));
    //            break;

    //    }
    //}

    private string GetNextState(string state, int emptyTileIndex,
        int numberIndex)
    {
        char zero = '0';
        StringBuilder stringBuilder = new StringBuilder(state);
        stringBuilder[emptyTileIndex] = state[numberIndex];
        stringBuilder[numberIndex] = zero;
        

        return stringBuilder.ToString();
    }

    private int GetEmptyTileLocation(string puzzleState)
    {
        int index = 0;
        
        for(int i = 0; i < puzzleState.Length; i++)
        {
            if (puzzleState[i] == '0')
                index = i;
        }

        return index;
    }

    private int CalculateH(string puzzleState, string goalState, int heuristic)
    {
        int h = 0;

        switch(heuristic)
        {
            case 1:
                for (int i = 0; i < puzzleState.Length; i++)
                    if (!puzzleState[i].Equals(goalState[i]) && puzzleState[i] != '0')
                        h++;
                break;
            case 2:
                int index = 0;
                int[,] puzzleStateArray = new int[3, 3];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        puzzleStateArray[i, j] = int.Parse(puzzleState[index].ToString());
                    }
                }
                break;
            default:
                break;
        }

        return h;

    }
}