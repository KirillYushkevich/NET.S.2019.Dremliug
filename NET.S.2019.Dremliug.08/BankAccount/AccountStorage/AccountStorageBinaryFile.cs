using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class AccountStorageBinaryFile : IAccountStorage
    {
        private readonly string _filePath = "SavedBooks.bin";

        public AccountStorageBinaryFile(string filePath = null)
        {
            _filePath = filePath ?? throw new AccountStorageException("Path must not be null");
        }

        public void SaveAccountBase(IEnumerable<IBankAccount> accountBase)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_filePath, FileMode.Create)))
            {
                foreach (IBankAccount account in accountBase)
                {
                    writer.Write((int)account.Status);
                    writer.Write(account.ID);
                    writer.Write(account.Owner.FirstName);
                    writer.Write(account.Owner.LastName);
                    writer.Write(account.Balance);
                    writer.Write((int)account.Rank);
                    writer.Write(account.BonusPoints);
                }
            }
        }

        IEnumerable<IBankAccount> IAccountStorage.LoadAccountBase()
        {
            using (var reader = new BinaryReader(File.Open(_filePath, FileMode.Open)))
            {
                while (reader.PeekChar() != -1)
                {
                    IBankAccount currentAccount = null;
                    try
                    {
                        AccountStatus status = (AccountStatus)reader.ReadInt32();
                        ulong id = reader.ReadUInt64();
                        IAccountOwner owner = new AccountOwner(reader.ReadString(), reader.ReadString());
                        decimal balance = reader.ReadDecimal();
                        AccountRank rank = (AccountRank)reader.ReadInt32();
                        int bonuspoints = reader.ReadInt32();

                        currentAccount = new AccountTemplate(id, owner, balance, rank);
                        currentAccount.Status = status;
                        currentAccount.BonusPoints = bonuspoints;
                    }

                    // Log account reading errors.
                    catch(BankAccountException){ }

                    yield return currentAccount;
                }
            }
        }
    }
}
