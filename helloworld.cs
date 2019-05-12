using System;
using System.Threading;
using Leap;

class mylistener : Listener
{
    private Object thisLock = new Object ();

    private void SafeWriteLine (String line)
    {
        lock (thisLock) {
                Console.WriteLine (line);
        }
    }

    public override void OnConnect (Controller controller)
    {
        SafeWriteLine ("Connected");
    }


    public override void OnFrame (Controller controller)
    {
        Frame frame = controller.Frame();
        HandList hands = frame.Hands;
        Hand rhand, lhand;
        if (hands[0].IsRight)
        {
            lhand = hands[1];
            rhand = hands[0];
        }
        else
        {
            lhand = hands[0];
            rhand = hands[1];
        } 
        Finger lindex = lhand.Fingers[1], rindex = rhand.Fingers[1];
        SafeWriteLine ("Frame available");
        // SafeWriteLine ("Frame id: " + frame.Id
        //      + ", timestamp: " + frame.Timestamp
        //      + ", hands: " + frame.Hands.Count
        //      + ", fingers: " + frame.Fingers.Count);

        SafeWriteLine("lhand: " + lhand.PalmPosition
             + ", lindex: " + lindex.ToString() + lindex.Type
             + ", tippos: " + lindex.TipPosition
             + ", stapos: " + lindex.StabilizedTipPosition
             + ", rhand: " + rhand.PalmPosition
             + ", rindex: " + rindex.ToString() + rindex.Type
             + ", tippos: " + rindex.TipPosition
             + ", stapos: " + rindex.StabilizedTipPosition);

        // foreach (Finger f in lfingers)
        // {
        //     Console.WriteLine(f.Type);
        // }
    }

}

class helloworld
{
    public static void Main ()
    {
        mylistener listener = new mylistener();
        Controller controller = new Controller();
        controller.AddListener(listener);

        // Keep this process running until Enter is pressed
        Console.WriteLine ("Press Enter to quit...");
        Console.ReadLine ();

        controller.RemoveListener(listener);
        controller.Dispose();
    }
}