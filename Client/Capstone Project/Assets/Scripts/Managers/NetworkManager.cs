using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager
{
    public enum PacketID
    {
        MyName = 1,
        MyPage = 2,
        Load30 = 3,
    }


	ServerSession _session = new ServerSession();
	string host;
	IPHostEntry ipHost;
	IPAddress ipAddr;
	IPEndPoint endPoint;

	Connector connector;

    byte waitingForConnect = 0;
    byte numOfWaitingPacket = 0;

    bool rankPacketArrival = false;
    bool myPagePacketArrival = false;
    bool loadStarPacketArrival = false;
    bool waitForPacketToReconnect = false;
    bool connectedToServer = false;

    public Action ConnectAction;
    public Action<ArraySegment<byte>> SendAction;

    
    Queue<ArraySegment<byte>> sendingPendingList = new Queue<ArraySegment<byte>>();


    float time = 0;

    public byte NumOfWaitingConnection
    {
        get { return waitingForConnect; }
        set { waitingForConnect = value; }
    }

    public byte NumOfWaitingPacket
    {
        get { return numOfWaitingPacket; }
        set { numOfWaitingPacket = value; }
    }

    public bool RankPacketArrival
    {
        get { return rankPacketArrival; }
        set { rankPacketArrival = value; }
    }

    public bool MyPagePacketArrival
    {
        get { return myPagePacketArrival; }
        set { myPagePacketArrival = value; }
    }

    public bool LoadStarPacketArrival
    {
        get { return loadStarPacketArrival; }
        set { loadStarPacketArrival = value; }
    }

    public bool Connected
    {
        get { return connectedToServer; }
        set { connectedToServer = value; }
    }

    public Queue<ArraySegment<byte>> SendingPendingList
    {
        get { return sendingPendingList; }
        set { sendingPendingList = value; }
    }

    public void ConnectAndSend(ArraySegment<byte> sendBuff, bool waitForResponse = false)
    {
        if (numOfWaitingPacket == 0 || sendingPendingList.Count == 0) // ���� ��⸦ �ϰ� �ִ� ��Ŷ�� ���ٸ� ���� ����
            ConnectToServer();

        sendingPendingList.Enqueue(sendBuff); // pendingList�� ��Ŷ�� �־���� ������ �Ϸ�Ǿ��� �� �����Ѵ�.

        if (waitForResponse) // Response�� ��ٷ��� �ϴ� �����̶�� numofWaitingPacket + 1 �� �����ν� ���� ���� �����Ѵ�
            numOfWaitingPacket++;
    }


    public void Send(ArraySegment<byte> sendBuff)
	{

        _session.Send(sendBuff);
	}

	public void Init()
    {
        // DNS (Domain Name System)

        host = Dns.GetHostName();
        ipHost = Dns.GetHostEntry(host);
        ipAddr = IPAddress.Parse("3.39.181.102"); //
        endPoint = new IPEndPoint(ipAddr, 7777);

        //host = Dns.GetHostName();
        //ipHost = Dns.GetHostEntry("NLB-CodingIsland-9cfc1fd3f5aa14cc.elb.ap-northeast-2.amazonaws.com");
        //ipAddr = ipHost.AddressList[0];
        //endPoint = new IPEndPoint(ipAddr, 7777);

        connector = new Connector();

        ConnectAction += ConnectToServer;
        SendAction += Send; // Stateless ���� ���� SendAction�� ��� �� Connect�� �Ϸ�Ǹ� Send;

        //connector.Connect(endPoint,
        //    () => { return _session; },
        //    1);
    }

    public void ConnectToServer() // Stateless ������ ���� �޼ҵ�
    {

        Connected = false;

        _session = new ServerSession();

        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

	public void TryReConnectToServer() // Stateful ������ ���� �޼ҵ�
    {

        if (rankPacketArrival && myPagePacketArrival && loadStarPacketArrival)
        {
            _session.Disconnect();
            _session = new ServerSession();

            connector.Connect(endPoint,
            () => { return _session; },
            1);

            waitForPacketToReconnect = false;
        }
        else
        {
            waitForPacketToReconnect = true;
        }
		
	}

    public void OnUpdate()
	{
        if (waitForPacketToReconnect)
            TryReConnectToServer();
        else
        {
            time += Time.deltaTime;
            if (time > 300f)
            {
                time = 0;
                TryReConnectToServer();
            }
        }
	}


}
