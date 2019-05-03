using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<AccountDTO> LoadAccountBase();

        void SaveAccountBase(IEnumerable<AccountDTO> accountBase);

        AccountDTO LoadAccount(string number);

        void SaveAccount(AccountDTO account);

        IEnumerable<AccountOwnerDTO> LoadAccountOwnerBase();

        void SaveAccountOwnerBase(IEnumerable<AccountOwnerDTO> ownerBase);

        AccountOwnerDTO LoadAccountOwner(string ownerUid);

        void SaveAccountOwner(AccountOwnerDTO owner);
    }
}
