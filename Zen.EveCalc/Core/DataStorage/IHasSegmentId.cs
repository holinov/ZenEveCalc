namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ������ ��� �������� � ��������
    /// </summary>
    public interface IHasSegmentId
    {
        /// <summary>
        ///     �� ��������
        /// </summary>
        string SegmentId { get; set; }
    }
}