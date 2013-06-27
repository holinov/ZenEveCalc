using System;

namespace Zen.EveCalc.Core.DataStorage
{
    [Serializable]
    public abstract class HasStringId : IHasStringId
    {
        /// <summary>
        ///     �� ������
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     �� ��������
        /// </summary>
        public virtual string SegmentId { get; set; }
    }
}