using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

public class Packet : IDisposable
{
    private List<byte> buffer;

    private bool disposed = false;

    private byte[] readableBuffer;

    private int readPos;

    public Packet()
    {
        this.buffer = new List<byte>(); // Initialize buffer
        this.readPos = 0; // Set readPos to 0
    }

    public Packet(int id)
    {
        this.buffer = new List<byte>(); // Initialize buffer
        this.readPos = 0; // Set readPos to 0

        this.Write(id); // Write packet id to the buffer
    }

    public Packet(byte[] data)
    {
        this.buffer = new List<byte>(); // Initialize buffer
        this.readPos = 0; // Set readPos to 0

        this.SetBytes(data);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void InsertInt(int value)
    {
        this.buffer.InsertRange(0, BitConverter.GetBytes(value)); // Insert the int at the start of the buffer
    }

    public int Length()
    {
        return this.buffer.Count; // Return the length of buffer
    }

    public bool ReadBool(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            bool value = BitConverter.ToBoolean(this.readableBuffer, this.readPos); // Convert the bytes to a bool
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += 1; // Increase readPos by 1
            }
            return value; // Return the bool
        }
        else
        {
            throw new Exception("Could not read value of type 'bool'!");
        }
    }

    public byte ReadByte(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            byte value = this.readableBuffer[this.readPos]; // Get the byte at readPos' position
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += 1; // Increase readPos by 1
            }
            return value; // Return the byte
        }
        else
        {
            throw new Exception("Could not read value of type 'byte'!");
        }
    }

    public byte[] ReadBytes(int length, bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            byte[] value = this.buffer.GetRange(this.readPos, length).ToArray(); // Get the bytes at readPos' position with a range of length
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += length; // Increase readPos by length
            }
            return value; // Return the bytes
        }
        else
        {
            throw new Exception("Could not read value of type 'byte[]'!");
        }
    }

    public float ReadFloat(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            float value = BitConverter.ToSingle(this.readableBuffer, this.readPos); // Convert the bytes to a float
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += 4; // Increase readPos by 4
            }
            return value; // Return the float
        }
        else
        {
            throw new Exception("Could not read value of type 'float'!");
        }
    }

    public int ReadInt(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            int value = BitConverter.ToInt32(this.readableBuffer, this.readPos); // Convert the bytes to an int
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += 4; // Increase readPos by 4
            }
            return value; // Return the int
        }
        else
        {
            throw new Exception("Could not read value of type 'int'!");
        }
    }

    public long ReadLong(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            long value = BitConverter.ToInt64(this.readableBuffer, this.readPos); // Convert the bytes to a long
            if (moveReadPos)
            {
                // If moveReadPos is true
                this.readPos += 8; // Increase readPos by 8
            }
            return value; // Return the long
        }
        else
        {
            throw new Exception("Could not read value of type 'long'!");
        }
    }

    public Quaternion ReadQuaternion(bool moveReadPos = true)
    {
        return new Quaternion(this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos));
    }

    public short ReadShort(bool moveReadPos = true)
    {
        if (this.buffer.Count > this.readPos)
        {
            // If there are unread bytes
            short value = BitConverter.ToInt16(this.readableBuffer, this.readPos); // Convert the bytes to a short
            if (moveReadPos)
            {
                // If moveReadPos is true and there are unread bytes
                this.readPos += 2; // Increase readPos by 2
            }
            return value; // Return the short
        }
        else
        {
            throw new Exception("Could not read value of type 'short'!");
        }
    }

    public string ReadString(bool moveReadPos = true)
    {
        try
        {
            int length = this.ReadInt(); // Get the length of the string
            string value = Encoding.ASCII.GetString(this.readableBuffer, this.readPos, length); // Convert the bytes to a string
            if (moveReadPos && value.Length > 0)
            {
                // If moveReadPos is true string is not empty
                this.readPos += length; // Increase readPos by the length of the string
            }
            return value; // Return the string
        }
        catch
        {
            throw new Exception("Could not read value of type 'string'!");
        }
    }

    public Vector3 ReadVector3(bool moveReadPos = true)
    {
        return new Vector3(this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos), this.ReadFloat(moveReadPos));
    }

    public void Reset(bool shouldReset = true)
    {
        if (shouldReset)
        {
            this.buffer.Clear(); // Clear buffer
            this.readableBuffer = null;
            this.readPos = 0; // Reset readPos
        }
        else
        {
            this.readPos -= 4; // "Unread" the last read int
        }
    }

    public void SetBytes(byte[] data)
    {
        this.Write(data);
        this.readableBuffer = this.buffer.ToArray();
    }

    public byte[] ToArray()
    {
        this.readableBuffer = this.buffer.ToArray();
        return this.readableBuffer;
    }

    public int UnreadLength()
    {
        return this.Length() - this.readPos; // Return the remaining length (unread)
    }

    public void Write(byte value)
    {
        this.buffer.Add(value);
    }

    public void Write(byte[] value)
    {
        this.buffer.AddRange(value);
    }

    public void Write(short value)
    {
        this.buffer.AddRange(BitConverter.GetBytes(value));
    }

    public void Write(int value)
    {
        this.buffer.AddRange(BitConverter.GetBytes(value));
    }

    public void Write(long value)
    {
        this.buffer.AddRange(BitConverter.GetBytes(value));
    }

    public void Write(float value)
    {
        this.buffer.AddRange(BitConverter.GetBytes(value));
    }

    public void Write(bool value)
    {
        this.buffer.AddRange(BitConverter.GetBytes(value));
    }

    public void Write(string value)
    {
        this.Write(value.Length); // Add the length of the string to the packet
        this.buffer.AddRange(Encoding.ASCII.GetBytes(value)); // Add the string itself
    }

    public void Write(Vector3 value)
    {
        this.Write(value.x);
        this.Write(value.y);
        this.Write(value.z);
    }

    public void Write(Quaternion value)
    {
        this.Write(value.x);
        this.Write(value.y);
        this.Write(value.z);
        this.Write(value.w);
    }

    public void WriteLength()
    {
        this.buffer.InsertRange(0, BitConverter.GetBytes(this.buffer.Count)); // Insert the byte length of the packet at the very beginning
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.buffer = null;
                this.readableBuffer = null;
                this.readPos = 0;
            }

            this.disposed = true;
        }
    }
}