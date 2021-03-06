﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using MongoDragons.Repository.Context;
using MongoDragons.Types;
using MongoDragons.Managers.Helpers;

namespace MongoDragons.Managers
{
    public static class DragonManager
    {
        #region Names

        private static string[] _firstNames = new string[]
        {
            "White",
            "Black",
            "Light",
            "Dark",
            "Evil",
            "Cunning",
            "Magic",
            "Silver",
            "Golden",
            "Slimy"
        };

        private static string[] _lastNames = new string[]
        {
            "Legendary",
            "Sneaky",
            "Cheating",
            "Stealth",
            "Serpent",
            "Ghost",
            "Chimaera",
            "Hippogryph",
            "Spirit",
            "Skeleton"
        };

        #endregion

        public static List<Dragon> GetAll()
        {
            return DbContext.Current.All<Dragon>().OrderBy(d => d.Name).ToList();
        }

        public static List<Dragon> Find(string keyword)
        {
            List<Dragon> dragons = null;

            if (keyword.Length > 0)
            {
                dragons = DbContext.Current.All<Dragon>().Where(d => d.Name.ToLower().Contains(keyword.ToLower())).OrderBy(d => d.Name).ToList();
            }
            else
            {
                dragons = GetAll();
            }

            return dragons;
        }

        public static void Save(Dragon dragon)
        {
            DbContext.Current.Add(dragon);
        }

        public static void Delete(Dragon dragon)
        {
            DbContext.Current.Delete<Dragon>(d => d.Id == dragon.Id);
        }

        #region Helpers

        public static Dragon CreateRandom()
        {
            Dragon dragon = new Dragon();
            dragon.Name = HelperManager.CreateRandomName(_firstNames, _lastNames);
            dragon.Age = HelperManager.RandomGenerator.Next(1, 101);
            dragon.Description = "A big dragon.";
            dragon.Gold = HelperManager.RandomGenerator.Next(1, 1001);
            dragon.Weapon = new Breath { Name = "Breath", Description = "A breath attack.", Type = (Breath.BreathType)HelperManager.RandomGenerator.Next(0, 6) };
            dragon.MaxHP = HelperManager.RandomGenerator.Next(10, 21);
            dragon.HP = dragon.MaxHP;
            dragon.Realm = RealmManager.CreateRandom();

            return dragon;
        }

        #endregion
    }
}
