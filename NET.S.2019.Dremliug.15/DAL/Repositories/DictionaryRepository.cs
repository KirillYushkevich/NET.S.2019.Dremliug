using System;
using System.Collections.Generic;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using DAL.Mappers;

namespace DAL.Repositories
{
    public class DictionaryRepository : IAccountRepository
    {
        #region fields
        private readonly Dictionary<string, (int, string, decimal, int, int)> _accounts;
        private readonly Dictionary<string, (string, string)> _owners;
        #endregion

        #region Constructor
        public DictionaryRepository(Dictionary<string, (int, string, decimal, int, int)> accounts, Dictionary<string, (string, string)> owners)
        {
            _accounts = accounts ?? throw new ArgumentNullException(nameof(accounts));
            _owners = owners ?? throw new ArgumentNullException(nameof(owners));
        }
        #endregion

        #region Load/Save Account
        public AccountDTO LoadAccount(string number)
        {
            return DictionaryMapper.MapToAccountDTO(number, _accounts[number]);
        }

        public IEnumerable<AccountDTO> LoadAccountBase()
        {
            foreach (KeyValuePair<string, (int, string, decimal, int, int)> kvp in _accounts)
            {
                yield return this.LoadAccount(kvp.Key);
            }
        }

        public void SaveAccount(AccountDTO account)
        {
            var kvp = DictionaryMapper.MapFromAccountDTO(account);
            _accounts[kvp.Key] = kvp.Value;
        }

        public void SaveAccountBase(IEnumerable<AccountDTO> accountBase)
        {
            foreach (AccountDTO account in accountBase)
            {
                this.SaveAccount(account);
            }
        }
        #endregion

        #region Load/Save Owner
        public AccountOwnerDTO LoadAccountOwner(string ownerUid)
        {
            return DictionaryMapper.MapToAccountOwnerDTO(ownerUid, _owners[ownerUid]);
        }

        public IEnumerable<AccountOwnerDTO> LoadAccountOwnerBase()
        {
            foreach (KeyValuePair<string, (string, string)> kvp in _owners)
            {
                yield return this.LoadAccountOwner(kvp.Key);
            }
        }

        public void SaveAccountOwner(AccountOwnerDTO owner)
        {
            var kvp = DictionaryMapper.MapFromAccountOwnerDTO(owner);
            _owners[kvp.Key] = kvp.Value;
        }

        public void SaveAccountOwnerBase(IEnumerable<AccountOwnerDTO> ownerBase)
        {
            foreach (AccountOwnerDTO owner in ownerBase)
            {
                this.SaveAccountOwner(owner);
            }
        }
        #endregion
    }
}
