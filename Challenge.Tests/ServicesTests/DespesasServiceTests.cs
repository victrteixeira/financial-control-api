using Bogus;
using Challenge.Core;

namespace Challenge.Tests;

public class DespesasServiceTests
{
    private readonly IDespesaService _sut;
    // Mocks
    private readonly IMapper _mapper;
    private readonly Mock<IDespesasRepository> _despesasRepositoryMock;
    
    public DespesasServiceTests()
    {
        _mapper = AutoMapperConfiguration.GetConfiguration();
        _despesasRepositoryMock = new Mock<IDespesasRepository>();

        _sut = new DespesaService
        (
            _despesasRepositoryMock.Object,
            _mapper
        );
    }
    
    #region Create
    
    
    [Fact(DisplayName = "Create A Valid Despesa")]
    [Trait("Category","Services")]
    public async Task Create_ValidDespesa_ReturnsDespesaDTO()
    {
        // Arrange
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesasCreated = _mapper.Map<Despesas>(despesaDto);

        _despesasRepositoryMock.Setup(x => x.SearchByDescricao(It.IsAny<string>()))
            .ReturnsAsync(() => default);

        _despesasRepositoryMock.Setup(x => x.Create(It.IsAny<Despesas>()))
            .ReturnsAsync(() => despesasCreated);
        despesasCreated.Validate();
        
        // Act
        var act = await _sut.CreateAsync(despesaDto);
        // Assert
        act.Should()
            .BeEquivalentTo(_mapper.Map<DespesasDTO>(despesasCreated));
    }
    
    [Fact(DisplayName = "Create When Despesa Already Added")]
    [Trait("Category","Services")]
    public void Create_WhenDespesaAlreadyAdded_ThrowDomainException()
    {
        // Arrange
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesaList = DespesasFixtures.CreateListValidDespesas();
        var despesa = _mapper.Map<Despesas>(despesaDto);

        despesaList[0] = despesa;
        
        // Act
        Func<Task<DespesasDTO>> act = async () => { return await _sut.CreateAsync(despesaDto); };
        //Assert
        act.Should()
            .ThrowAsync<DomainException>();
    }
    
    [Fact(DisplayName = "Create When Despesa Already Exist But With Different Month")]
    [Trait("Category","Services")]
    public async Task Create_WhenHasMonthDifferent_ReturnsDespesaDTO()
    {
        // Assert
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesaList = DespesasFixtures.CreateListValidDespesas();
        var despesa = _mapper.Map<Despesas>(despesaDto);
        despesa.Data = new DateTime(2022, 01, 04);

        despesaList[0] = despesa;
        
        
        _despesasRepositoryMock.Setup(x => x.SearchByDescricao(It.IsAny<string>()))
            .ReturnsAsync(() => despesaList);

        _despesasRepositoryMock.Setup(x => x.Create(It.IsAny<Despesas>()))
            .ReturnsAsync(() => despesa);
        despesa.Validate();
        
        // Act
        var act = await _sut.CreateAsync(despesaDto);
        // Assert
        act.Should()
            .BeEquivalentTo(_mapper.Map<DespesasDTO>(despesa));
    }
    
    [Fact(DisplayName = "Create When Despesa Already Exist But With Same Month")]
    [Trait("Category","Services")]
    public void Create_WhenHasSameMonth_ThrowDomainException()
    {
        // Arrange
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesaList = DespesasFixtures.CreateListValidDespesas();
        var despesa = _mapper.Map<Despesas>(despesaDto);

        despesa.Descricao = "SameThing";
        despesa.Data = new DateTime(2022, 01, 01);

        despesaList[0] = despesa;
    
        _despesasRepositoryMock.Setup(x => x.SearchByDescricao(It.IsAny<string>()))
            .ReturnsAsync(() => despesaList);
    
        // Act
        Func<Task<DespesasDTO>> act = async () => { return await _sut.CreateAsync(despesaDto); };
        // Assert
        act.Should()
            .ThrowAsync<DomainException>();
    }
    
    [Fact(DisplayName = "Update A Valid Despesa")]
    [Trait("Category","Services")]
    

    #endregion
    
    // Create an invalid dto
    public async Task Update_ValidDespesa_ReturnsDespesaDTO()
    {
        // Arrange
        var oldDespesa = DespesasFixtures.CreateValidDespesa();
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesaUpdated = _mapper.Map<Despesas>(despesaDto);

        _despesasRepositoryMock.Setup(x => x.Get(It.IsAny<long>()))
            .ReturnsAsync(() => oldDespesa).Verifiable();

        _despesasRepositoryMock.Setup(x => x.SearchByDescricao(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        _despesasRepositoryMock.Setup(x => x.Update(It.IsAny<Despesas>()))
            .ReturnsAsync(() => despesaUpdated);
        despesaUpdated.Validate();
        // Act
        var act = await _sut.UpdateAsync(despesaDto);
        // Assert
        act.Should()
            .BeEquivalentTo(_mapper.Map<DespesasDTO>(despesaUpdated));
    }
    
    
}