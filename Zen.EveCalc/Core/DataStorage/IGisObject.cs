namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     Объект отображаемый на ГИС
    /// </summary>
    public interface IGisObject
    {
        /// <summary>
        ///     Широта
        /// </summary>
        float Lat { get; set; }

        /// <summary>
        ///     Долгота
        /// </summary>
        float Long { get; set; }
    }
}