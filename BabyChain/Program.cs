using System;


namespace BabyChainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Node node1 = new("Node1");
            Node node2 = new("Node2");

            node1.StartListening(5000);
            node2.StartListening(5001);

            node1.ConnectToPeer("127.0.0.1:5001");
            node2.ConnectToPeer("127.0.0.1:5000");

            node1.Blockchain.AddBlock(new Block(1, DateTime.Now, "Block 1 Data", ""));
            node1.Broadcast("BLOCK:" + "Block 1 Data");

            Console.ReadLine();
        }

    }
}