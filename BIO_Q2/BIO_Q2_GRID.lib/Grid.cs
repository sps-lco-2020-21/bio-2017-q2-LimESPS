using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIO_Q2_GRID.lib
{
    public class Grid
    { //why did i need final node?
        int m1, m2;
        int boxFormed = 0;
        int turn_num;
        public LinkedList player_1,player_2;
        List<string> edge_pairs = new List<string>();
        int player1_boxCount = 0;
        int player2_boxCount = 0;
        List<int> left = new List<int> { 1, 7, 13, 19, 25, 31 };
        List<int> right = new List<int> { 6,12,18,24,30,36};
        //1 2 3 4 5 6 
        //7 8 9 10 11 12
        //13 14 15 16 17 18 etc
        //modifier - goes up, if not possible, player 1 tries posibilities in a clockwise direction, right,down,left            player 2 goes in anticlockwise direction
        public Grid(int pos1, int mod1, int pos2,int mod2, int number_of_turns)
        {
           
            m1 = mod1; //jump number, ie if player starts at 1 and has mod 5, next num has to be 6;
            m2 = mod2;
            player_1 = new LinkedList(pos1);
            player_2 = new LinkedList(pos2);
            turn_num = number_of_turns;
        }
        public void Play()
        {
            for(int i = 0; i <= turn_num; ++i)
            {
                int jump_val1 = player_1.jump(m1);
                Add_Edge(jump_val1,1);
                int jump_val2 = player_2.jump(m2);
                Add_Edge(jump_val2,2);

            }
            Box_Formed();
            Console.WriteLine(player1_boxCount);
            Console.WriteLine(player2_boxCount);
            Console.WriteLine("finished");
            Console.WriteLine(string.Join(",", edge_pairs)+ "!" +edge_pairs.Count());
            
        }
        public void Add_Edge(int val, int playerNum)
        {
            string result;
            string result2; //values switched to add to string list
            int up = val - 6;
            result = form_edge_pair(val, up,playerNum);
            result2 = form_edge_pair(up, val,playerNum);
            if (up > 6 && !EdgeAlreadyThere(result, result2))
            {
                Console.WriteLine("adding results" + result + result2);
                edge_pairs.Add(result);
                edge_pairs.Add(result2);

            }
            else
                secondPlan(playerNum,val);
                
        }
        public void secondPlan(int player_num,int val)
        {
            if(player_num == 1) //clockwise
            {
                AddEdgeSecond("right",val,player_num);
                AddEdgeSecond("down", val,player_num);
                AddEdgeSecond("left", val,player_num);
            }
            else if (player_num == 2)//anticlockwise
            {
                AddEdgeSecond("left", val,player_num);
                AddEdgeSecond("down", val,player_num);
                AddEdgeSecond("right", val,player_num);
            }
        }
        public void AddEdgeSecond(string entry,int val,int player_num)
        {
            if(entry == "right")
            {
                if (!edge_pairs.Contains(form_edge_pair(val + 1, val,player_num)) && !edge_pairs.Contains(form_edge_pair(val, val + 1,player_num))&& !right.Contains(val)) //can go right
                {
                    edge_pairs.Add(form_edge_pair(val, val + 1,player_num)); //still need to imolement boundaries, ie val+1 cant be 7,13,19,or more than 36
                    edge_pairs.Add(form_edge_pair(val + 1, val,player_num));
                }
            }
            if (entry == "down")
            {
                if (!edge_pairs.Contains(form_edge_pair(val +6, val,player_num)) && !edge_pairs.Contains(form_edge_pair(val, val + 6,player_num))&&(val+6)<=36) //can go right
                {
                    edge_pairs.Add(form_edge_pair(val, val + 1,player_num));
                    edge_pairs.Add(form_edge_pair(val + 1, val,player_num));
                }
            }
            if (entry == "left")
            {
                if (!edge_pairs.Contains(form_edge_pair(val -1, val,player_num)) && !edge_pairs.Contains(form_edge_pair(val, val -1,player_num))&&!left.Contains(val)) //can go right
                {
                    edge_pairs.Add(form_edge_pair(val, val + 1,player_num));
                    edge_pairs.Add(form_edge_pair(val + 1, val,player_num));
                }
            }
        }
        
        public string form_edge_pair(int val, int val2,int player_num) //includes adding nodes to the linked list, list
        {
            return val.ToString() + "-" + val2.ToString()+"-"+ player_num.ToString();

        }
        public bool EdgeAlreadyThere(string a,string b)
        {
            if (edge_pairs.Contains(a) || edge_pairs.Contains(b) || a == "empty")
                return true;
            else
                return false;
        }
        public void Box_Formed()
        {
            
            foreach(string item in edge_pairs)
            {
                List<int> pairs = (from num in item.Split('-') select int.Parse(num)).ToList();
                int firstval = pairs[1];
                int secondval = pairs[2];
                List<int> up_edge = search_list(firstval, secondval - 6); //position || playernum
                List<int> left_edge = search_list(firstval, secondval - 1);
                List<int> down_edge = search_list(firstval, secondval + 6);
                List<int> right_edge = search_list(firstval, secondval +1);
                if(up_edge.Count() == 2 && left_edge.Count() == 2 && down_edge.Count() == 2 && right_edge.Count() == 2) //box formed
                {
                    ++boxFormed;
                    List<int> positions = new List<int> { up_edge[0], left_edge[0], right_edge[0], down_edge[0] }; //up left right down
                    List<List<int>> edges = new List<List<int>>{ up_edge, left_edge, right_edge, down_edge };
                    int highest = positions.IndexOf(positions.Max());
                    int playerNum = edges[highest][1];
                    if (playerNum == 1)
                        ++player1_boxCount;
                    if (playerNum == 2)
                        ++player2_boxCount;
                }
            }
        }
        public List<int> search_list(int val,int val2)
        {
            List<int> found = new List<int>();
            foreach (string item in edge_pairs)
            {
                List<int> pairs = (from num in item.Split('-') select int.Parse(num)).ToList();
                int playernum = pairs[2];
                pairs.Remove(pairs[2]); //remove player number
                if (pairs.Contains(val) && pairs.Contains(val2))
                {
                    int position = edge_pairs.IndexOf(item);
                    found.Add(position);
                    found.Add(playernum);

                }
                
            }
            return found;
        }
        
    }


























    public class LinkedList //planned to use this for the edges however I found that it would require a lot more code, increasing the codes complexity.
    {
        Node headNode;
        public LinkedList(int val)
        {
            headNode = new Node(val);
        }
        public void Add(int val)
        {
            headNode.Add(val);
        }
        public void print()
        {
            headNode.print();
        }
        public int finalNode()
        {
            return headNode.finalNode();
        }
        public int jump(int mod)
        {
            int result = finalNode() + mod;
            if (result > 36)
                result -= 36;
            Add(result);
            return result;
        }
    }
    public class Node
    {
        Node _next;
        int value;
        public Node(int val)
        {
            _next = null;
            value = val;
        }
        
        public void Add(int val)
        {
            if (_next == null)
                _next = new Node(val);
            else
                _next.Add(val);
        }
        public void print()
        {
            Console.Write("|" + value + "->");
            if (_next != null)
                _next.print();
        }
        public int finalNode()
        {
            if (_next == null)
                return value;
            else
                return _next.finalNode();
        }
    }

}
