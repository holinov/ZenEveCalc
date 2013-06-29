using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Zen.EveCalc.Annotations;

namespace Zen.EveCalc.Core.DataStorage
{
    [Serializable]
    public abstract class HasStringId : IHasStringId, INotifyPropertyChanged
    {
        private bool _isDirty;

        /// <summary>
        ///     Ид записи
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     ИД сегмента
        /// </summary>
        public virtual string SegmentId { get; set; }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (value.Equals(_isDirty)) return;
                _isDirty = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "IsDirty") IsDirty = true;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}