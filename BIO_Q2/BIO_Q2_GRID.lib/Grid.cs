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
        int width = 6;
        int height = 6;
        int turn_num;
        public LinkedList player_1,player_2;
        List<string> edge_pairs = new List<string>();
        List<string> all = new List<string>(); //just to check my filter works
        
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
                Add_Edge(jump_val1);
                int jump_val2 = player_2.jump(m2);
                Add_Edge(jump_val2);

            }
            player_1.print();
            player_2.print();
            Console.WriteLine(string.Join(",", edge_pairs)+ "!" +edge_pairs.Count());
            Console.WriteLine(all.Count()); //126, much higher than edge_pairs list
        }
        public void Add_Edge(int val)
        {
            string result;
            string result2; //values switched to add to string list
            int up = val - 6;
            if (up > 6)
            {
                result = form_edge_pair(val, up);
                result2 = form_edge_pair(up, val);
     
                if (!EdgeAlreadyThere(result, result2)) //tested, should work as only 24 (acc 12) from each player is added, if end does not work check again
                {
                    Console.WriteLine("adding results" + result + result2);
                    edge_pairs.Add(result);
                    edge_pairs.Add(result2);
                }
                all.Add(result);
                all.Add(result2);


            }
            
                
        }
        public void secondPlan(int player_num)
        {

        }
        public string form_edge_pair(int val, int val2)
        {
            return val.ToString()+"-"+val2.ToString();
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
            //convert to bool
        }
        
    }
    public class LinkedList
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
