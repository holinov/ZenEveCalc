namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     Объект со строковым идентификатором
    /// </summary>
    public interface IHasStringId : IHasSegmentId
    {
        /// <summary>
        ///     Ид записи
        /// </summary>
        string Id { get; set; }
    }
}