﻿using Bogus;
using Bogus.DataSets;
using Challenge.Domain;
using Challenge.Services.DTO;

namespace Challenge.Tests.Fixtures;

public class DespesasFixtures
{
    private static DateTime begin = new DateTime(2021, 12, 31);
    private static DateTime end = new DateTime(2023, 01, 01);
    
    public static Despesas CreateValidDespesa()
    {
        return new Despesas
        (
            descricao: new Finance().TransactionType(),
            valor: new Randomizer().Double(1, 1000),
            data: new Date().Between(begin, end)
        );
    }

    public static List<Despesas> CreateListValidDespesas(int limit = 5)
    {
        var list = new List<Despesas>();

        for (int i = 0; i < limit; i++)
        {
            list.Add(CreateValidDespesa());
        }

        return list;
    }

    public static DespesasDTO CreateValidDespesasDTO(bool newId = false)
    {
        return new DespesasDTO
        {
            Id = newId ? new Randomizer().Int(0, 10000) : 0,
            Descricao = new Finance().TransactionType(),
            Valor = new Randomizer().Double(1, 1000),
            Data = new Date().Between(begin, end)
        };
    }

    public static DespesasDTO CreateInvalidDespesasDTO()
    {
        return new DespesasDTO
        {
            Id = 0,
            Descricao = "",
            Data = new DateTime(2023, 12, 03),
            Valor = 0.88
        };
    }
}