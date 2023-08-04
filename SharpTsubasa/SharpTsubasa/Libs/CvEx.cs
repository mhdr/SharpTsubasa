using AdvancedSharpAdbClient;
using OpenCvSharp;

namespace SharpTsubasa.Libs;

public class CvEx
{
    public static FindResult Find(string templateNumber, double threshold = 0.9)
    {
        FindResult result = new FindResult();
        var src = Cv2.ImRead("screenshot.png");
        var tmp = Cv2.ImRead($"templates/{templateNumber}.png");
        var r = src.MatchTemplate(tmp, TemplateMatchModes.CCoeffNormed);
        double minVal, maxVal;
        Point minLoc, maxLoc;
        r.MinMaxLoc(out minVal, out maxVal, out minLoc, out maxLoc);

        result.Score = maxVal;

        if (maxVal >= threshold)
        {
            result.IsFound = true;
        }

        if (result.IsFound)
        {
            var width = tmp.Width;
            var height = tmp.Height;
            result.Start = maxLoc;

            int X2 = maxLoc.X + width;
            int Y2 = maxLoc.Y + height;
            result.End = new Point(X2, Y2);

            int X3 = Convert.ToInt32(maxLoc.X + width / 2);
            int Y3 = Convert.ToInt32(maxLoc.Y + height / 2);
            result.ClickPoint = new Point(X3, Y3);

            Console.WriteLine("template: {0}, point: {1}, score: {2}", templateNumber, maxLoc, maxVal);
        }

        return result;
    }

    public static FindResult Find2(byte[] data, string templateNumber, double threshold = 0.9)
    {
        FindResult result = new FindResult();
        var src = Cv2.ImDecode(data, ImreadModes.Color);
        var tmp = Cv2.ImRead($"templates/{templateNumber}.png", ImreadModes.Color);
        var r = src.MatchTemplate(tmp, TemplateMatchModes.CCoeffNormed);
        double minVal, maxVal;
        Point minLoc, maxLoc;
        r.MinMaxLoc(out minVal, out maxVal, out minLoc, out maxLoc);

        result.Score = maxVal;

        if (maxVal >= threshold)
        {
            result.IsFound = true;
        }

        if (result.IsFound)
        {
            var width = tmp.Width;
            var height = tmp.Height;
            result.Start = maxLoc;

            int X2 = maxLoc.X + width;
            int Y2 = maxLoc.Y + height;
            result.End = new Point(X2, Y2);

            int X3 = Convert.ToInt32(maxLoc.X + width / 2);
            int Y3 = Convert.ToInt32(maxLoc.Y + height / 2);
            result.ClickPoint = new Point(X3, Y3);

            Console.WriteLine("template: {0}, point: {1}, score: {2}", templateNumber, maxLoc, maxVal);
        }

        return result;
    }
}