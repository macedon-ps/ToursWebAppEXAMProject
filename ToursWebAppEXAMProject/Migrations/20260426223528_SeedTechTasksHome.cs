using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedTechTasksHome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TechTaskPages",
                columns: new[] { "Id", "PageName" },
                values: new object[,]
                {
                    { 1, "Home" },
                    { 2, "Search" },
                    { 3, "Edit" },
                    { 4, "Support" },
                    { 5, "About" },
                    { 6, "Common" }
                });

            migrationBuilder.InsertData(
                table: "TechTaskItems",
                columns: new[] { "Id", "Description", "IsCompleted", "OrderNumber", "TechTaskPageId" },
                values: new object[,]
                {
                    { 1, "Данная страница д.содержать:\r\n			<br /><strong>блок навигационных кнопок:</strong>\r\n			\"Новости со всего мира\", \"Блоги о туризме\", \"ТЗ для Home\"\r\n			<br /><strong>большую пейзажную фоновую картинку</strong>\r\n			(м.б. море, тропический остров с пальмами)\r\n			<br /><strong>списки новостей и блогов о туризме с вертикальными скроллами</strong>,\r\n			которые загружаются при открытии страницы вертикальными полосами слева и справа;", false, 1, 1 },
                    { 2, "Д.б. создана <strong>страница со списком новостей</strong>,\r\n			которая открывается по нажатию на кнопку\r\n			<strong>\"Новости со всего мира\"</strong>.\r\n			<br />В списке новостей <strong>для каждой новости</strong>\r\n			указываются:\r\n			титульная картинка,\r\n			название новости,\r\n			краткое описание,\r\n			полное описание,\r\n			ссылка на полное описание.\r\n			<br />При нажатии на ссылку\r\n			<strong>\"Подробнее\"</strong>\r\n			открывается новость с:\r\n			титульной картинкой,\r\n			названием,\r\n			полным описанием,\r\n			id новости и датой сохранения.", false, 2, 1 },
                    { 3, "Д.б. создана <strong>страница со списком блогов</strong>,\r\n			которая открывается по нажатию на кнопку\r\n			<strong>\"Блоги о туризме\"</strong>.\r\n			<br />В списке блогов <strong>для каждого блога</strong>\r\n			указываются:\r\n			титульная картинка,\r\n			название блога,\r\n			краткое описание,\r\n			ссылка на полное описание.\r\n			<br />При нажатии на ссылку\r\n			<strong>\"Подробнее\"</strong>\r\n			открывается блог с:\r\n			титульной картинкой,\r\n			названием,\r\n			полным описанием,\r\n			сообщениями блоггеров,\r\n			id блога и датой сохранения.", false, 3, 1 },
                    { 4, "Д.б. реализована\r\n			<strong>возможность создания текстовых сообщений блоггеров,\r\n			редактирования и удаления со стороны редактора сайта</strong>.\r\n			<br />Д.б. реализован\r\n			<strong>механизм получения, пересылки и сохранения текстовых сообщений блоггеров</strong>.", false, 4, 1 },
                    { 5, "Д.б. реализована\r\n			<strong>функциональность оценивания прогресса выполнения технических задач\r\n			для каждой страницы</strong>.\r\n			<br />По нажатию на кнопки:\r\n			<strong>\"ТЗ для Home\"</strong>,\r\n			<strong>\"ТЗ для Search\"</strong>,\r\n			<strong>\"ТЗ для Admin\"</strong>,\r\n			<strong>\"ТЗ для Support\"</strong>,\r\n			<strong>\"ТЗ для About\"</strong>\r\n			открываются соответствующие страницы\r\n			со шкалой прогресса\r\n			и возможностью редактирования и сохранения выполненных ТЗ.", false, 5, 1 },
                    { 6, "Дополнительно:\r\n			<br />М.б. создан\r\n			<strong>дополнительный функционал:</strong>\r\n			<strong>\"последние прочитанные новости/блоги\"</strong>,\r\n			<strong>\"статистика прочтения новостей/\r\n			написания сообщений в блогах\"</strong>,\r\n			<strong>\"чаще всего читают новости/блоги\"</strong>\r\n			и т.д.", false, 6, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TechTaskPages",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
