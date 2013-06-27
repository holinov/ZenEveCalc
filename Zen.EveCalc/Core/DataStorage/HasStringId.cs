using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Zen.EveCalc.Annotations;

namespace Zen.EveCalc.Core.DataStorage
{
    [Serializable]
    public abstract class HasStringId : IHasStringId, INotifyPropertyChanged
    {
        /// <summary>
        ///     Ид записи
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     ИД сегмента
        /// </summary>
        public virtual string SegmentId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}