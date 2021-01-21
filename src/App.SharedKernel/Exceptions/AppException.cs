﻿using System;

namespace App.SharedKernel.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message)
        {

        }

        protected AppException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
