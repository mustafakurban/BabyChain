﻿using System;


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

            node1.Blockchain.AddBlock(new Block(3, DateTime.Now, "{sender: 'Alice', receiver: 'Charlie', amount: 100}", ""));
            node1.Broadcast("BLOCK:" + node1.SerializeBlock(node1.Blockchain.GetLatestBlock()));

            // Wait for the block to be added
            System.Threading.Thread.Sleep(10);

            node2.Blockchain.AddBlock(new Block(3, DateTime.Now, "{sender: 'Bob', receiver: 'Dave', amount: 50}", ""));
            node2.Broadcast("BLOCK:" + node2.SerializeBlock(node2.Blockchain.GetLatestBlock()));

            Console.ReadLine();
        }

    }
}