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
        //tcp������� ����
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //���ۿ� �����͸� �׾Ƽ� �ѹ��� �����ϴ°� �ƴ϶�, �׶��׶� ����.
        m_socket.NoDelay = true;

        //������ ������ ip ��Ʈ����
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
            Debug.Log("���Ἲ��");
        }
        else
        {
            Debug.Log("�������");
        }
    }

    //�޽�������
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
            Debug.Log("���� ����");
        }
        else
            Debug.Log("�������");

        e.Completed -= onSendComplected;
    }
}
