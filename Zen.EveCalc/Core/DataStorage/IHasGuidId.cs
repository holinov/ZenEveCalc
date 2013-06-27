using System;

namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ������ � GUID ���������������
    /// </summary>
    public interface IHasGuidId : IHasStringId
    {
        /// <summary>
        ///     ���� ������
        /// </summary>
        Guid Guid { get; set; }
    }
}