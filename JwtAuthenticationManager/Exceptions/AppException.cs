﻿using System.Globalization;

namespace JwtAuthenticationManager.Exceptions;

public class AppException: Exception
{
    public AppException(string? message): base(message) {}
    
    public AppException(string message, params object[] args)
        :base(String.Format(CultureInfo.CurrentCulture, message, args)) {}
}