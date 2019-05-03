namespace DAL.Interface.DTO
{
    /// <summary>
    /// Represents bank account.
    /// </summary>
    public class AccountDTO
    {
        #region Properties
        public int Status { get; set; }

        public string Number { get; set; }

        public string OwnerUid { get; set; }

        public decimal Balance { get; set; }

        public int BonusPoints { get; set; }

        public int Rank { get; set; }
        #endregion
    }
}
