namespace Challenge.Tests;

public class DespesasServiceTests
{
    private readonly IDespesaService _sut;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDespesasRepository> _repositoryMock;
    public DespesasServiceTests()
    {
        _repositoryMock = new Mock<IDespesasRepository>();
        _mapper = new Mock<IMapper>();
        _sut = new DespesaService
        (
            _repositoryMock.Object,
            _mapper.Object
        );
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var responseList = DespesasFixtures.CreateListValidDespesasResponse();
        _mapper.Setup(x => x.Map<List<ResponseDespesa>>(It.IsAny<List<Despesas>>())).Returns(responseList);
        // Act
        var res = await _sut.GetAllAsync();
        // Assert
        res.Should().BeEquivalentTo(responseList);
    }
}