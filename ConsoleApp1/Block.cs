using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public string Data { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }

    public Block(int index, DateTime timestamp, string data, string previousHash)
    {
        Index = index;
        Timestamp = timestamp;
        Data = data;
        PreviousHash = previousHash;
        Hash = CalculateHash();
    }

    public string CalculateHash()
    {
        SHA256 sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes($"{Index}-{Timestamp}-{Data}-{PreviousHash}");
        byte[] outputBytes = sha256.ComputeHash(inputBytes);
        return Convert.ToBase64String(outputBytes);
    }
}