using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public enum Dir
    {
        N = 0b0001,
        E = 0b0010,
        S = 0b0100,
        W = 0b1000
    }

    public void SetRisk(int maxRisk)
    {
        // Set risk based on distance to the boss room.
        foreach(Room r in rooms)
        {
            if (r.type == Room.Type.NORMAL)
            {
                int risk = maxRisk - (Mathf.Abs(boss.getX() - r.getX()) + Mathf.Abs(boss.getY() - r.getY()));
                if (risk <= 0) risk = 1;
                r.setRisk(risk);
            }
        }
    }

    private List<Room> colourRooms;

    private int getDistance(Room r1, Room r2)
    {
        return (Mathf.Abs(r1.getX() - r2.getX()) + Mathf.Abs(r1.getY() - r2.getY()));
    }

    private Room GetNearest(Room target, List<Room> checks)
    {
        Room nearest = checks[0];
        int minDist = getDistance(target, checks[0]);

        foreach(Room r in checks)
        {
            if (getDistance(target, r) < minDist)
            {
                nearest = r;
                minDist = getDistance(target, r);
            }
        }
        return nearest;
    }

    private List<Dir> CarveDirections(Room r1, Room r2)
    {
        List<Dir> output = new List<Dir>();

        if(r1.getX() > r2.getX())
        {
            output.Add(Dir.W);
        }
        else if(r1.getX() < r2.getX())
        {
            output.Add(Dir.E);
        }

        if(r1.getY() > r2.getY())
        {
            output.Add(Dir.S);
        }

        if(r1.getY() < r2.getY())
        {
            output.Add(Dir.N);
        }

        return output;
    }

    private void CarvePath(Room r1, Room r2)
    {
        List<Dir> dirs = CarveDirections(r1, r2);
        Room r = r1;
        while(dirs.Count > 0)
        {
            Dir d = dirs[Random.Range(0, dirs.Count)];
            r.AddExit(d);
            r = GetRoom(r, d);
            r.AddExit(opposites[d]);
            dirs = CarveDirections(r, r2);
        }
    }

    public void AddCycles()
    {
        List<Room> rs = new List<Room>
            {
                start, boss
            };
        foreach (Room r in colourRooms)
        {
            //Room near = GetNearest(r, rs);
            CarvePath(r, start);
        }
    }

    public void PopulateColourRooms(int count)
    {
        for(int i = 0; i<count; i++)
        {
            int y = Random.Range(0, size);
            int x = Random.Range(0, size);

            Room r = GetRoom(x, y);
            if(r.type == Room.Type.NORMAL)
            {
                r.type = Room.Type.COLOUR;
                colourRooms.Add(r);
            } else
            {
                i--;
            }
        }
    }

    private System.Tuple<int, int> GetDelta(Dir d)
    {
        int x = 0;
        int y = 0;

        if(d == Dir.N)
        {
            y += 1;
        } else if(d == Dir.S)
        {
            y -= 1;
        }

        if(d==Dir.E)
        {
            x += 1;
        } else if(d==Dir.W)
        {
            x -= 1;
        }

        return new System.Tuple<int, int>(x, y);
    }

    public List<Dir> allDirs()
    {
        return new List<Dir>()
        {
            Dir.E, Dir.N, Dir.S, Dir.W
        };
    }

    private Dictionary<Dir, Dir> opposites;

    private Room[,] rooms;

    private Room start;
    private Room boss;
    private int size;

    public Room GetStart() { return start; }

    public Maze()
    {
        //this.size = size;
        //rooms = new Room[size, size];
        opposites = new Dictionary<Dir, Dir>
        {
            { Dir.N, Dir.S },
            { Dir.S, Dir.N },
            { Dir.E, Dir.W },
            { Dir.W, Dir.E }
        };
        colourRooms = new List<Room>();
    }

    public List<Room> GetRooms()
    {
        List<Room> output = new List<Room>();

        foreach(Room r in rooms)
        {
            output.Add(r);
        }

        return output;
    }
   

    private void FillRooms()
    {
        for(int i = 0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                rooms[i, j] = new Room(j, i);
            }
        }
    }

    public void Create(int size)
    {
        this.size = size;
        rooms = new Room[size, size];
        FillRooms();
        GrowingTree();
        debug();
    }

    private void debug()
    {
        //foreach(Room r in rooms)
        //{
        //    //Debug.Log(r.GetExits());
        //}
    }

    /*
     * Remove any dead-ends
     */
    public void RemoveDeadEnds()
    {
        foreach(Room r in rooms)
        {
            if (r.ExitCount() == 1 && r.type == Room.Type.NORMAL)
                removePath(r);
        }

    }

    /*
     * Remove the path from a room with a single exit
     */
    private void removePath(Room r)
    {
        if (r.ExitCount() != 1) return;

        Stack<Room> rs = new Stack<Room>();
        rs.Push(r);

        while(rs.Count > 0)
        {
            Room s = rs.Pop();
            if(s.ExitCount() == 1 && s.type == Room.Type.NORMAL)
            {
                Dir d = getExits(s)[0];
                Room t = GetRoom(s, d);
                s.RemoveExit(d);
                t.RemoveExit(opposites[d]);
                rs.Push(t);
            }
        }
    }


    private Dir getRandomDir()
    {
        List<Dir> dirs = allDirs();
        return dirs[Random.Range(0, dirs.Count)];
    }

    private Dir getRestrictedDir(Dir d)
    {
        List<Dir> dirs = allDirs();
        dirs.Remove(d);
        return dirs[Random.Range(0, dirs.Count)];
    }

    public void MakeBoss(int distance)
    {
        Room r = FindWithDist(start, distance);
        boss = r;
        r.type = Room.Type.BOSS;
    }

    public void MakeRandomBoss()
    {
        bool done = false;
        Room r = null;
        while(!done)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);
            r = GetRoom(x, y);
            if (r.type == Room.Type.NORMAL)
                done = true;
        }
        boss = r;
        r.type = Room.Type.BOSS;
    }

    /*
     * Find a room 'distance' steps away
     */
    private Room FindWithDist(Room r, int distance)
    {
        Stack<System.Tuple<Room, int, List<Dir>>> values = new Stack<System.Tuple<Room, int, List<Dir>>>();
        values.Push(System.Tuple.Create(r, 0, getExits(r)));

        while(values.Count>0)
        {
            System.Tuple<Room, int, List<Dir>> t = values.Peek();
            if (t.Item2 == distance && t.Item1.type == Room.Type.NORMAL)
                return t.Item1;

            if (t.Item3.Count == 0)
                values.Pop();
            else
            {
                Dir d = getRandom(t.Item3);
                t.Item3.Remove(d);
                Room newR = GetRoom(t.Item1, d);
                List<Dir> exits = getExits(newR);
                exits.Remove(opposites[d]);
                values.Push(System.Tuple.Create(newR, t.Item2 + 1,exits));
            }
        }

        return null;
        
    }

    private List<Dir> getExits(Room r)
    {
        List<Dir> output = new List<Dir>();
        if (r.HasExit(Dir.N)) output.Add(Dir.N);
        if (r.HasExit(Dir.S)) output.Add(Dir.S);
        if (r.HasExit(Dir.E)) output.Add(Dir.E);
        if (r.HasExit(Dir.W)) output.Add(Dir.W);
        return output;
    }

    private Room FindRoomWithDist(Room r, int depth)
    {
        if(depth>0)
        {
            foreach(Dir d in getExits(r))
            {
                Room output = FindRecursive(r, depth - 1, d);
                if(output!=null)
                {
                    return output;
                }
            }
        }
        return r;
    }

    private Room FindRecursive(Room r, int depth, Dir d)
    {
        if(depth == 0)
        {
            return r;
        } else
        {
            List<Dir> dirs = allDirs();
            dirs.Remove(opposites[d]);
        }
        return null;
    }

    private Dir getRandom(List<Dir> ts)
    {
        return ts[Random.Range(0, ts.Count)];
    }

    private Room findNeighbour(Room r, List<Dir> dirs)
    {
        bool picked = false;
        Room output = null;
        //List<Dir> dirs = allDirs();
        while(!picked)
        {
            int index = Random.Range(0, dirs.Count);
            Dir d = dirs[index];
            dirs.RemoveAt(index);
            if(r.HasExit(d))
            {
                output = GetRoom(r, d);
                picked = true;
            }
        }
        // Return the relevant room
        return output;
    }

    private Dir RandomDir(List<Dir> dirs)
    {
        return dirs[Random.Range(0, dirs.Count)];
    }

    public int getSize() { return size; }

    public Room GetRoom(Room r, Dir d)
    {
        System.Tuple<int, int> delta = GetDelta(d);
        int x = r.getX() + delta.Item1;
        int y = r.getY() + delta.Item2;

        return GetRoom(x, y);
    }

    public Room GetRoom(int x, int y)
    {
        //Debug.Log(x + " : " + y);
        return rooms[y, x];
    }

    private void GrowingTree()
    {
        // Choose random start point
        //int asize = size - 1;
        int startx = Random.Range(0, size);
        int starty = Random.Range(0, size);
        start = GetRoom(startx, starty);
        start.type = Room.Type.START;
        List<Room> myRooms = new List<Room>();
        myRooms.Add(GetRoom(startx, starty));

        //Need to store the position of each cell

        //Looping component
        while (myRooms.Count > 0)
        {
            //Debug.Log(myRooms.Count);
            int index = ChooseIndex(myRooms.Count);
            Room r = myRooms[index];
            List<Dir> dirs = new List<Dir>(){Dir.W, Dir.S, Dir.N, Dir.E};
            bool found = false;
            while (dirs.Count > 0)
            {
                //Debug.Log("Dirs : " + dirs.Count);
                int dirindex = Random.Range(0, dirs.Count);

                Dir d = dirs[dirindex];
                dirs.RemoveAt(dirindex);
                System.Tuple<int, int> delta = GetDelta(d);
                int nx = r.getX() + delta.Item1;
                int ny = r.getY() + delta.Item2;
                //Debug.Log("nx = " + nx + " / ny = " + ny);

                if(nx >=0 && ny >=0 
                    && nx < size && ny < size)
                {
                    Room r2 = GetRoom(nx, ny);
                    if (r2.ExitCount() == 0)
                    {
                        //Debug.Log("Added room");
                        // Found an unvisited room
                        r.AddExit(d);
                        r2.AddExit(opposites[d]);
                        myRooms.Add(r2);
                        found = true;
                        break;
                    }
                }
            }
            if(!found)
            {
                myRooms.RemoveAt(index);
            }
        }
    }

    private int ChooseIndex(int max)
    {
        // Currently recursive backtracker
        //return max - 1;
        //return 0;
        //Alternate between prim and backtracker
        int random = Random.Range(0, 2);
        //Debug.Log(random);
        if (random == 0)
        {
            //Debug.Log("Backtrack");
            return max - 1;
        }
        else
        {
            //Debug.Log("Random");
            return Random.Range(0, max);
        }
    }

    
}
