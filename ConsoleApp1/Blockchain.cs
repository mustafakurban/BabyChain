using System;
using System.Collections.Generic;

public class Blockchain
{
    public List<Block> Chain { get; set; }

    public Blockchain()
    {
        Chain = new List<Block>();
        Chain.Add(CreateGenesisBlock());
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTime.Now, "Genesis Block", "0");
    }

    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    public void AddBlock(Block newBlock)
    {
        newBlock.PreviousHash = GetLatestBlock().Hash;
        newBlock.Hash = newBlock.CalculateHash();
        Chain.Add(newBlock);
    }
}