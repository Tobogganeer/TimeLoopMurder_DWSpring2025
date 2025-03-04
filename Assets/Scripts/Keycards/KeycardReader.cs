using System;
using System.IO.Ports;

public class KeycardReader
{
    const int BaudRate = 115200;
    const int Payload_Bit1 = 16;
    const int Payload_Bit2 = 32;
    const int PayloadSize = 6;

    public event Action<uint> OnUIDRead;
    public bool Connected => arduino != null && arduino.IsOpen;
    public SerialPort UnderlyingPort => arduino;

    SerialPort arduino;
    byte[] readBuffer;

    public KeycardReader() : this(null) { }

    public KeycardReader(Action<uint> onUIDRead)
    {
        readBuffer = new byte[64]; // 64 should be more than enough

        if (onUIDRead != null)
            OnUIDRead += onUIDRead;

        TryConnect();
    }

    /// <summary>
    /// Try to connect to the Arduino
    /// </summary>
    /// <returns>True if a connection was established, whether open or not</returns>
    public bool TryConnect()
    {
        string port = GetArduinoPort();
        if (port == null)
            return false;

        arduino = new SerialPort(port, BaudRate);

        if (!arduino.IsOpen)
            arduino.Open();

        return true;
    }

    public void Close()
    {
        arduino?.Close();
    }

    /*
    private void SerialDataReceived(object o, SerialDataReceivedEventArgs args)
    {
        lock (lockObj)
        {
            dataReceivedFlag = true;
        }
    }
    */

    public void Tick()
    {
        if (!Connected)
            return;

        //Debug.Log(arduino.BytesToRead);

        int numBytesToRead = arduino.BytesToRead;

        if (numBytesToRead < PayloadSize)
            return;

        // Clamp to read buffer size
        if (numBytesToRead > readBuffer.Length)
            numBytesToRead = readBuffer.Length;

        int numReadBytes = arduino.Read(readBuffer, 0, numBytesToRead);
        arduino.DiscardInBuffer(); // Empty/flush out the rest of the buffer...
        // ... Hopefully? Maybe arduino.BaseStream.Flush()?

        // Search for the payload in the read buffer
        // Seems like we are receiving 4 payloads, each with 1 more byte than the last
        // i.e. the final payload has all 4 bytes we want
        // Loop backwards and pick the last payload with all the data
        // numReadBytes - 2 to start at second-last element
        for (int i = numReadBytes - 2; i >= 0; i--)
        {
            // We found our UID!!!
            if (readBuffer[i] == Payload_Bit1 && readBuffer[i + 1] == Payload_Bit2)
            {
                // Read the UID (4 bytes)
                uint uid = BitConverter.ToUInt32(readBuffer, i + 2);
                OnUIDRead?.Invoke(uid);
                //Debug.Log(Convert.ToString(uid, 2));
                // We found the UID, return and don't read any more
                return;
            }
        }
    }

    string GetArduinoPort()
    {
        string[] ports = SerialPort.GetPortNames();

        if (ports == null || ports.Length == 0)
            return null; // No COM ports found

        return ports[^1];
    }
}
