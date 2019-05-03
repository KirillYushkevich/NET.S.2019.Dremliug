using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Fake
{
    public static class Storage
    {
        public static Dictionary<string, (int, string, decimal, int, int)> AccountBase { get; } = new Dictionary<string, (int, string, decimal, int, int)>
        {
            // [AccNumber] = (AccStatus, OwnerUid, Balance, BonusPoints, Rank)
            ["AccNumber01"] = (0, "OwnerUid01", 101M, 15, 0),
            ["AccNumber02"] = (0, "OwnerUid01", 10001M, 15, 3),
            ["AccNumber03"] = (0, "OwnerUid02", 10002M, 15, 3),
            ["AccNumber04"] = (0, "OwnerUid03", 100003M, 15, 4),
            ["AccNumber05"] = (0, "OwnerUid04", 104M, 15, 0),
        };

        public static Dictionary<string, (string, string)> AccountOwnerBase { get; } = new Dictionary<string, (string, string)>
        {
            // [OwnerUid] = (FirstName, LastName)
            ["OwnerUid01"] = ("FirstName01", "LastName01"),
            ["OwnerUid02"] = ("FirstName02", "LastName02"),
            ["OwnerUid03"] = ("FirstName03", "LastName03"),
            ["OwnerUid04"] = ("FirstName04", "LastName04"),
        };
    }
}
