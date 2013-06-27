using System;
using System.ComponentModel;
using Raven.Imports.Newtonsoft.Json;

namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     Денормализованная ссылка на объект
    /// </summary>
    /// <typeparam name="TRefObject">Тип объекта</typeparam>
    [Serializable]
    public class Refrence<TRefObject> : IRefrence where TRefObject : class, IHasStringId
    {
        /// <summary>
        ///     Объект на который ссылаемся
        /// </summary>
        [JsonIgnore]
        public virtual TRefObject Object
        {
            get
            {
                return Repository != null && !RefrenceHacks.SkipRefrences
                           ? Repository.Find(Id)
                           : default(TRefObject);
            }
            set
            {
                if (value != null)
                {
                    Id = value.Id;
                    if (Repository != null && Repository.Find(Id) == null)
                    {
                        Repository.Store(value);
                        Repository.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        ///     Репозитарий объектов
        /// </summary>
        [JsonIgnore]
        public virtual IRepository<TRefObject> Repository { get; set; }

        /// <summary>
        ///     Ид на который ссылаемся
        /// </summary>
        [DisplayName("Идентификатор объекта")]
        public string Id { get; set; }
    }
}