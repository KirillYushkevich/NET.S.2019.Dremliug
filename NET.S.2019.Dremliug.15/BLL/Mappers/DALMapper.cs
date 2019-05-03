using BLL.Interface.Entities.BankAccount;
using BLL.Interface.Entities.BankAccountOwner;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    internal static class DALMapper
    {
        #region Owner
        internal static AccountOwner MapToAccountOwnerEntity(AccountOwnerDTO accountOwnerDTO)
        {
            return new AccountOwner(accountOwnerDTO.Uid, accountOwnerDTO.FirstName, accountOwnerDTO.LastName);
        }

        internal static AccountOwnerDTO MapFromAccountOwnerEntity(AccountOwner accountOwner)
        {
            return new AccountOwnerDTO() { Uid = accountOwner.Uid, FirstName = accountOwner.FirstName, LastName = accountOwner.LastName };
        }
        #endregion

        #region Account
        internal static Account MapToAccountEntity(AccountDTO accountDTO, AccountOwnerDTO accountOwnerDTO)
        {
            var accountOwner = MapToAccountOwnerEntity(accountOwnerDTO);
            return new Account(accountDTO.Number, accountOwner, accountDTO.Balance, (AccountRank)accountDTO.Rank) { Status = (AccountStatus)accountDTO.Status, BonusPoints = accountDTO.BonusPoints };
        }

        internal static AccountDTO MapFromAccountEntity(Account account)
        {
            return new AccountDTO { Status = (int)account.Status, Number = account.Number, OwnerUid = account.Owner.Uid, Balance = account.Balance, BonusPoints = account.BonusPoints, Rank = (int)account.Rank };
        } 
        #endregion
    }
}
