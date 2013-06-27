namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ������ �� ��������� ���������������
    /// </summary>
    public interface IHasStringId : IHasSegmentId
    {
        /// <summary>
        ///     �� ������
        /// </summary>
        string Id { get; set; }
    }
}