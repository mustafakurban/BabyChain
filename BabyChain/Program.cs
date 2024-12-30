using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();

            blockchain.AddBlock(new Block(1, DateTime.Now, "Block 1 Data", ""));
            blockchain.AddBlock(new Block(2, DateTime.Now, "Block 2 Data", ""));
            blockchain.AddBlock(new Block(3, DateTime.Now, "Block 3 Data", ""));
            blockchain.AddBlock(new Block(4, DateTime.Now, "Block 4 Data", ""));
            blockchain.AddBlock(new Block(5, DateTime.Now, "Block 5 Data", ""));
            blockchain.AddBlock(new Block(6, DateTime.Now, "Block 6 Data", ""));
            blockchain.AddBlock(new Block(7, DateTime.Now, "Block 7 Data", ""));
            blockchain.AddBlock(new Block(8, DateTime.Now, "Block 8 Data", ""));

            foreach (var block in blockchain.Chain)
            {
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Timestamp: {block.Timestamp}");
                Console.WriteLine($"Data: {block.Data}");
                Console.WriteLine($"Previous Hash: {block.PreviousHash}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine();
            }
        }
    }
}