using OpenCvSharp;

namespace SharpTsubasa.Libs;

public class FindResult
{
    public bool IsFound { get; set; } = false;
    public Point Start { get; set; }
    public Point End { get; set; }
    public double Score { get; set; }
    public Point ClickPoint { get; set; }
}