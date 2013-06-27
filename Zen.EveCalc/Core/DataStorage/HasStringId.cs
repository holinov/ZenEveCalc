using System;

namespace Zen.EveCalc.Core.DataStorage
{
    [Serializable]
    public abstract class HasStringId : IHasStringId
    {
        /// <summary>
        ///     Ид записи
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     ИД сегмента
        /// </summary>
        public virtual string SegmentId { get; set; }
    }
}