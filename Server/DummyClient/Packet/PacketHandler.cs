﻿using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{

    public static void S_Challenge_Load_StarHandler(PacketSession session, IPacket packet)
    {
        S_Challenge_Load_Star pkt = packet as S_Challenge_Load_Star;
        ServerSession serverSession = session as ServerSession;

    }

    public static void S_Challenge_MyPageHandler(PacketSession session, IPacket packet)
    {
        S_Challenge_MyPage pkt = packet as S_Challenge_MyPage;
        ServerSession serverSession = session as ServerSession;

    }

    public static void S_Challenge_Top30RankHandler(PacketSession session, IPacket packet)
    {
        S_Challenge_Top30Rank pkt = packet as S_Challenge_Top30Rank;
        ServerSession serverSession = session as ServerSession;

    }

    /*public static void S_ChallengeTop30Handler(PacketSession session, IPacket packet)
    {
        S_ChallengeTop30 pkt = packet as S_ChallengeTop30;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);
    }*/
}
