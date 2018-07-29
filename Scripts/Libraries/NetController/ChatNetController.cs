using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
public class ChatNetController : Singleton<ChatNetController>
{
    private string ip = "172.16.22.30";
    private int port = 1234;
    private Socket socket;
    private const int BUFFER_SIZE = 1024;
    private byte[] readBuff = new byte[BUFFER_SIZE];
    public string reveStr = "聊天:";


    public void ConnBtnClick(string text)
    {
        reveStr = "";
        //1
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //2
        socket.Connect(ip, port);
        //把自己的名字发送给服务器
        byte[] name = System.Text.Encoding.Default.GetBytes(text);
        socket.Send(name);
        //3
        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);
    }
    private void ReceiveCb(IAsyncResult ar)
    {
        //如何处理数据
        try
        {
            int count = socket.EndReceive(ar);
            string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
            if (str == "")
            {
                return;
            }
            else
            {
                reveStr += str + "\n";
            }

            socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCb, null);

        }
        catch (Exception e)
        {
            Debug.Log(e);
            socket.Close();
        }
    }
    public void SendBtnClick(string text)
    {
        //如何处理发送的数据
        if (text == "")
        {
            return;
        }
        byte[] str = System.Text.Encoding.Default.GetBytes(text);
        socket.Send(str);
    }

}
