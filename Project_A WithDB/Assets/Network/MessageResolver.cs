using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MessageResolver
{
    public delegate void CompletedMessageCallback(Packet packet);

    int m_message_size;
    byte[] m_message_buffer = new byte[1024 * 2000]; //2000k
    byte[] m_header_buffer = new byte[4]; //4byte
    byte[] m_type_buffer = new byte[2]; //2byte

    int m_head_position;
    int m_type_position;
    int m_current_position;

    short m_message_type;
    int m_remain_bytes;

    bool m_head_completed;
    bool m_type_complected;
    bool m_completed;

    CompletedMessageCallback m_complete_callback;

    public MessageResolver()
    {
        ClearBuffer();
    }

    public void OnReceive(byte[] buffer, int offset, int transffered, CompletedMessageCallback callback)
    {
        //현재 들어온 데이터의 위치를 저장
        int scr_position = offset;
        
        //메세지가 완성되었다면 콜백함수 호출
        m_complete_callback = callback;

        //처리해야할 메시지 양을 저장
        m_remain_bytes = transffered;

        if(!m_head_completed)
        {
           
        }
    }



    public void ClearBuffer()
    {
        Array.Clear(m_message_buffer, 0, m_message_buffer.Length);
        Array.Clear(m_header_buffer, 0, m_header_buffer.Length);
        Array.Clear(m_type_buffer, 0, m_type_buffer.Length);

        m_message_size = 0;
        m_head_position = 0;
        m_type_position = 0;
        m_current_position = 0;
        m_message_type = 0;
        //m_remain_bytes			= 0;

        m_head_completed = false;
        m_type_complected = false;
        m_completed = false;
    }
    
}
