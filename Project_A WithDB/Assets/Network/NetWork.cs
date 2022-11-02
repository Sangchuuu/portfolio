using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class NetWork
{
    SocketAsyncEventArgs m_recieve_event_args;
    LinkedList<Packet> m_recive_packet_list;

    Socket m_socket;
    public NetWork()
    {

    }

    public void Connect(string adress, int port)
    {
        //tcp통신으로 연결
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //버퍼에 데이터를 쌓아서 한번에 전송하는게 아니라, 그때그때 전송.
        m_socket.NoDelay = true;

        //연결할 서버의 ip 포트설정
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(adress), port);

        SocketAsyncEventArgs event_arg = new SocketAsyncEventArgs();
        event_arg.Completed += OnConnected;
        event_arg.RemoteEndPoint = endpoint;

        bool pending = m_socket.ConnectAsync(event_arg);

        if (!pending)
            OnConnected(null, event_arg);
    }

    void OnConnected(object sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError == SocketError.Success)
        {
            Debug.Log("연결성공");
        }
        else
        {
            Debug.Log("연결실패");
        }
    }

    //메시지전송
    public void Send(Packet packet)
    {
        if (m_socket == null || !m_socket.Connected)
            return;

        SocketAsyncEventArgs send_event_args = new SocketAsyncEventArgs();
        if(send_event_args == null)
        {
            Debug.Log("SocketAsyncEventArgsPool::Pop() result is null");
            return;
        }

        send_event_args.Completed += onSendComplected;
        send_event_args.UserToken = this;

        byte[] send_data = packet.GetSendBytes();
        send_event_args.SetBuffer(send_data, 0, send_data.Length);

        bool pending = m_socket.SendAsync(send_event_args);
        if (!pending)
            onSendComplected(null, send_event_args);
    }

    public void onSendComplected(object sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError == SocketError.Success)
        {
            Debug.Log("연결 성공");
        }
        else
            Debug.Log("연결실패");

        e.Completed -= onSendComplected;
    }
}
