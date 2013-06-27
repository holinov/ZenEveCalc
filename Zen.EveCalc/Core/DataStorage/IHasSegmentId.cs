namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     Объект для хранения в шардинге
    /// </summary>
    public interface IHasSegmentId
    {
        /// <summary>
        ///     ИД сегмента
        /// </summary>
        string SegmentId { get; set; }
    }
}