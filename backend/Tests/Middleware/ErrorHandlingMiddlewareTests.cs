using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using backend.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace backend.Tests.Middleware
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _mockLogger;

        public ErrorHandlingMiddlewareTests()
        {
            _mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
        }

        [Fact]
        public async Task InvokeAsync_CuandoOcurreExcepcion_DeberiaLoguearError()
        {
            var middleware = new ErrorHandlingMiddleware(
                next: (context) => throw new Exception("Error de prueba"),
                _mockLogger.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.InvokeAsync(context);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v != null && v.ToString()!.Contains("Excepción no manejada capturada por el middleware")),
                    It.IsAny<Exception?>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_ConArgumentNullException_DeberiaRetornar400()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var middleware = new ErrorHandlingMiddleware(
                next: (context) => throw new ArgumentNullException("parámetro requerido"),
                _mockLogger.Object);

            await middleware.InvokeAsync(context);

            Assert.Equal(400, context.Response.StatusCode);
        }
    }
}