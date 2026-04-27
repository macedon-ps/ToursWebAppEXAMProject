using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToursWebAppEXAMProject.Migrations
{
    /// <inheritdoc />
    public partial class _SeedTechTasks_Search_Edit_Support_About : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TechTaskItems",
                columns: new[] { "Id", "Description", "IsCompleted", "OrderNumber", "TechTaskPageId" },
                values: new object[,]
                {
                    { 7, "Данная страница д.содержать:\r\n							<br /><strong>блок навигационных кнопок: </strong> \"Поиск туров\", \"Все турпродуктв\", \"Все страны\", \"Все города\", \"ТЗ для Search\"\r\n							< br />< strong > меню поиска:</ strong > страны,\r\n					города,\r\n					дат тура,\r\n					количества туристов и кнопка \"Найти тур\"\r\n							< br />< strong > информативное меню:</ strong > карта местности и описания страны,\r\n					города,\r\n					достопримечательностей\r\n							< br />< strong > галерея с фото:</ strong > фотографии страны,\r\n					городов,\r\n					достопримечательностей\r\n\r\n							< br /> По нажатию на кнопку < strong > \"Поиск туров\" или \"Найти тур\" </ strong > осуществляется < strong > функционал поиска турпродукта по заданным параметрам поиска </ strong > и выводится < strong > список всех найденных туристических продуктов </ strong > с вертикальным скроллом", false, 1, 2 },
                    { 8, "<strong>Меню поиска</strong> д.содержать: \r\n							<br/><strong>выпадающий список стран</strong> (элемент radiobutton - выбор только 1 страны);\r\n							<br/><strong>выпадающие календари</strong> с <strong>датами начала и окончания тура</strong>);\r\n							<br/><strong>выпадающий список - количество дней/ночей</strong> (элемент select - выбор 1 или больше вариантов);\r\n							<br/><strong>выпадающий список людей</strong> (взрослых) + возможность добавить детей (количество и возраст детей) (по умолчанию - 2 взрослых, без детей)\r\n							<br/><strong>кнопка \"Найти тур\"</strong>;\r\n							< br /> Д.б.организован < strong > поиск туристических продуктов по заданным параметрам </ strong > ", false, 2, 2 },
                    { 9, "<strong>Информативное меню</strong> под меню поиска, д. содержать:\r\n							<br/><strong>раскрытый список городов</strong> данной страны;\r\n							<br />(элемент select - выбор 1 или больше городов) (расположено - под страной) (при выборе страны - появляются города этой страны);\r\n							<br/><strong>д.б. карта местности</strong> (рядом с городами):\r\n							<br/><strong>описание:</strong>\r\n							<br />при выборе страны - в верхней трети - появляется <strong>описание о стране</strong>\r\n							<br />при выборе города - в средней трети - появляется <strong>описание о городе</strong>\r\n							<br />а в нижней трети - появляется <strong>описание о достопримечательности</strong>", false, 3, 2 },
                    { 10, "Д.б. реализована <strong>возможность просматривать карту страны</strong>, с нанесенными на нее городами и турпродуктами\r\n							", false, 4, 2 },
                    { 11, "Д.б. реализована <strong>галерея с фото страны, городов и достопримечательностей</strong>\r\n							<br />выводятся все фото, кот. есть в базе; если их много и они не помещаются, то появляется горизонт. скроллинг<br/>", false, 5, 2 },
                    { 12, "Дополнительно:\r\n							<br />м.б. реализована <strong>галерея с фото из гостинниц, кафе, других мест</strong>", false, 6, 2 },
                    { 13, "Данная страница д. содержать:\r\n							<br/><strong>блок навигационных кнопок: </strong> \"Редактировать новости\", \"Редактировать блоги\", \"Редактировать турпродукты\", \"ТЗ для Edit\";\r\n							< br />< strong > общее меню поиска сущностей(новостей / блогов / турпродуктов) по их полному названию / или по ключевому слову </ strong > из названия сущности для выборки из БД;\r\n							< br />< strong > страницу со списком выбранных сущностей </ strong >,\r\n					отвечающих критериям поиска.", false, 1, 3 },
                    { 14, "Д.б. создана <strong>страница создания, редактирования и удаления новости</strong>, ее названия, краткого и полного описания, пути к титульной картинке", false, 2, 3 },
                    { 15, "Д.б. создана <strong>страница создания, редактирования и удаления блога</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 3, 3 },
                    { 16, "Д.б. создана <strong>страница создания, редактирования и удаления туристического продукта</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 4, 3 },
                    { 17, "Д.б. организован <strong>доступ для входа на страницу - для редактирования данных БД</strong> - только для сотрудников турфирмы, у кот. <strong> роль \"superadmin\" илм \"editor\"</strong>", false, 5, 3 },
                    { 18, "Дополнительно:\r\n							<br />М.б. созданы <strong>3 роли с разным доступом: </strong>\r\n							<br />М.б. создана <strong>страница создания, редактирования и удаления страны</strong>, ее названия, краткого и полного описания, пути к титульной картинке\r\n							<br />М.б. создана <strong>страница создания, редактирования и удаления города</strong>, его названия, краткого и полного описания, пути к титульной картинке", false, 6, 3 },
                    { 19, "Данная страница д. содержать:\r\n							<br /><strong>блок навигационных кнопок:</strong> \"Перевод текста\", \"Карта\" или \"Прогноз погоды\", \"ТЗ для Support\";\r\n							< br /> по нажатию на кнопку < strong > \"Перевод текста\" </ strong > -раскрывается область,\r\n					в кот.слева - текст,\r\n					кот.\r\n							н.перевести,\r\n					а справа - перевод на иностр.язык\r\n							< br /> по нажатию на кнопку < strong > \"Карта\" </ strong > -раскрывается область,\r\n					в кот.появляется карта данной местности\r\n							< br /> по нажатию на кнопку < strong > \"Прогноз погоды\" </ strong > -раскрывается область,\r\n					в кот.появляется прогноз погоды для данной местности", false, 1, 4 },
                    { 20, "Д.б. создана <strong>страница с возможностью перевода текстов на иностранный язык</strong> с использованием <strong>Google Translate API</strong>", false, 2, 4 },
                    { 21, "Д.б. создана <strong>страница с выводимой картой местности</strong>, с использованием <strong>Google Map API</strong>", false, 3, 4 },
                    { 22, "М.б. создана <strong>страница с прогнозом погоды для данной местности</strong>, с использованием <strong>Open Weather API</strong>", false, 4, 4 },
                    { 23, "М.б. создана <strong>страница с информацией о странах, об их географическом положении, об их достопримечательностях</strong>", false, 5, 4 },
                    { 24, "Дополнительно:\r\n							<br />М.б. создана <strong>страница с информацией о странах, об их истории, политическом устройстве, праве, традициях и обычаях местного населения, об объектах сферы гостеприимства (гостинницы, кафе и т.д.)</strong>", false, 6, 4 },
                    { 25, "Данная страница д. содержать:\r\n							<br/><strong>блок навигационных кнопок:</strong> \"Наши реквизиты\", \"Режим работы\", \"Фотогаллерея\", \"Обратная связь\", \"Edit AboutPage\", \"ТЗ для About\"\r\n							< br />< strong > блок рекизитов тур.фирмы </ strong >\r\n							< br />< strong > блок с информацией о режиме работы турфирмы </ strong >\r\n							< br />< strong > блок с фотографиями путешествий по странам мира </ strong > ", false, 1, 5 },
                    { 26, "Д.б. <strong>страница для создания, редактирования и удаления разных версий страницы About</strong>.\r\n							<br />Д.б. организован <strong>доступ к данной функциональности по ролям \"superadmin\", \"editor\"</strong>", false, 2, 5 },
                    { 27, "Д.б. <strong>страница с формой обратной связи с клиентами туристической фирмы</strong>\r\n							<br />Д.б. реализован <strong>функционал создания, отправки, сохранения вопросов от клиентов и просмотра  ответов на них от туристической фирмы</strong> (+ для боковой панели)", false, 3, 5 },
                    { 28, "Д.б. реализована возможность <strong>звонить из приложения и отправлять сообщения по email</strong> (+ для боковой панели)", false, 4, 5 },
                    { 29, "Д.б. реализована возможность <strong>общения с клиентами через социальные сети Facebook, WhatsApp, Telegram, Viber</strong> (+ для боковой панели)", false, 5, 5 },
                    { 30, "Дополнительно:\r\n							<br />М.б. реализован <strong>функционал подсчета стоимости поездки</strong> (+ для боковой панели)", false, 6, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "TechTaskItems",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
