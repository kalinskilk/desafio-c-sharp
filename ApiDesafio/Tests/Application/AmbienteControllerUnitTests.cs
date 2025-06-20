using ApiDesafio.API.Controllers;
using ApiDesafio.Application.Services;
using ApiDesafio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AmbienteControllerTests
{
    private readonly Mock<IAmbienteService> _ambienteServiceMock;
    private readonly AmbienteController _controller;

    public AmbienteControllerTests()
    {
        _ambienteServiceMock = new Mock<IAmbienteService>();
        _controller = new AmbienteController(_ambienteServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithAmbientes()
    {
        // Arrange
        var ambientes = new List<AmbienteDto>
        {
            new AmbienteDto { Id = 1, NomeUnico = "Dev" },
            new AmbienteDto { Id = 2, NomeUnico = "Prod" }
        };

        _ambienteServiceMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(ambientes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<AmbienteDto>>(okResult.Value);
        Assert.Equal(2, ((List<AmbienteDto>)returnValue).Count);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionWithAmbiente()
    {
        // Arrange
        var input = new CreateAmbienteDto { NomeUnico = "Teste" };
        var createdDto = new AmbienteDto { Id = 42, NomeUnico = input.NomeUnico };

        _ambienteServiceMock.Setup(s => s.CreateAmbienteAsync(input))
            .ReturnsAsync(createdDto);

        _ambienteServiceMock.Setup(s => s.GetByIdAsync(createdDto.Id))
            .ReturnsAsync(createdDto);

        // Act
        var result = await _controller.Create(input);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(createdDto.Id, ((AmbienteDto)createdResult.Value).Id);
    }
}
