// Copyright (c) 2025 Infineon. All rights reserved.
// 
// All rights are reserved. Reproduction or transmission in whole or in part,
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// 
// File Name: Extensions.cs
// Created: 13.04.2026
// Author: keuschni [Marko Keuschnig]

using Microsoft.Extensions.DependencyInjection;

namespace MetricDb;

public static class Extensions
{
    public static IServiceCollection AddMetricDb(this IServiceCollection services)
    {
        return services;
    }
}