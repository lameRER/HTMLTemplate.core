using System;

namespace HTMLTemplate.core.BL.Interface
{
    /// <summary>
    /// Описание платформы
    /// </summary>
    internal interface IPlatformProperty
    {
        /// <summary>
        /// Текущая платформа
        /// </summary>
        public PlatformID PlatformId { get; internal set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; internal set; }
    }
}