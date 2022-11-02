using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

//패킷구성
public class Packet
{
    public int m_Type { get; set; }
    public byte[] m_data { get; set; }

    public Packet()
    {
    }
    
    public void SetData(byte[] data, int length)
    {
        m_data = new byte[length];
        Array.Copy(data, m_data, length);        
    }

    public byte[] GetSendBytes()
    {
        byte[] type_bytes = BitConverter.GetBytes(m_Type);
        int header_size = (int)(m_data.Length);
        byte[] header_bytes = BitConverter.GetBytes(header_size);
        byte[] send_bytes = new byte[header_bytes.Length + type_bytes.Length + m_data.Length];

        //[헤더 + 패킷 타입 + 데이터] 헤더 = 데이트byte배열의 크기, 데이터 = 어떤 객체를 c#의 마샬링을 이용해 직렬화.
        Array.Copy(header_bytes, 0, send_bytes, 0, header_bytes.Length);
        Array.Copy(type_bytes, 0, send_bytes, 0, type_bytes.Length);
        Array.Copy(m_data, 0, send_bytes, header_bytes.Length + type_bytes.Length, m_data.Length);
        
        return send_bytes;
    }
}

//데이터 직렬화
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //마샤링을 위한 것 pack = 1은 1byte단위로 데이터의 크기를 맞춘다는 것.
public class Data<T> where T : class
{
    public Data()
    {

    }

    public byte[] Serialize() // 객체를 Byte단위의 배열로 만드는 것.
    {
        var size = Marshal.SizeOf(typeof(T));
        var array = new byte[size];
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(this, ptr, true);
        Marshal.Copy(ptr, array, 0, size);
        Marshal.FreeHGlobal(ptr);
        return array;
    }

    public static T Deserialize(byte[] array) // Byte배열을 객체로 복구할때 쓴다.
    {
        var size = Marshal.SizeOf(typeof(T));
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(array, 0, ptr, size);
        var s = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return s;
    }
}


