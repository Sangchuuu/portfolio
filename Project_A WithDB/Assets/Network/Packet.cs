using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

//��Ŷ����
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

        //[��� + ��Ŷ Ÿ�� + ������] ��� = ����Ʈbyte�迭�� ũ��, ������ = � ��ü�� c#�� �������� �̿��� ����ȭ.
        Array.Copy(header_bytes, 0, send_bytes, 0, header_bytes.Length);
        Array.Copy(type_bytes, 0, send_bytes, 0, type_bytes.Length);
        Array.Copy(m_data, 0, send_bytes, header_bytes.Length + type_bytes.Length, m_data.Length);
        
        return send_bytes;
    }
}

//������ ����ȭ
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)] //�������� ���� �� pack = 1�� 1byte������ �������� ũ�⸦ ����ٴ� ��.
public class Data<T> where T : class
{
    public Data()
    {

    }

    public byte[] Serialize() // ��ü�� Byte������ �迭�� ����� ��.
    {
        var size = Marshal.SizeOf(typeof(T));
        var array = new byte[size];
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(this, ptr, true);
        Marshal.Copy(ptr, array, 0, size);
        Marshal.FreeHGlobal(ptr);
        return array;
    }

    public static T Deserialize(byte[] array) // Byte�迭�� ��ü�� �����Ҷ� ����.
    {
        var size = Marshal.SizeOf(typeof(T));
        var ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(array, 0, ptr, size);
        var s = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return s;
    }
}


