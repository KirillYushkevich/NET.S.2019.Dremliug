using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Mappers
{
    internal static class DictionaryMapper
    {
        #region Account
        internal static AccountDTO MapToAccountDTO(string key, (int, string, decimal, int, int) value)
        {
            return new AccountDTO() { Status = value.Item1, Number = key, OwnerUid = value.Item2, Balance = value.Item3, BonusPoints = value.Item4, Rank = value.Item5 };
        }

        internal static KeyValuePair<string, (int, string, decimal, int, int)> MapFromAccountDTO(AccountDTO account)
        {
            return new KeyValuePair<string, (int, string, decimal, int, int)>(account.Number, (account.Status, account.OwnerUid, account.Balance, account.BonusPoints, account.Rank));
        } 
        #endregion

        #region Owner
        internal static AccountOwnerDTO MapToAccountOwnerDTO(string key, (string, string) value)
        {
            return new AccountOwnerDTO() { Uid = key, FirstName = value.Item1, LastName = value.Item2 };
        }

        internal static KeyValuePair<string, (string, string)> MapFromAccountOwnerDTO(AccountOwnerDTO owner)
        {
            return new KeyValuePair<string, (string, string)>(owner.Uid, (owner.FirstName, owner.LastName));
        } 
        #endregion
    }
}
