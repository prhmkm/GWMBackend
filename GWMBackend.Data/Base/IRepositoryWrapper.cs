﻿using GWMBackend.Data.Interface;

namespace GWMBackend.Data.Base
{
    public interface IRepositoryWrapper
    {
        IEmailRepository email { get; }
        IOrderRepository order { get; }

        void Save();
    }
}