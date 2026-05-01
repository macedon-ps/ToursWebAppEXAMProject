using ToursWebAppEXAMProject.Interfaces;
using ToursWebAppEXAMProject.Models;

namespace ToursWebAppEXAMProject.Utils
{
    public class TechTaskItemUtils
    {
        private readonly IBaseInterface<TechTaskItem> _AllTechTaskItems;

        public TechTaskItemUtils(IBaseInterface<TechTaskItem> AllTechTaskItems)
        {
            _AllTechTaskItems = AllTechTaskItems;
        }


        public IEnumerable<TechTaskItem> GetTechTaskItems()
        {
            return _AllTechTaskItems.GetAllItems();
        }


        public TechTaskItem GetTechTaskItemById(int id)
        {
            var techTaskItem = _AllTechTaskItems.GetItemById(id);
            
            return techTaskItem;
        }


        public IEnumerable<TechTaskItem> QueryResult(bool isFullName, string insertedText)
        {
            var techTaskItems = _AllTechTaskItems.GetQueryResultItemsAfterFullName(insertedText, isFullName);

            return techTaskItems;
        }


        public void DeleteTechTaskItemById(TechTaskItem techTaskItem)
        {
            _AllTechTaskItems.DeleteItem(techTaskItem, techTaskItem.Id);
        }


        public void SaveTechTaskItem(TechTaskItem techTaskItem)
        {
            if (techTaskItem != null)
            {
                _AllTechTaskItems.SaveItem(techTaskItem, techTaskItem.Id);
            }
        }
    }
}
