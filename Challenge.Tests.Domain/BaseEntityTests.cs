using Challenge.Domain;
using Challenge.Domain.Validators;
using FluentValidation.TestHelper;

namespace Challenge.Tests.Domain;

[TestFixture]
public class BaseEntityTests
{
    private BaseEntityValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new BaseEntityValidator();
    }

    [Test]
    [TestCase(null, 5, "2022/05/22")]
    [TestCase("1234", 5, "2022/05/22")]
    [TestCase("PgvwVnPdYkESSjYUVGFAPktsfnNmbhzDeZtDkdaPTQgTCNPZjGceRFFfSSuJbuBWFJmBPpPwkgudREHSjyqehazSbwJHybzdpyvCDSadjk", 5, "2022/05/22")]
    public void Descricao_IsNotValid_ShouldThrowAnError(string desc, double valor, DateTime data)
    {
        // Arrange
        var despesas = new Receitas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(despesas);
        
        // Assert that should be a validation error for "Descricao" property.
        result.ShouldHaveValidationErrorFor(d => d.Descricao);
    }

    [Test]
    [TestCase("Something", 0.99, "2022/04/22")]
    [TestCase("Something", 0.932131, "2022/04/22")]
    [TestCase("Something", 0.888888888888888, "2022/04/22")]
    [TestCase("Something", 0.99999999999, "2022/04/22")]
    public void Valor_IsNotValid_ShouldThrowAnError(string desc, double valor, DateTime data)
    {
        // Arrange
        var despesas = new Despesas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(despesas);
        
        // Assert that should be a validation error for "Valor" property
        result.ShouldHaveValidationErrorFor(v => v.Valor);
    }

    [Test]
    [TestCase("Something", 1, "2023/01/01")]
    [TestCase("Something", 1, "2021/12/30")]
    [TestCase("Something", 1, "2025/02/03")]
    [TestCase("Something", 1, "2026/05/01")]
    [TestCase("Something", 1, "2019/12/19")]
    [TestCase("Something", 1, "2003/03/30")]
    [TestCase("Something", 1, "2014/09/19")]
    [TestCase("Something", 1, "2023/07/23")]
    public void Data_IsNotValid_ShouldThrowAnError(string desc, double valor, DateTime data)
    {
        // Arrange
        var despesas = new Despesas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(despesas);
        
        // Assert that should be a validation error for "Data" property
        result.ShouldHaveValidationErrorFor(d => d.Data);
    }

    [Test]
    [TestCase("kdasjdklsajdklas", 5, "2023/01/01")]
    [TestCase("TestingAutommattedsad", 5, "2023/01/01")]
    [TestCase("asdqw", 5, "2023/01/01")]
    [TestCase("Vi!dsadjkls@&*(&(@)(@_", 5, "2023/01/01")]
    public void Descricao_IsValid_ShouldReturnOk(string desc, double valor, DateTime data)
    {
        // Arrange
        var receitas = new Receitas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(receitas);
        
        // Assert that should there are no failures to "Descricao" property
        result.ShouldNotHaveValidationErrorFor(d => d.Descricao);
    }

    [Test]
    [TestCase("Something", 1, "2022/04/22")]
    [TestCase("Something", 3654689789132132456978.00, "2022/04/22")]
    [TestCase("Something", 999999999999954665456798791231.05645467498, "2022/04/22")]
    [TestCase("Something", 26, "2022/04/22")]
    [TestCase("Something", 21364, "2022/04/22")]
    public void Valor_IsValid_ShouldReturnOk(string desc, double valor, DateTime data)
    {
        // Arrange
        var receitas = new Receitas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(receitas);
        
        // Assert that should there are no failures to "Valor" property
        result.ShouldNotHaveValidationErrorFor(d => d.Valor);
    }


    [Test]
    [TestCase("Something", 1, "2022/01/01")]
    [TestCase("Something", 3654689789132132456978.00, "2022/02/01")]
    [TestCase("Something", 999999999999954665456798791231.05645467498, "2022/12/31")]
    [TestCase("Something", 26, "2022/07/24")]
    [TestCase("Something", 21364, "2022/11/3")]
    public void Data_IsValid_ShouldReturnOk(string desc, double valor, DateTime data)
    {
        // Arrange
        var receitas = new Receitas(desc, valor, data);
        // Act
        var result = _validator.TestValidate(receitas);
        
        // Assert that should there are no failures to "Valor" property
        result.ShouldNotHaveValidationErrorFor(d => d.Data);
    }
}