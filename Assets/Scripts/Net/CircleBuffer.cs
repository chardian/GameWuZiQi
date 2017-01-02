using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CircleBuffer
{
    const int MAX_LEN = 64 * 128;
    private byte[] _buff;
    /// <summary>
    /// 当前游标的位置 / 也是数据结束的位置
    /// </summary>
    private int m_point;
    /// <summary>
    /// 开始的位置   / 也是读取数据的位置
    /// </summary>
    private int m_begin;

    public CircleBuffer()
    {
        _buff = new byte[MAX_LEN];
        m_point = 0;
        m_begin = 0;
    }

    /// <summary>
    /// 已用的长度
    /// </summary>
    /// <returns></returns>
    public int getLength()
    {
        //UnityEngine.Debug.Log(m_begin + "----->" + m_point);
        if (m_point > m_begin)
        {
            return m_point - m_begin;
        }
        else if (m_point == m_begin)
        {
            return 0;
        }
        else
        {
            return MAX_LEN - (m_begin - m_point);
        }
    }
    public bool appendData(byte[] buffAdd, int len)
    {
        if (getLength() + len > MAX_LEN)
        {
            UnityEngine.Debug.Log("length is not enough");
            return false;
        }
        if (m_point >= m_begin)
        {
            //没有正向填满
            if (m_point + len > MAX_LEN)
            {
                //填了之后会有的多
                int second = m_point + len - MAX_LEN;
                Array.Copy(buffAdd, 0, _buff, m_point, len - second);
                Array.Copy(buffAdd, 0, _buff, 0, second);
                m_point = second;
            }
            else
            {
                Array.Copy(buffAdd, 0, _buff, m_point, len);
                m_point += len;
            }
        }
        else
        {
            //如果终点在前，起点在后
            Array.Copy(buffAdd, 0, _buff, m_point, len);
            m_point += len;
        }
        return true;
    }

    /// <summary>
    /// 读取一个长度而已
    /// </summary>
    /// <returns></returns>
    public int readInt()
    {
        byte[] ret = new byte[4];
        int length = 4;
        int p = m_begin;
        for (int i = 0; i < length; i++)
        {
            if (p < MAX_LEN)
            {
                ret[i] = _buff[p];
                p++;
            }
            else
            {
                p = 0;
                ret[i] = _buff[p];
                p++;
            }
        }
        return BitConverter.ToInt32(ret, 0);
    }

    /// <summary>
    /// 起始位置往后移动
    /// </summary>
    /// <param name="len"></param>
    public void moveNext(int len)
    {
        if(m_begin + len > MAX_LEN)
        {
            m_begin = m_begin + len - MAX_LEN;
        }
        else
        {
            m_begin = m_begin + len;
        }
    }

    /// <summary>
    /// 获取数据并且会后移
    /// </summary>
    /// <param name="len"></param>
    /// <returns></returns>
    public byte[] getBytes(int len)
    {
        byte[] ret = new byte[len];
        int index = m_begin;
        for (int i = 0; i < len; i++)
        {
            if(index >= MAX_LEN)
            {
                index = 0;
            }
            ret[i] = _buff[index];
            index++;
        }
        m_begin = index;
        return ret;
    }

    public void displayAll()
    {
        UnityEngine.Debug.Log("=============");

        for (int i = m_begin; i < m_point; i++)
        {
            UnityEngine.Debug.Log(_buff[i]);
        }
        UnityEngine.Debug.Log("=============");
    }
}