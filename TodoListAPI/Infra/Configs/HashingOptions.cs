﻿using System;

namespace TodoListAPI.Infra.Configs
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
