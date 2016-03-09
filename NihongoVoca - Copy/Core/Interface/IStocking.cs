namespace Ivs.Core.Interface
{
    public interface IStocking
    {
        #region Validate Stocking

        int CheckCanStockOut(IDto stockTransactionDto);

        int CheckStockMinus(IDto stockTransactionDto);

        #endregion Validate Stocking

        #region Stocking Action

        int StockIn(IDto stockTransactionDto);

        int StockOut(IDto stockTransactionDto);

        int TransferStock(IDto stockTransactionDto);

        int TransferPosting(IDto stockTransactionDto);

        #endregion Stocking Action
    }
}