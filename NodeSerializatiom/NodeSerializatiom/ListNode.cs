using System;
using System.Collections.Generic;
using System.IO;

namespace NodeSerializatiom
{
    class ListNode

    {
        public ListNode Prev;

        public ListNode Next;

        public ListNode Rand; // произвольный элемент внутри списка

        public string Data;
    }


    class ListRand

    {
        public ListNode Head;

        public ListNode Tail;

        public int Count;

        public void Serialize(FileStream s)
        {
            var nodeList = new List<ListNode>();
            var currentNode = Head;
            while (currentNode != null)
            {
                nodeList.Add(currentNode);
                currentNode = currentNode.Next;
            }

            if (nodeList.Count != Count)
            {
                throw new Exception("Internal structure of ListRand object and Count are not equal");
            }

            using (BinaryWriter writer = new BinaryWriter(s))
            {
                writer.Write(Count);
                foreach (var node in nodeList)
                {
                    writer.Write(node.Data);
                    int pos = -1;
                    if (node.Rand != null)
                    {
                        pos = nodeList.IndexOf(node.Rand);
                    }
                    writer.Write(pos);
                }
            }
        }

        public void Deserialize(FileStream s)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(s))
                {
                    Count = reader.ReadInt32();
                    var nodeList = new List<ListNode>(Count);
                    for (int i = 0; i < Count; i++)
                    {
                        nodeList.Add(new ListNode());
                    }
                    for (int i = 0; i < Count; i++)
                    {
                        nodeList[i].Prev = i > 0 ? nodeList[i - 1] : null;
                        nodeList[i].Next = i < (Count - 1) ? nodeList[i + 1] : null;
                        nodeList[i].Data = reader.ReadString();
                        int randIndex = reader.ReadInt32();
                        nodeList[i].Rand = randIndex >= 0 ? nodeList[randIndex] : null;
                    }
                    Head = nodeList[0];
                    Tail = nodeList[Count - 1];
                }
            }
            catch (Exception ex) when (ex is EndOfStreamException || ex is ObjectDisposedException || ex is IOException)
            {
                Count = 0;
                Head = null;
                Tail = null;
            }
        }
    }
}