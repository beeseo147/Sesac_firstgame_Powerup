using Nettention.Proud;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Item;

namespace Nettention.Proud
{
    public class CustomMarshaler : Marshaler
    {
        //랭크 시스템 PlayersRank
        public static bool Read(Message msg, out SortedDictionary<int,int> playersRank)
        {
            playersRank = new SortedDictionary<int, int>();

            long size = 0;
            msg.Read(out size);

            for (int i = 0; i < size; ++i)
            {
                int playerNo = 0;
                int playerRank = 0;

                Read(msg, out playerNo);
                Read(msg, out playerRank);

                playersRank.Add(playerNo, playerRank);
            }

            return true;
        }

        public static void Write(Message msg, SortedDictionary<int, int> playersRanks)
        {
            msg.Write((long)playersRanks.Count);

            foreach (KeyValuePair<int, int> player in playersRanks)
            {
                Write(msg, player.Key);
                Write(msg, player.Value);
            }
        }

        //플레이어 준비상태 PlayersReady
        public static bool Read(Message msg, out SortedDictionary<int, bool> playersReady)
        {
            playersReady = new SortedDictionary<int, bool>();

            long size = 0;
            msg.Read(out size);

            for (int i = 0; i < size; ++i)
            {
                int playerNo = 0;
                bool playerreadyed = false;

                Read(msg, out playerNo);
                Read(msg, out playerreadyed);

                playersReady.Add(playerNo, playerreadyed);
            }

            return true;
        }

        public static void Write(Message msg, SortedDictionary<int, bool> playersReady)
        {
            msg.Write((long)playersReady.Count);

            foreach (KeyValuePair<int, bool> player in playersReady)
            {
                Write(msg, player.Key);
                Write(msg, player.Value);
            }
        }
        
        //플레이어 움직임 PlayerMove
        public static bool Read(Message msg, out List<int> PlayerMoveDirection)
        {
            PlayerMoveDirection = new List<int>();
            long size = 0;
            msg.Read(out size);

            for (int i = 0; i < size; ++i)
            {
                int playerMove = 0;

                Read(msg, out playerMove);

                PlayerMoveDirection.Add(playerMove);
            }

            return true;
        }

        public static void Write(Message msg, List<int> PlayerMoveDirection)
        {
            msg.Write((long)PlayerMoveDirection.Count);

            foreach (int player in PlayerMoveDirection)
            {
                Write(msg, player);
            }
        }
    }
}