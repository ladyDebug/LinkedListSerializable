using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NodeSerializatiom
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var first = new ListNode
            {
                Data = "firstObj"

            };
            var second = new ListNode
            {
                Data = "secondObj",
                Prev = first,
                Rand = first

            };
            first.Next = second;
            var third = new ListNode
            {
                Data = "thirdObj",
                Prev = second,
                Rand = first,
                

            };

            second.Next = third;

            var fourth = new ListNode
            {
                Data = "4Obg",
                Prev = third,
                Rand = second

            };
            third.Next = fourth;

            ListRand rand = new ListRand
            {
                Count = 4,
                Head = first,
                Tail = fourth
            };

            

            using (FileStream fStream = new FileStream("user.dat",
                FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                rand.Serialize(fStream);
            }

            var res = new ListRand();
            using (FileStream fStream = new FileStream("user.dat",
                FileMode.Open, FileAccess.Read, FileShare.None))
            {
                
                res.Deserialize(fStream);

            }
            
        }
    }
}
