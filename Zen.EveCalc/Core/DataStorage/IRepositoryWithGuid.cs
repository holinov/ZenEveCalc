using System;
using System.Collections.Generic;
using System.Linq;

namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ����������� ��������
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryWithGuid<TEntity> : IRepository<TEntity>
    {
        /// <summary>
        ///     ����� ������ �� GUID
        /// </summary>
        /// <param name="guid">���������� �� �������</param>
        /// <returns></returns>
        TEntity Find(Guid guid);

        IQueryable<TEntity> Find(IEnumerable<Guid> guids);

        /// <summary>
        ///     ����������� ��������
        /// </summary>
        /// <param name="entity">��������, ������� ����� �����������</param>
        void Clone(TEntity entity);

        /// <summary>
        ///     ���������
        /// </summary>
        /// <param name="entity">��������</param>
        void Detach(TEntity entity);

        void DeleteById(string id);

        IEnumerable<TEntity> GetAll();
    }
}