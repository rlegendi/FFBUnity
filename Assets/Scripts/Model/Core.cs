﻿using Fumbbl.Ffb.Dto.ModelChanges;
using Fumbbl.Model.Types;
using System;
using System.Collections.Generic;

namespace Fumbbl.Model
{
    public class Core
    {
        //private ModelChangeFactory ModelChangeFactory { get; }
        private ReflectedFactory<ModelUpdater<Ffb.Dto.ModelChange>, Type> ModelChangeFactory { get; }
        public ActingPlayer ActingPlayer { get; set; }
        private Dictionary<string, Player> Players { get; set; }
        public Coach HomeCoach { get; internal set; }
        public Dictionary<int, View.PushbackSquare> PushbackSquares;

        internal void AddPushbackSquare(PushbackSquare square)
        {
            int key = square.coordinate[0] * 100 + square.coordinate[1];
            if (!PushbackSquares.ContainsKey(key))
            {
                PushbackSquares.Add(key, new View.PushbackSquare(square));
            } else
            {
                PushbackSquares[key].Refresh(new View.PushbackSquare(square));
            }
        }

        internal void RemovePushbackSquare(PushbackSquare square)
        {
            int key = square.coordinate[0] * 100 + square.coordinate[1];
            PushbackSquares.Remove(key);
        }

        public Coach AwayCoach { get; internal set; }

        public Ball Ball;

        public Core()
        {
            //ModelChangeFactory = new ModelChangeFactory();
            ModelChangeFactory = new ReflectedFactory<ModelUpdater<Ffb.Dto.ModelChange>, Type>();
            ActingPlayer = new ActingPlayer();
            Players = new Dictionary<string, Player>();
            Ball = new Ball();
            PushbackSquares = new Dictionary<int, View.PushbackSquare>();
        }

        public void Clear()
        {
            Players.Clear();
            ActingPlayer.Clear();
            PushbackSquares.Clear();
        }

        internal IEnumerable<Player> GetPlayers()
        {
            return Players.Values;
        }

        internal void ApplyChange(Ffb.Dto.ModelChange change)
        {
            //IModelUpdater updater = ModelChangeFactory.Create(change);
            ModelUpdater<Ffb.Dto.ModelChange> updater = ModelChangeFactory.GetReflectedInstance(change.GetType());
            updater?.Apply(change);
        }

        internal Player GetPlayer(string playerId)
        {
            if (playerId == null)
            {
                return null;
            }
            return Players[playerId];
        }

        internal void AddPlayer(Player player)
        {
            if (Players.ContainsKey(player.Id))
            {
                Players[player.Id] = player;
            }
            else
            {
                Players.Add(player.Id, player);
            }
        }
    }
}