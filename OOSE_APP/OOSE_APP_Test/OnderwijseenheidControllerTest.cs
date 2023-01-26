using Logic.Models;
using Logic.Models.DocumentExportEnImport;
using Logic.Services.Interfaces;

namespace OOSE_APP.Test
{
    public class OnderwijseenheidControllerTest
    {
        private readonly IOnderwijseenheidService onderwijseenheidService;

        public OnderwijseenheidControllerTest()
        {
            onderwijseenheidService = new OnderwijseenheidServiceMock();
        }

        [Fact]
        public async void GetAll()
        {
            var onderwijseenheden = await onderwijseenheidService.GetAllOnderwijseenheden(string.Empty);

            Assert.Equal(onderwijseenheden.Count, 4);
        }

        [Fact]
        public async void GetById_ShouldFind()
        {
            var onderwijseenheid = await onderwijseenheidService.GetOnderwijseenheidById(1, string.Empty);

            Assert.NotNull(onderwijseenheid);
        }

        [Fact]
        public async void GetById_ShouldNotFind()
        {
            var onderwijseenheid = await onderwijseenheidService.GetOnderwijseenheidById(9, string.Empty);

            Assert.Null(onderwijseenheid);
        }

        [Fact]
        public async void Update()
        {
            var onderwijseenheid = await onderwijseenheidService.GetOnderwijseenheidById(2, string.Empty);
            onderwijseenheid.Studiepunten = 20;

            await onderwijseenheidService.UpdateOnderwijseenheid(2, onderwijseenheid, string.Empty);

            onderwijseenheid = await onderwijseenheidService.GetOnderwijseenheidById(2, string.Empty);

            Assert.Equal(onderwijseenheid.Studiepunten, 20);
        }
    }
}
