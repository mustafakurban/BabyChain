using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Node
{
    public string NodeId { get; set; }
    public List<string> Peers { get; set; } = new List<string>();
    public Blockchain Blockchain { get; set; }

    public Node(string nodeId)
    {
        NodeId = nodeId;
        Blockchain = new Blockchain(); // Initialize the blockchain
    }

    public void ConnectToPeer(string peerAddress)
    {
        if (!Peers.Contains(peerAddress))
        {
            Peers.Add(peerAddress);
        }
    }

    public void Broadcast(string message)
    {
        foreach (var peer in Peers)
        {
            SendMessage(peer, message);
        }
    }

    public void SendMessage(string peerAddress, string message)
    {
        var parts = peerAddress.Split(':');
        string ip = parts[0];
        int port = int.Parse(parts[1]);

        using (var client = new TcpClient(ip, port))
        {
            var stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }

    public void StartListening(int port)
    {
        Task.Run(() =>
        {
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Node {NodeId} is listening on port {port}...");

            while (true)
            {
                using (var client = listener.AcceptTcpClient())
                {
                    var stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    HandleReceivedMessage(message);
                }
            }
        });
    }

    public void HandleReceivedMessage(string message)
    {
        if (message.StartsWith("BLOCK"))
        {
            // Parse and add the block
            string blockData = message.Substring(6);
            Block receivedBlock = DeserializeBlock(blockData);
            if (ValidateBlock(receivedBlock))
            {
                Blockchain.AddBlock(receivedBlock);
                Console.WriteLine($"Block added: {receivedBlock.Index}");
            }
        }
        else if (message.StartsWith("REQUEST_CHAIN"))
        {
            // Send the entire blockchain to the requester
            Broadcast("CHAIN:" + SerializeChain());
        }
        else if (message.StartsWith("CHAIN"))
        {
            // Synchronize the chain
            string chainData = message.Substring(6);
            List<Block> receivedChain = DeserializeChain(chainData);
            if (ValidateChain(receivedChain))
            {
                Blockchain.Chain = receivedChain;
                Console.WriteLine("Blockchain synchronized.");
            }
        }
    }
    private bool ValidateBlock(Block block)
    {
        // Validate block structure, hash, and linkage
        return block.PreviousHash == Blockchain.GetLatestBlock().Hash;
        
    }

    private string SerializeChain()
    {
        // Serialize the blockchain
        return JsonSerializer.Serialize(Blockchain.Chain);
    }

    private List<Block> DeserializeChain(string chainData)
    {
        // Deserialize the chain
        return JsonSerializer.Deserialize<List<Block>>(chainData) ?? [];
    }

    // Serialize the block
    public string SerializeBlock(Block block)
    {
        return JsonSerializer.Serialize(block);
    }

    // Deserialize the block
    private Block DeserializeBlock(string blockData)
    {
        return JsonSerializer.Deserialize<Block>(blockData) ?? new Block(0, DateTime.Now, "", "0");
    }

    private bool ValidateChain(List<Block> chain)
    {
        // Validate the entire chain (similar to IsChainValid)
        return true; // Placeholder
    }
}
