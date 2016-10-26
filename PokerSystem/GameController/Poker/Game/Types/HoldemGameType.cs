﻿using CoinPokerCommonLib;
using CoinPokerServer.PokerSystem.GameController.Poker.Game.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinPokerServer.PokerSystem.CommonExtensions;

namespace CoinPokerServer.PokerSystem.GameController.Poker.Game.GameElement
{
    class HoldemGameType : BaseGameType, IGameType
    {
        public void OnGameStart()
        {
            //Tworzymy i losujemy karty
            InitializeCards();

            //Rozdajemy po dwie karty dla uczestników gry
            foreach (PlayerModel player in Game.GameTableModel.PlayerHavingPlayStatus())
            {
                //Dajemy dla gracza dwie karty
                List<CardModel> playerCards = new List<CardModel>();
                playerCards.Add(Helper.Pop<CardModel>(CardList));
                playerCards.Add(Helper.Pop<CardModel>(CardList));

                //Zapisujemy w pamieci karty gracza
                player.Cards = playerCards;

                //Dodajemy akcję doręczenia kart dla użytkownika, która jest
                //jednoznaczna z wysłaniem informacji do wszystkich graczy
                CardBacksideAction cardAction = new CardBacksideAction()
                {
                    Count = 2,
                    CreatedAt = DateTime.Now,
                    Player = player
                };

                Game.GameTableModel.ActionHistory.Add(cardAction);

                Game.SendPlayerCards(player);
            }
        }
    }
}
