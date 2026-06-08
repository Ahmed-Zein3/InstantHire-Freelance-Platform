namespace InstantHire.Services;

public class InputService
{
    // =========================
    // Read string input
    // =========================
    public string ReadString(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }

    // =========================
    // Read decimal input
    // =========================
    public decimal ReadDecimal(string message)
    {
        Console.Write(message);
        return decimal.Parse(Console.ReadLine()!);
    }

    // =========================
    // Read int input
    // =========================
    public int ReadInt(string message)
    {
        Console.Write(message);
        return int.Parse(Console.ReadLine()!);
    }
}