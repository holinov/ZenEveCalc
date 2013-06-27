using System;
using System.Collections.Generic;
using System.Linq;

namespace Zen.EveCalc.Core.DataStorage
{
    /// <summary>
    ///     ����������� ��������
    /// </summary>
    /// <typeparam name="TEntity">��� �������</typeparam>
    public interface IRepository<TEntity> : IDisposable
    {
        /// <summary>
        ///     �������� ������
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        ///     �������� ������
        /// </summary>
        IQueryable<TEntity> QuerySync { get; }

        /// <summary>
        ///     ����� ������ �� �� ���������� ��
        /// </summary>
        /// <param name="id">�� �������</param>
        /// <returns>������ �� ��</returns>
        TEntity Find(string id);

        IQueryable<TEntity> Find(IEnumerable<string> ids);

        /// <summary>
        ///     ��������� ������ � ��
        /// </summary>
        /// <param name="entity">������</param>
        void Store(TEntity entity);

        void StoreBulk(IEnumerable<TEntity> entities);

        /// <summary>
        ///     ������� ������ �� ��
        /// </summary>
        /// <param name="entity">������</param>
        void Delete(TEntity entity);

        /// <summary>
        ///     ��������� ��������� ������
        /// </summary>
        void SaveChanges();

        void DeleteAttach(string key);
    }
}