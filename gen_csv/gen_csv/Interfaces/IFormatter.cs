﻿namespace gen_csv.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFormatter<T>
    {
        void FormatToFile(T arg);
    }
}
