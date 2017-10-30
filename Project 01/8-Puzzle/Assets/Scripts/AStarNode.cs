public class AStarNode
{
    private int f; // Estimated cost of the cheapest solution through n.
    private int g; // The cost to reach the node.
    private int h; // The cost to get from the node to the goal.
    private string state;
    private AStarNode parent;
    private string action;
    private int depth;
    
    public AStarNode(int g, int h, string state, AStarNode parent,
        string action, int depth)
    {
        this.F = g + h;
        this.G = g;
        this.H = h;
        this.State = state;
        this.Parent = parent;
        this.Action = action;
        this.Depth = depth;
    }

    public string State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }

    public int F
    {
        get
        {
            return f;
        }

        set
        {
            f = value;
        }
    }

    public int G
    {
        get
        {
            return g;
        }

        set
        {
            g = value;
        }
    }

    public int H
    {
        get
        {
            return h;
        }

        set
        {
            h = value;
        }
    }

    public AStarNode Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public string Action
    {
        get
        {
            return action;
        }

        set
        {
            action = value;
        }
    }

    public int Depth
    {
        get
        {
            return depth;
        }

        set
        {
            depth = value;
        }
    }
}
