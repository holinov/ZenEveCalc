namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ������ ������������ �� ���
    /// </summary>
    public interface IGisObject
    {
        /// <summary>
        ///     ������
        /// </summary>
        float Lat { get; set; }

        /// <summary>
        ///     �������
        /// </summary>
        float Long { get; set; }
    }
}