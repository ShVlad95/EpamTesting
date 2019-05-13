using System;

namespace CmdParser
{
    /// <summary>
    /// Интерфейс параметров командной строки.
    /// </summary>
    public interface ICmdArgsParser
    {
        /// <summary>
        /// Получить значения свойства.
        /// При этом необходимо указать тип и
        /// соответсвующий для этого типа парсер.
        /// </summary>
        /// <typeparam name="T">Обобщенный тип.</typeparam>
        /// <param name="propertyName">Имя свойства, значение которого хотим получить.</param>
        /// <param name="parser">Парсер.</param>
        /// <returns>Значение свойства.</returns>
        T GetPropertyValue<T>(string propertyName, Func<string, T> parser);

        /// <summary>
        /// Получить значение свойства
        /// в строковом формате.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        /// <returns>Значение свойства.</returns>
        string GetPropertyStringValueOrNull(string propertyName);
    }
}
