using System;
using System.Net.Sockets;

using UnityEngine;

using Zenject;

public class TCPCommunicationBusiness
{
    [Inject]
    private readonly Client client;

    public void Connect()
    {
        this.client.Tcp.Socket = new TcpClient
        {
            ReceiveBufferSize = this.client.DataBufferSize,
            SendBufferSize = this.client.DataBufferSize
        };

        this.client.Tcp.ReceiveBuffer = new byte[this.client.DataBufferSize];
    }

    public void ConnectCallback(IAsyncResult result)
    {
        this.client.Tcp.Socket.EndConnect(result);

        if (!this.client.Tcp.Socket.Connected)
        {
            return;
        }

        this.client.Tcp.Stream = this.client.Tcp.Socket.GetStream();

        this.client.Tcp.ReceivedData = new Packet();

        this.client.Tcp.Stream.BeginRead(this.client.Tcp.ReceiveBuffer, 0, this.client.DataBufferSize, this.ReceiveCallback, null);
    }

    public void Disconnect()
    {
        this.client.Tcp.Socket.Close();
    }

    public bool HandleData(byte[] data)
    {
        int packetLength = 0;

        this.client.Tcp.ReceivedData.SetBytes(data);

        if (this.client.Tcp.ReceivedData.UnreadLength() >= 4)
        {
            // If client's received data contains a packet
            packetLength = this.client.Tcp.ReceivedData.ReadInt();
            if (packetLength <= 0)
            {
                // If packet contains no data
                return true; // Reset receivedData instance to allow it to be reused
            }
        }

        while (packetLength > 0 && packetLength <= this.client.Tcp.ReceivedData.UnreadLength())
        {
            // While packet contains data AND packet data length doesn't exceed the length of the packet we're reading
            byte[] packetBytes = this.client.Tcp.ReceivedData.ReadBytes(packetLength);
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new Packet(packetBytes))
                {
                    int packetId = packet.ReadInt();
                    this.client.PacketHandlers[packetId](packet); // Call appropriate method to handle the packet
                }
            });

            packetLength = 0; // Reset packet length
            if (this.client.Tcp.ReceivedData.UnreadLength() >= 4)
            {
                // If client's received data contains another packet
                packetLength = this.client.Tcp.ReceivedData.ReadInt();
                if (packetLength <= 0)
                {
                    // If packet contains no data
                    return true; // Reset receivedData instance to allow it to be reused
                }
            }
        }

        if (packetLength <= 1)
        {
            return true; // Reset receivedData instance to allow it to be reused
        }

        return false;
    }

    public void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        this.SendData(packet);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int byteLength = this.client.Tcp.Stream.EndRead(result);
            if (byteLength <= 0)
            {
                this.Disconnect();
                return;
            }

            byte[] data = new byte[byteLength];
            Array.Copy(this.client.Tcp.ReceiveBuffer, data, byteLength);

            this.client.Tcp.ReceivedData.Reset(this.HandleData(data)); // Reset receivedData if all data was handled
            this.client.Tcp.Stream.BeginRead(this.client.Tcp.ReceiveBuffer, 0, this.client.DataBufferSize, this.ReceiveCallback, null);
        }
        catch
        {
            this.Disconnect();
        }
    }

    private void SendData(Packet packet)
    {
        try
        {
            if (this.client.Tcp.Socket != null)
            {
                this.client.Tcp.Stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null); // Send data to server
            }
        }
        catch (Exception ex)
        {
            ConnectionManager.Instance.ErrorText.text += ex.Message;
            Debug.Log($"Error sending data to server via TCP: {ex}");
        }
    }
}