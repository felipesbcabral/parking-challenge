﻿using ParkingChallenge.Core.Domain.Exceptions;

namespace ParkingChallenge.Core.Domain.Validation;
public static class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
            throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be empty or null");
    }

    public static void MinValue(int target, int minValue, string fieldName)
    {
        if (target < minValue)
            throw new EntityValidationException($"{fieldName} should be at least {minValue}");
    }

    public static void MaxValue(int target, int maxValue, string fieldName)
    {
        if (target > maxValue)
            throw new EntityValidationException($"{fieldName} should be less or equal to {maxValue}");
    }
}
