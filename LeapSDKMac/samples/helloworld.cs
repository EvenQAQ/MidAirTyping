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
        SafeWriteLine ("Frame available");
        SafeWriteLine ("Frame id: " + frame.Id
             + ", timestamp: " + frame.Timestamp
             + ", hands: " + frame.Hands.Count
             + ", fingers: " + frame.Fingers.Count
             + ", tools: " + frame.Tools.Count
             + ", gestures: " + frame.Gestures ().Count);
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