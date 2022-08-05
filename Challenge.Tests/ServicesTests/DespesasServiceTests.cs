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
    
    [Fact(DisplayName = "Create Despesa When Despesa Doesn't Exist")]
    [Trait("Category","Services")]
    public async Task Create_WhenDespesaDoesntExist()
    {
        // Arrange
        var despesaDto = DespesasFixtures.CreateValidDespesasDTO();
        var despesasCreated = _mapper.Map<Despesas>(despesaDto);

        _despesasRepositoryMock.Setup(x => x.SearchByDescricao(It.IsAny<string>()))
            .ReturnsAsync(() => default);

        _despesasRepositoryMock.Setup(x => x.Create(It.IsAny<Despesas>()))
            .ReturnsAsync(despesasCreated);
        // Act
        var res = await _sut.CreateAsync(despesaDto);
        // Assert
        res.Should()
            .BeEquivalentTo(_mapper.Map<DespesasDTO>(despesasCreated));
    }
    
    
    [Fact(DisplayName = "")]
    [Trait("Category","Services")]
    public async Task Test1()
    {
    }
}